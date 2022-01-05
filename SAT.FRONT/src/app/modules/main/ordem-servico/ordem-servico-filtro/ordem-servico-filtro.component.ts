import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormControl } from '@angular/forms';
import { MatSidenav } from '@angular/material/sidenav';
import { ClienteService } from 'app/core/services/cliente.service';
import { FilialService } from 'app/core/services/filial.service';
import { RegiaoAutorizadaService } from 'app/core/services/regiao-autorizada.service';
import { StatusServicoService } from 'app/core/services/status-servico.service';
import { TipoIntervencaoService } from 'app/core/services/tipo-intervencao.service';
import { Cliente, ClienteParameters } from 'app/core/types/cliente.types';
import { PontoEstrategicoEnum } from 'app/core/types/equipamento-contrato.types';
import { Filial, FilialParameters } from 'app/core/types/filial.types';
import { RegiaoAutorizadaParameters } from 'app/core/types/regiao-autorizada.types';
import { Regiao } from 'app/core/types/regiao.types';
import { Autorizada, AutorizadaParameters } from 'app/core/types/Autorizada.types';
import { StatusServico, StatusServicoParameters } from 'app/core/types/status-servico.types';
import { TipoIntervencao } from 'app/core/types/tipo-intervencao.types';
import { UserService } from 'app/core/user/user.service';
import { Subject } from 'rxjs';
import { debounceTime, distinctUntilChanged, takeUntil } from 'rxjs/operators';
import { AutorizadaService } from 'app/core/services/autorizada.service';
import { Tecnico, TecnicoParameters } from 'app/core/types/tecnico.types';
import { TecnicoService } from 'app/core/services/tecnico.service';
import { GrupoEquipamento } from 'app/core/types/grupo-equipamento.types';
import { TipoEquipamento } from 'app/core/types/tipo-equipamento.types';
import { Equipamento, EquipamentoFilterEnum } from 'app/core/types/equipamento.types';
import { EquipamentoService } from 'app/core/services/equipamento.service';
import { FilterBase } from 'app/core/filters/filter-base';
import { IFilterBase } from 'app/core/types/filtro.types';
import Enumerable from 'linq';
import { LocalAtendimentoService } from 'app/core/services/local-atendimento.service';
import { LocalAtendimento, LocalAtendimentoParameters } from 'app/core/types/local-atendimento.types';
import { statusConst } from 'app/core/types/status-types';

@Component({
  selector: 'app-ordem-servico-filtro',
  templateUrl: './ordem-servico-filtro.component.html'
})
export class OrdemServicoFiltroComponent extends FilterBase implements OnInit, IFilterBase {
  @Input() sidenav: MatSidenav;

  filiais: Filial[] = [];
  clientes: Cliente[] = [];
  regioes: Regiao[] = [];
  autorizadas: Autorizada[] = [];
  statusServicos: StatusServico[] = [];
  tiposIntervencao: TipoIntervencao[] = [];
  gruposEquip: GrupoEquipamento[] = [];
  tipoEquip: TipoEquipamento[] = [];
  equipamentos: Equipamento[] = [];
  tecnicos: Tecnico[] = [];
  locaisAtendimento: LocalAtendimento[] = [];
  pas: number[] = [];
  pontosEstrategicos: any;
  clienteFilterCtrl: FormControl = new FormControl();
  equipamentoCtrl: FormControl = new FormControl();

  protected _onDestroy = new Subject<void>();

  constructor(
    private _filialService: FilialService,
    private _tipoIntervencaoService: TipoIntervencaoService,
    private _equipamentoService: EquipamentoService,
    private _statusServicoService: StatusServicoService,
    private _clienteService: ClienteService,
    private _regiaoAutorizadaService: RegiaoAutorizadaService,
    private _autorizadaService: AutorizadaService,
    private _tecnicoService: TecnicoService,
    protected _userService: UserService,
    private _localAtendimentoSvc: LocalAtendimentoService,
    protected _formBuilder: FormBuilder
  ) {
    super(_userService, _formBuilder, 'ordem-servico');
  }

  ngOnInit(): void {
    this.createForm();
    this.loadData();
  }

  loadData(): void {
    this.pontosEstrategicos = PontoEstrategicoEnum;
    this.obterFiliais();
    this.obterClientes();
    this.obterTiposIntervencao();
    this.obterStatusServicos();
    this.registrarEmitters();

    this.aoSelecionarFilial();
    this.aoSelecionarCliente();
  }

  createForm(): void {
    this.form = this._formBuilder.group({
      codFiliais: [undefined],
      codRegioes: [undefined],
      codTecnicos: [undefined],
      codAutorizadas: [undefined],
      codTiposIntervencao: [undefined],
      codClientes: [undefined],
      codStatusServicos: [undefined],
      codOS: [undefined],
      numOSCliente: [undefined],
      numOSQuarteirizada: [undefined],
      dataAberturaInicio: [undefined],
      dataAberturaFim: [undefined],
      dataFechamentoInicio: [undefined],
      dataFechamentoFim: [undefined],
      pas: [undefined],
      codPostos: [undefined],
      pontosEstrategicos: [undefined],
      codEquipamentos: [undefined],
      dataHoraSolucaoInicio: [undefined],
      dataHoraSolucaoFim: [undefined],
      numSerie: [undefined]
    });

    this.form.patchValue(this.filter?.parametros);
  }

  async obterFiliais() {
    let params: FilialParameters = {
      indAtivo: statusConst.ATIVO,
      sortActive: 'nomeFilial',
      sortDirection: 'asc'
    };

    const data = await this._filialService
      .obterPorParametros(params)
      .toPromise();

    this.filiais = data.items;
  }

  async obterEquipamentos(filtro: string='') {
    const data = await this._equipamentoService
      .obterPorParametros({
        codClientes: this.form.controls['codClientes'].value,
        filterType: EquipamentoFilterEnum.FILTER_CHAMADOS,
        filter: filtro
      })
      .toPromise();

    this.equipamentos = data.items;
  }

  async obterTiposIntervencao() {
    let params = {
      indAtivo: statusConst.ATIVO,
      sortActive: 'nomTipoIntervencao',
      sortDirection: 'asc'
    }

    const data = await this._tipoIntervencaoService
      .obterPorParametros(params)
      .toPromise();

    this.tiposIntervencao = data.items;
  }

  async obterClientes(filtro: string = '') {
    let params: ClienteParameters = {
      filter: filtro,
      indAtivo: statusConst.ATIVO,
      sortActive: 'nomeFantasia',
      sortDirection: 'asc',
      pageSize: 1000
    };

    const data = await this._clienteService
      .obterPorParametros(params)
      .toPromise();

    this.clientes = data.items;
  }

  async obterTecnicos(filialFilter: string) {
    let params: TecnicoParameters = {
      indAtivo: statusConst.ATIVO,
      sortActive: 'nome',
      sortDirection: 'asc',
      codPerfil: 35,
      codFiliais: filialFilter,
      pageSize: 1000
    };

    const data = await this._tecnicoService
      .obterPorParametros(params)
      .toPromise();

    this.tecnicos = data.items;
  }

  aoSelecionarFilial() {
    this.form.controls['codFiliais']
      .valueChanges
      .subscribe(() => {
        if ((this.form.controls['codFiliais'].value && this.form.controls['codFiliais'].value != '')) {
          var filialFilter: any = this.form.controls['codFiliais'].value;

          this.obterTecnicos(filialFilter);
          this.obterRegioesAutorizadas(filialFilter);
          this.obterAutorizadas(filialFilter);

          this.form.controls['pas'].enable();
          this.form.controls['codRegioes'].enable();
          this.form.controls['codTecnicos'].enable();
          this.form.controls['codAutorizadas'].enable();
        }
        else {
          this.form.controls['pas'].disable();
          this.form.controls['codRegioes'].disable();
          this.form.controls['codTecnicos'].disable();
          this.form.controls['codAutorizadas'].disable();
        }
      });

    if (this.userSession.usuario.codFilial) {
      this.form.controls['codFiliais'].setValue([this.userSession.usuario.codFilial]);
      this.form.controls['codFiliais'].disable();
    }
    else {
      this.form.controls['codFiliais'].enable();
    }
  }

  aoSelecionarCliente() {
    if (
      this.form.controls['codClientes'].value &&
      this.form.controls['codClientes'].value != ''
    ) {
      this.obterEquipamentos();
      this.obterLocaisAtendimentos();
      this.form.controls['codPostos'].enable();
      this.form.controls['codEquipamentos'].enable();
    }
    else {
      this.form.controls['codPostos'].disable();
      this.form.controls['codEquipamentos'].disable();
    }

    this.form.controls['codClientes']
      .valueChanges
      .subscribe(() => {
        if (this.form.controls['codClientes'].value && this.form.controls['codClientes'].value != '') {
          this.obterEquipamentos();
          this.obterLocaisAtendimentos();
          this.form.controls['codPostos'].enable();
          this.form.controls['codEquipamentos'].enable();
        }
        else {
          this.form.controls['codPostos'].setValue(null);
          this.form.controls['codPostos'].disable();

          this.form.controls['codEquipamentos'].setValue(null);
          this.form.controls['codEquipamentos'].disable();
        }
      });
  }

  async obterRegioesAutorizadas(filialFilter: any) {
    let params: RegiaoAutorizadaParameters = {
      indAtivo: statusConst.ATIVO,
      codFiliais: filialFilter,
      pageSize: 1000
    };

    const data = await this._regiaoAutorizadaService
      .obterPorParametros(params)
      .toPromise();

    this.regioes = Enumerable.from(data.items).where(ra => ra.regiao?.indAtivo == 1).select(ra => ra.regiao).distinct(r => r.codRegiao).orderBy(i => i.nomeRegiao).toArray();
    this.pas = Enumerable.from(data.items).select(ra => ra.pa).distinct(r => r).orderBy(i => i).toArray();
  }

  async obterAutorizadas(filialFilter: any) {
    let params: AutorizadaParameters = {
      indAtivo: statusConst.ATIVO,
      codFiliais: filialFilter,
      pageSize: 1000
    };

    const data = await this._autorizadaService
      .obterPorParametros(params)
      .toPromise();

    this.autorizadas = Enumerable.from(data.items).orderBy(i => i.nomeFantasia).toArray();
  }

  async obterLocaisAtendimentos() {
    var filialFilter = this.form.controls['codFiliais'].value;
    var clienteFilter = this.form.controls['codClientes'].value;
    var regiaoFilter = this.form.controls['codRegioes'].value;
    var autorizadaFilter = this.form.controls['codAutorizadas'].value;

    let params: LocalAtendimentoParameters = {
      indAtivo: statusConst.ATIVO,
      codFiliais: filialFilter,
      codClientes: clienteFilter,
      codRegioes: regiaoFilter,
      codAutorizada: autorizadaFilter,
      sortActive: 'nomeLocal',
      sortDirection: 'asc',
      pageSize: 1000
    };

    const data = await this._localAtendimentoSvc
      .obterPorParametros(params)
      .toPromise();

    this.locaisAtendimento = Enumerable.from(data.items).orderBy(i => i.nomeLocal.trim()).toArray();
  }

  async obterStatusServicos() {
    let params: StatusServicoParameters = {
      indAtivo: statusConst.ATIVO,
      sortActive: 'nomeStatusServico',
      sortDirection: 'asc'
    }

    const data = await this._statusServicoService
      .obterPorParametros(params)
      .toPromise();

    this.statusServicos = data.items;
  }

  registrarEmitters(): void {
    this.clienteFilterCtrl.valueChanges
      .pipe(
        takeUntil(this._onDestroy),
        debounceTime(700),
        distinctUntilChanged()
      )
      .subscribe(() => {
        this.obterClientes(this.clienteFilterCtrl.value);
      });

    this.equipamentoCtrl.valueChanges
      .pipe(
        takeUntil(this._onDestroy),
        debounceTime(700),
        distinctUntilChanged()
      )
      .subscribe(() => {
        this.obterEquipamentos(this.equipamentoCtrl.value);
      });
  }

  limpar() {
    super.limpar();

    if (this.userSession?.usuario?.codFilial) {
      this.form.controls['codFiliais'].setValue([this.userSession.usuario.codFilial]);
      this.form.controls['codFiliais'].disable();
    }
  }

  ngOnDestroy() {
    this._onDestroy.next();
    this._onDestroy.complete();
  }
}
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
import { GrupoEquipamentoService } from 'app/core/services/grupo-equipamento.service';
import { GrupoEquipamento, GrupoEquipamentoParameters } from 'app/core/types/grupo-equipamento.types';
import { TipoEquipamento } from 'app/core/types/tipo-equipamento.types';
import { TipoEquipamentoService } from 'app/core/services/tipo-equipamento.service';
import { Equipamento, EquipamentoParameters } from 'app/core/types/equipamento.types';
import { EquipamentoService } from 'app/core/services/equipamento.service';
import { FilterBase } from 'app/core/filters/filter-base';
import { IFilterBase } from 'app/core/types/filtro.types';
import Enumerable from 'linq';

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
  pas: number[] = [];
  pontosEstrategicos: any;
  clienteFilterCtrl: FormControl = new FormControl();

  protected _onDestroy = new Subject<void>();

  constructor(
    private _filialService: FilialService,
    private _tipoIntervencaoService: TipoIntervencaoService,
    private _grupoEquipService: GrupoEquipamentoService,
    private _tipoEquipService: TipoEquipamentoService,
    private _equipamentoService: EquipamentoService,
    private _statusServicoService: StatusServicoService,
    private _clienteService: ClienteService,
    private _regiaoAutorizadaService: RegiaoAutorizadaService,
    private _autorizadaService: AutorizadaService,
    private _tecnicoService: TecnicoService,
    protected _userService: UserService,
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
    this.obterAutorizadas();
    this.obterTipoEquipamentos();

    this.configurarFiliais();
    this.configurarEquipamentos();
    this.configurarAutorizadas();
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
      pontosEstrategicos: [undefined],
      codTipoEquip: [undefined],
      codGrupoEquip: [undefined],
      codEquipamentos: [undefined]
    });

    this.form.patchValue(this.filter?.parametros);
  }

  async obterFiliais() {
    let params: FilialParameters = {
      indAtivo: 1,
      sortActive: 'nomeFilial',
      sortDirection: 'asc'
    };

    const data = await this._filialService
      .obterPorParametros(params)
      .toPromise();

    this.filiais = data.items;
  }

  async obterTipoEquipamentos() {
    let params = {
      sortActive: 'nomeTipoEquip',
      sortDirection: 'asc'
    }

    const data = await this._tipoEquipService
      .obterPorParametros(params)
      .toPromise();

    this.tipoEquip = data.items;
  }

  async obterGrupoEquipamentos() {
    let params: GrupoEquipamentoParameters =
    {
      codTipoEquip: this.form.controls['codTipoEquip'].value,
      sortActive: 'nomeGrupoEquip',
      sortDirection: 'asc'
    }

    const data = await this._grupoEquipService
      .obterPorParametros(params)
      .toPromise();

    this.gruposEquip = data.items;
  }


  async obterEquipamentos() {
    let params: EquipamentoParameters =
    {
      codGrupo: this.form.controls['codGrupoEquip'].value,
      codTipo: this.form.controls['codTipoEquip'].value,
      sortActive: 'nomeEquip',
      sortDirection: 'asc'
    }

    const data = await this._equipamentoService
      .obterPorParametros(params)
      .toPromise();

    this.equipamentos = data.items;
  }

  async obterTiposIntervencao() {
    let params = {
      indAtivo: 1,
      sortActive: 'nomTipoIntervencao',
      sortDirection: 'asc'
    }

    const data = await this._tipoIntervencaoService
      .obterPorParametros(params)
      .toPromise();

    this.tiposIntervencao = data.items;
  }

  async obterClientes(filter: string = '') {
    let params: ClienteParameters = {
      filter: filter,
      indAtivo: 1,
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
      indAtivo: 1,
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

  configurarFiliais() {
    if (!this.userSession.usuario.codFilial)
      this.form.controls['codFiliais']
        .valueChanges
        .subscribe(() => {
          this.obterRegioesAutorizadas(this.form.controls['codFiliais'].value);
          this.obterTecnicos(this.form.controls['codFiliais'].value);
        });

    if (this.userSession.usuario.codFilial) {
      this.form.controls['codFiliais'].setValue([this.userSession.usuario.codFilial]);
      this.form.controls['codFiliais'].disable();
    }
    else {
      this.form.controls['codFiliais'].enable();
    }

    this.obterTecnicos(this.form.controls['codFiliais'].value);
    this.obterRegioesAutorizadas(this.form.controls['codFiliais'].value);
  }

  configurarAutorizadas() {
    this.form.controls["codFiliais"].valueChanges.subscribe(() => {

    });
  }

  configurarEquipamentos() {
    if ((this.form.controls['codTipoEquip'].value))
      this.obterGrupoEquipamentos();
    else
      this.form.controls['codGrupoEquip'].disable();

    if ((this.form.controls['codGrupoEquip'].value))
      this.obterEquipamentos();
    else
      this.form.controls['codEquipamentos'].disable();


    this.form.controls['codTipoEquip']
      .valueChanges
      .subscribe(() => {
        if (this.form.controls['codTipoEquip'].value) {
          this.obterGrupoEquipamentos();
          this.form.controls['codGrupoEquip'].enable();
        }
        else {
          this.form.controls['codGrupoEquip'].setValue("");
          this.form.controls['codGrupoEquip'].disable();
        }
      });

    this.form.controls['codGrupoEquip']
      .valueChanges
      .subscribe(() => {
        if (this.form.controls['codGrupoEquip'].value) {
          this.obterEquipamentos();
          this.form.controls['codEquipamentos'].enable();
        }
        else {
          this.form.controls['codEquipamentos'].setValue("");
          this.form.controls['codEquipamentos'].disable();
        }
      });
  }

  async obterRegioesAutorizadas(filialFilter: any) {
    let params: RegiaoAutorizadaParameters = {
      indAtivo: 1,
      codFiliais: filialFilter,
      pageSize: 1000
    };

    const data = await this._regiaoAutorizadaService
      .obterPorParametros(params)
      .toPromise();

    this.regioes = Enumerable.from(data.items).select(ra => ra.regiao).distinct(r => r.codRegiao).toArray();
    this.pas = Enumerable.from(data.items).select(ra => ra.pa).distinct(r => r).toArray();
  }

  async obterAutorizadas(filter: string = '') {
    let params: AutorizadaParameters = {
      filter: filter,
      indAtivo: 1,
      codFiliais: this.form.controls['codFiliais'].value.join(','),
      pageSize: 1000
    };

    const data = await this._autorizadaService
      .obterPorParametros(params)
      .toPromise();

    this.autorizadas = Enumerable.from(data.items).toArray();
  }

  async obterStatusServicos() {
    let params: StatusServicoParameters = {
      indAtivo: 1,
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
  }

  clean() {
    super.clean();

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
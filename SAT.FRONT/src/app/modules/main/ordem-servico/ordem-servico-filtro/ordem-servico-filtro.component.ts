import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { AbstractControl, FormBuilder, FormControl, FormGroup, NgModel } from '@angular/forms';
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
import { UsuarioSessao } from 'app/core/types/usuario.types';
import { UserService } from 'app/core/user/user.service';
import { Subject } from 'rxjs';
import { debounceTime, distinctUntilChanged, takeUntil } from 'rxjs/operators';
import Enumerable from 'linq';
import { AutorizadaService } from 'app/core/services/autorizada.service';
import { Equipamento, EquipamentoParameters } from 'app/core/types/equipamento.types';
import { EquipamentoService } from 'app/core/services/equipamento.service';
import { Tecnico, TecnicoParameters } from 'app/core/types/tecnico.types';
import { TecnicoService } from 'app/core/services/tecnico.service';

@Component({
  selector: 'app-ordem-servico-filtro',
  templateUrl: './ordem-servico-filtro.component.html'
})
export class OrdemServicoFiltroComponent implements OnInit
{
  filtro: any;
  sessionData: UsuarioSessao;
  @Input() sidenav: MatSidenav;
  form: FormGroup;
  filiais: Filial[] = [];
  clientes: Cliente[] = [];
  regioes: Regiao[] = [];
  autorizadas: Autorizada[] = [];
  statusServicos: StatusServico[] = [];
  tiposIntervencao: TipoIntervencao[] = [];
  tecnicos: Tecnico[] = [];
  pas: number[] = [];
  equipamentos: Equipamento[] = [];
  pontosEstrategicos: any;
  clienteFilterCtrl: FormControl = new FormControl();
  protected _onDestroy = new Subject<void>();

  constructor (
    private _filialService: FilialService,
    private _tipoIntervencaoService: TipoIntervencaoService,
    private _statusServicoService: StatusServicoService,
    private _clienteService: ClienteService,
    private _userService: UserService,
    private _regiaoAutorizadaService: RegiaoAutorizadaService,
    private _autorizadaService: AutorizadaService,
    private _equipamentosService: EquipamentoService,
    private _formBuilder: FormBuilder,
    private _tecnicoService: TecnicoService
  )
  {
    this.filtro = this._userService.obterFiltro('ordem-servico');
    this.sessionData = JSON.parse(this._userService.userSession);
  }

  ngOnInit(): void
  {
    this.obterFiliais();
    this.obterClientes();
    this.obterTiposIntervencao();
    this.obterStatusServicos();
    this.registrarEmitters();
    this.inicializarForm();
    this.pontosEstrategicos = PontoEstrategicoEnum;
    this.obterAutorizadas();
    this.obterEquipamentos();

    this.configurarFiliais();
  }

  private inicializarForm(): void
  {
    this.form = this._formBuilder.group({
      codFiliais: [undefined],
      codRegioes: [undefined],
      codTecnicos: [undefined],
      codAutorizadas: [undefined],
      codTiposIntervencao: [undefined],
      codClientes: [undefined],
      codStatusServicos: [undefined],
      codEquipamentos: [undefined],
      codOS: [undefined],
      numOSCliente: [undefined],
      numOSQuarteirizada: [undefined],
      dataAberturaInicio: [undefined],
      dataAberturaFim: [undefined],
      dataFechamentoInicio: [undefined],
      dataFechamentoFim: [undefined],
      pas: [undefined],
      pontosEstrategicos: [undefined]
    });

    this.form.patchValue(this.filtro?.parametros);
  }

  async obterFiliais()
  {
    let params: FilialParameters = {
      indAtivo: 1,
      sortActive: 'nomeFilial',
      sortDirection: 'asc',
      pageSize: 50
    };

    const data = await this._filialService
      .obterPorParametros(params)
      .toPromise();

    this.filiais = data.items;
  }

  async obterTiposIntervencao()
  {
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

  async obterClientes(filter: string = '')
  {
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

  async obterTecnicos(filialFilter: string)
  {
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

  configurarFiliais()
  {
    if (!this.sessionData.usuario.codFilial)
      this.form.controls['codFiliais']
        .valueChanges
        .subscribe(() => 
        {
          this.obterRegioesAutorizadas(this.form.controls['codFiliais'].value);
          this.obterTecnicos(this.form.controls['codFiliais'].value);
        });

    if (this.sessionData.usuario.codFilial)
    {
      this.form.controls['codFiliais'].setValue([this.sessionData.usuario.codFilial]);
      this.form.controls['codFiliais'].disable();
    }

    this.obterTecnicos(this.form.controls['codFiliais'].value);
    this.obterRegioesAutorizadas(this.form.controls['codFiliais'].value);
  }

  async obterRegioesAutorizadas(filialFilter: any)
  {
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

  async obterAutorizadas(filter: string = '')
  {
    let params: AutorizadaParameters = {
      filter: filter,
      indAtivo: 1,
      codFilial: this.sessionData.usuario.codFilial,
      pageSize: 1000
    };

    const data = await this._autorizadaService
      .obterPorParametros(params)
      .toPromise();

    this.autorizadas = Enumerable.from(data.items).toArray();
  }

  async obterStatusServicos()
  {
    let params: StatusServicoParameters = {
      indAtivo: 1,
      sortActive: 'nomeStatusServico',
      sortDirection: 'asc',
      pageSize: 50
    }

    const data = await this._statusServicoService
      .obterPorParametros(params)
      .toPromise();

    this.statusServicos = data.items;
  }

  async obterEquipamentos()
  {
    let params: EquipamentoParameters = {
      sortActive: 'nomeEquip',
      sortDirection: 'asc',
      pageSize: 50
    }

    const data = await this._equipamentosService
      .obterPorParametros(params)
      .toPromise();

    this.equipamentos = data.items;
  }

  private registrarEmitters(): void
  {
    this.clienteFilterCtrl.valueChanges
      .pipe(
        takeUntil(this._onDestroy),
        debounceTime(700),
        distinctUntilChanged()
      )
      .subscribe(() =>
      {
        this.obterClientes(this.clienteFilterCtrl.value);
      });
  }

  aplicar(): void
  {
    const form: any = this.form.getRawValue();

    const filtro: any = {
      nome: 'ordem-servico',
      parametros: form
    }

    this._userService.registrarFiltro(filtro);

    const newFilter: any = { nome: 'ordem-servico', parametros: this.form.getRawValue() }
    const oldFilter = this._userService.obterFiltro('ordem-servico');

    if (oldFilter != null)
      newFilter.parametros =
      {
        ...newFilter.parametros,
        ...oldFilter.parametros
      };

    this._userService.registrarFiltro(newFilter);
    this.sidenav.close();
  }

  limpar(): void
  {
    this.form.reset();
    this.aplicar();
    this.sidenav.close();
  }

  ngOnDestroy()
  {
    this._onDestroy.next();
    this._onDestroy.complete();
  }

  selectAll(select: AbstractControl, values, propertyName)
  {
    if (select.value[0] == 0 && propertyName != '')
      select.patchValue([...values.map(item => item[`${propertyName}`]), 0]);
    else if (select.value[0] == 0 && propertyName == '')
      select.patchValue([...values.map(item => item), 0]);
    else
      select.patchValue([]);
  }
}
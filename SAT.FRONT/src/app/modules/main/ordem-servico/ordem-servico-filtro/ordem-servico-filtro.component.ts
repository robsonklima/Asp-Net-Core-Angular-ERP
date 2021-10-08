import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';
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
import { StatusServico, StatusServicoParameters } from 'app/core/types/status-servico.types';
import { TipoIntervencao } from 'app/core/types/tipo-intervencao.types';
import { UsuarioSessao } from 'app/core/types/usuario.types';
import { UserService } from 'app/core/user/user.service';
import { Subject } from 'rxjs';
import { debounceTime, distinctUntilChanged, takeUntil } from 'rxjs/operators';
import Enumerable from 'linq'

@Component({
  selector: 'app-ordem-servico-filtro',
  templateUrl: './ordem-servico-filtro.component.html'
})
export class OrdemServicoFiltroComponent implements OnInit {
  filtro: any;
  sessionData: UsuarioSessao;
  @Input() sidenav: MatSidenav;
  form: FormGroup;
  filiais: Filial[] = [];
  clientes: Cliente[] = [];
  regioes: Regiao[] = [];
  statusServicos: StatusServico[] =[];
  tiposIntervencao: TipoIntervencao[] = [];
  pontosEstrategicos: any;
  clienteFilterCtrl: FormControl = new FormControl();
  protected _onDestroy = new Subject<void>();

  constructor(
    private _filialService: FilialService,
    private _tipoIntervencaoService: TipoIntervencaoService,
    private _statusServicoService: StatusServicoService,
    private _clienteService: ClienteService,
    private _userService: UserService,
    private _regiaoAutorizadaService: RegiaoAutorizadaService,
    private _formBuilder: FormBuilder
  ) {
    this.filtro = this._userService.obterFiltro('ordem-servico');
    this.sessionData = JSON.parse(this._userService.userSession);
  }

  ngOnInit(): void {
    this.obterFiliais();
    this.obterClientes();
    this.obterTiposIntervencao();
    this.obterStatusServicos();
    this.registrarEmitters();
    this.inicializarForm();
<<<<<<< HEAD

    this.pontosEstrategicos = PontoEstrategicoEnum;  
=======
    this.obterRegioes();
>>>>>>> 03f92999c3c5ce79a87fae18a5522a0fe82f3f1a
  }

  private inicializarForm(): void {
    this.form = this._formBuilder.group({
      codFiliais: [undefined],
      codRegioes: [undefined],
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
      pa: [undefined],
      pontosEstrategicos: [undefined]
    });

    this.form.patchValue(this.filtro?.parametros);
  }

  async obterFiliais() {
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

  async obterRegioes(filter: string = '') {
    let params: RegiaoAutorizadaParameters = {
      filter: filter,
      indAtivo: 1,
      codAutorizada: this.sessionData.usuario.codAutorizada,
      pageSize: 1000
    };

    const data = await this._regiaoAutorizadaService
      .obterPorParametros(params)
      .toPromise();

    this.regioes =  Enumerable.from(data.items).select(ra => ra.regiao).distinct(r => r.codRegiao).toArray();
  }

  async obterStatusServicos() {
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

  private registrarEmitters(): void {
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

  aplicar(): void {
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

  limpar(): void {
    this.form.reset();
    this.aplicar();
    this.sidenav.close();
  }

  ngOnDestroy() {
    this._onDestroy.next();
    this._onDestroy.complete();
  }
}

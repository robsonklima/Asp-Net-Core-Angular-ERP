import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { MatSidenav } from '@angular/material/sidenav';
import { ClienteService } from 'app/core/services/cliente.service';
import { FilialService } from 'app/core/services/filial.service';
import { StatusServicoService } from 'app/core/services/status-servico.service';
import { TipoIntervencaoService } from 'app/core/services/tipo-intervencao.service';
import { Cliente, ClienteParameters } from 'app/core/types/cliente.types';
import { Filial, FilialParameters } from 'app/core/types/filial.types';
import { StatusServico, StatusServicoParameters } from 'app/core/types/status-servico.types';
import { TipoIntervencao } from 'app/core/types/tipo-intervencao.types';
import { UsuarioSessionData } from 'app/core/types/usuario.types';
import { UserService } from 'app/core/user/user.service';
import { Subject } from 'rxjs';
import { debounceTime, distinctUntilChanged, takeUntil } from 'rxjs/operators';

@Component({
  selector: 'app-ordem-servico-filtro',
  templateUrl: './ordem-servico-filtro.component.html'
})
export class OrdemServicoFiltroComponent implements OnInit {
  filtro: any;
  sessioData: UsuarioSessionData;
  @Input() sidenav: MatSidenav;
  form: FormGroup;
  filiais: Filial[] = [];
  clientes: Cliente[] = [];
  statusServicos: StatusServico[] =[];
  tiposIntervencao: TipoIntervencao[] = [];
  clienteFilterCtrl: FormControl = new FormControl();
  protected _onDestroy = new Subject<void>();

  constructor(
    private _filialService: FilialService,
    private _tipoIntervencaoService: TipoIntervencaoService,
    private _statusServicoService: StatusServicoService,
    private _clienteService: ClienteService,
    private _userService: UserService,
    private _formBuilder: FormBuilder
  ) {
    this.filtro = this._userService.obterFiltro('ordem-servico');
    this.sessioData = JSON.parse(this._userService.userSession);
  }

  ngOnInit(): void {
    this.obterFiliais();
    this.obterClientes();
    this.obterTiposIntervencao();
    this.obterStatusServicos();
    this.registrarEmitters();
    this.inicializarForm();
  }

  private inicializarForm(): void {
    this.form = this._formBuilder.group({
      codFiliais: [undefined],
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

    this.filiais = data.filiais;
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

    this.tiposIntervencao = data.tiposIntervencao;
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

    this.clientes = data.clientes;
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

    this.statusServicos = data.statusServico;
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

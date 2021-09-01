import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { MatSidenav } from '@angular/material/sidenav';
import { ClienteService } from 'app/core/services/cliente.service';
import { CustomSnackbarService } from 'app/core/services/custom-snackbar.service';
import { FilialService } from 'app/core/services/filial.service';
import { StatusServicoService } from 'app/core/services/status-servico.service';
import { TipoIntervencaoService } from 'app/core/services/tipo-intervencao.service';
import { Cliente, ClienteParameters } from 'app/core/types/cliente.types';
import { Filial, FilialParameters } from 'app/core/types/filial.types';
import { StatusServico, StatusServicoParameters } from 'app/core/types/status-servico.types';
import { TipoEquipamentoParameters } from 'app/core/types/tipo-equipamento.types';
import { TipoIntervencao } from 'app/core/types/tipo-intervencao.types';
import { UsuarioSessao } from 'app/core/types/usuario.types';
import { UserService } from 'app/core/user/user.service';
import moment from 'moment';

@Component({
  selector: 'app-dashboard-filtro',
  templateUrl: './dashboard-filtro.component.html'
})
export class DashboardFiltroComponent implements OnInit {
  @Input() sidenav: MatSidenav;
  form: FormGroup;
  tiposIntervencao: TipoIntervencao[] = [];
  clientes: Cliente[] = [];
  statusServicos: StatusServico[] = [];
  filiais: Filial[] = [];
  usuarioSessao: UsuarioSessao;
  filtro: any;

  constructor(
    private _formBuilder: FormBuilder,
    private _tipoIntervencaoService: TipoIntervencaoService,
    private _statusServicoService: StatusServicoService,
    private _filialService: FilialService,
    private _clienteService: ClienteService,
    private _userService: UserService,
    private _snack: CustomSnackbarService,
  ) {
    this.usuarioSessao = JSON.parse(this._userService.userSession);
    this.filtro = this._userService.obterFiltro('dashboard');
  }

  ngOnInit(): void {
    this.inicializarForm();
    this.obterClientes();
    this.obterStatusServicos();
    this.obterFiliais();
    this.obterTiposIntervencao();
  }

  private inicializarForm(): void {
    this.form = this._formBuilder.group({
      dataInicio: [undefined],
      dataFim: [undefined],
      codFiliais: [undefined],
      codTiposIntervencao: [undefined],
      codClientes: [undefined],
      codStatusServicos: [undefined],
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
    let params: TipoEquipamentoParameters = {
      sortActive: 'nomTipoIntervencao',
      sortDirection: 'asc',
      pageSize: 50
    }

    const data = await this._tipoIntervencaoService
      .obterPorParametros(params)
      .toPromise();

    this.tiposIntervencao = data.items;
  }

  async obterStatusServicos() {
    let params: StatusServicoParameters = {
      indAtivo: 1,
      sortActive: 'nomeStatusServico',
      sortDirection: 'asc',
      pageSize: 50
    };

    const data = await this._statusServicoService
      .obterPorParametros(params)
      .toPromise();

    this.statusServicos = data.items;
  }

  async obterClientes(filter: string = '') {
    let params: ClienteParameters = {
      filter: filter,
      indAtivo: 1,
      sortActive: 'nomeFantasia',
      sortDirection: 'asc',
      pageSize: 300
    };

    const data = await this._clienteService
      .obterPorParametros(params)
      .toPromise();

    this.clientes = data.items;
  }

  aplicar(): void {
    const form: any = this.form.getRawValue();

    const filtro: any = {
      nome: 'dashboard',
      parametros: form
    }

    if (this.periodoValido(form.dataInicio, form.dataFim)) {
      this._userService.registrarFiltro(filtro);
      this.sidenav.close();
    }
  }

  private periodoValido(dataInicio: string, dataFim: string): boolean {
    var periodo = moment.duration(moment(dataFim).diff(moment(dataInicio)));
    var meses = periodo.asMonths();

    if (meses > 1) {
      this._snack.exibirToast('Excedido período máximo de dois meses', 'error');
      return false;
    }

    return true;
  }

  limpar(): void {
    this.form.reset();
    this._userService.removerFiltro('dashboard');
    this.filtro = this._userService.obterFiltro('dashboard');
    this.sidenav.close();
  }
}

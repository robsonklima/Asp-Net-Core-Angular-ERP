import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { UsuarioLoginService } from 'app/core/services/usuario-login.service';
import { UsuarioService } from 'app/core/services/usuario.service';
import { statusConst } from 'app/core/types/status-types';
import { UsuarioLogin, UsuarioLoginData, UsuarioLoginParameters } from 'app/core/types/usuario-login.types';
import { Usuario, UsuarioData, UsuarioParameters } from 'app/core/types/usuario.types';
import Enumerable from 'linq';
import _ from 'lodash';
import moment from 'moment';
import { interval, Subject } from 'rxjs';
import { startWith, takeUntil } from 'rxjs/operators';
import { UtilizacaoUsuariosDialogComponent } from './utilizacao-usuarios-dialog/utilizacao-usuarios-dialog.component';

@Component({
  selector: 'app-utilizacao',
  templateUrl: './utilizacao.component.html'
})
export class UtilizacaoComponent implements OnInit {
  usuarios: Usuario[] = [];
  acessos: UsuarioLogin[] = [];
  acessosPorData: any[] = [];
  usuariosAtivos: Usuario[] = [];
  usuariosOnline: Usuario[] = [];
  perfisAtivos: any;
  perfisOnline: any;
  loading: boolean = true;
  protected _onDestroy = new Subject<void>();

  constructor(
    private _usuarioService: UsuarioService,
    private _usuarioLoginService: UsuarioLoginService,
    private _dialog: MatDialog
  ) { }

  async ngOnInit() {
    interval(3 * 60 * 1000)
			.pipe(
				startWith(0),
				takeUntil(this._onDestroy)
			)
			.subscribe(() => {
				this.obterDados();
			});
  }

  private async obterDados() {
    this.usuarios = (await this.obterUsuarios()).items;
    this.usuariosOnline = this.obterUsuariosOnline();
    this.obterUsuariosAtivos();
    this.obterPerfis();
    this.acessos = (await this.obterAcessos()).items;
    this.obterAcessosPorData();
    this.loading = false;
  }

  obterPerfis() {
    this.perfisAtivos = _(this.usuariosAtivos)
      .groupBy(usuario => usuario?.perfil?.nomePerfil)
      .map((value, key) => ({nomePerfil: key, usuarios: value}))
      .value();

    this.perfisOnline = _(this.usuariosOnline)
      .groupBy(usuario => usuario?.perfil?.nomePerfil)
      .map((value, key) => ({nomePerfil: key, usuarios: value}))
      .value();
  }

  obterAcessosPorData() {
    this.acessosPorData = _(this.acessos)
      .groupBy(acesso => moment(acesso?.dataHoraCad).format('DD/MM/YY'))
      .map((value, key) => ({ data: key, acessos: value }))
      .value();
  }

  async obterUsuarios(): Promise<UsuarioData> {
		let params: UsuarioParameters = {
			indAtivo: statusConst.ATIVO,
			sortActive: 'nomeUsuario',
			sortDirection: 'asc',
      codPerfisNotIn: "34,81,87,90,93,97,98",
      ultimoAcessoInicio: moment().subtract(1, 'year').format('YYYY-MM-DD HH:mm:ss')
		};

		return await this._usuarioService
			.obterPorParametros(params)
			.toPromise();
	}

  async obterAcessos(): Promise<UsuarioLoginData> {
		let params: UsuarioLoginParameters = {
      dataHoraCadInicio: moment().subtract(1, 'month').format('YYYY-MM-DD HH:mm:ss')
		};

		return await this._usuarioLoginService
			.obterPorParametros(params)
			.toPromise();
	}

  obterUsuariosAtivos(parametro: number=-365) {
    const dataFiltro = moment().add('days', parametro);

    this.usuariosAtivos = Enumerable
          .from(this.usuarios)
          .where(u => moment(u.ultimoAcesso) >= dataFiltro)
          .toArray();

    this.obterPerfis();
  }

  obterUsuariosOnline(): Usuario[] {
    return Enumerable
      .from(this.usuarios)
      .where(u => moment(u.ultimoAcesso) >= moment()
      .add('hour', -6))
      .toArray();
  }

  public onUsuarios(usuarios: Usuario[]) {
    this._dialog.open(UtilizacaoUsuariosDialogComponent, {
      data: {
        usuarios: usuarios,
      },
    });
  }

  ngOnDestroy() {
		this._onDestroy.next();
		this._onDestroy.complete();
	}
}
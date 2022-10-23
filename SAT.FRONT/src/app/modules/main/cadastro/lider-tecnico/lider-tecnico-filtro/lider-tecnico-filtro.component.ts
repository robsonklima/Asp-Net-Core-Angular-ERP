import { UserService } from '../../../../../core/user/user.service';
import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormControl } from '@angular/forms';
import { MatSidenav } from '@angular/material/sidenav';
import { Subject } from 'rxjs';
import { FilterBase } from '../../../../../core/filters/filter-base';
import { IFilterBase } from '../../../../../core/types/filtro.types';
import { takeUntil, debounceTime, distinctUntilChanged } from 'rxjs/operators';
import { Tecnico, TecnicoParameters } from 'app/core/types/tecnico.types';
import { TecnicoService } from 'app/core/services/tecnico.service';
import { Usuario, UsuarioParameters } from 'app/core/types/usuario.types';
import { UsuarioService } from 'app/core/services/usuario.service';
import { statusConst } from 'app/core/types/status-types';
import { PerfilEnum } from 'app/core/types/perfil.types';


@Component({
	selector: 'app-lider-tecnico-filtro',
	templateUrl: './lider-tecnico-filtro.component.html'
})
export class LiderTecnicoFiltroComponent extends FilterBase implements OnInit, IFilterBase {

	@Input() sidenav: MatSidenav;

	tecnicos: Tecnico[] = [];
	tecnicoFilterCtrl: FormControl = new FormControl();
	usuarios: Usuario[] = [];
	usuarioFilterCtrl: FormControl = new FormControl();
	
	
	protected _onDestroy = new Subject<void>();

	constructor(
		private _tecnicoService: TecnicoService,
		private _usuarioService: UsuarioService,
		protected _userService: UserService,
		protected _formBuilder: FormBuilder
	) {
		super(_userService, _formBuilder, 'lider-tecnico');
	}

	ngOnInit(): void {
		this.createForm();
		this.loadData();
	}

	async loadData() {
		this.obterUsuarios();
		this.obterTecnicos();
;		this.registrarEmitters();
	}

	createForm(): void {
		this.form = this._formBuilder.group({
			codUsuarioLideres: [undefined],
			codTecnicos: [undefined],
			codUsuarios: [undefined]
		});
		this.form.patchValue(this.filter?.parametros);
	}

	async obterUsuarios(filtro: string = '') {
		let params: UsuarioParameters = {
			codPerfis: PerfilEnum.FILIAL_LIDER_C_FUNCOES_COORDENADOR.toString(),
			indAtivo: statusConst.ATIVO,
			filter: filtro,
			sortActive: 'nomeUsuario',
			sortDirection: 'asc',
			pageSize: 1000
		};
		const data = await this._usuarioService
			.obterPorParametros(params)
			.toPromise();
		this.usuarios = data.items;
	}

	async obterTecnicos(filtro: string = '') {
		let params: TecnicoParameters = {
			filter: filtro,
			sortActive: 'nome',
			sortDirection: 'asc',
			pageSize: 1000
		};
		const data = await this._tecnicoService
			.obterPorParametros(params)
			.toPromise();
		this.tecnicos = data.items;
	}


	private registrarEmitters() {

		this.usuarioFilterCtrl.valueChanges
			.pipe(
				takeUntil(this._onDestroy),
				debounceTime(700),
				distinctUntilChanged()
			)
			.subscribe(() => {
				this.obterUsuarios(this.usuarioFilterCtrl.value);
			});
		this.tecnicoFilterCtrl.valueChanges
			.pipe(
				takeUntil(this._onDestroy),
				debounceTime(700),
				distinctUntilChanged()
			)
			.subscribe(() => {
				this.obterTecnicos(this.tecnicoFilterCtrl.value);
			});
	}

	ngOnDestroy() {
		this._onDestroy.next();
		this._onDestroy.complete();
	}
}
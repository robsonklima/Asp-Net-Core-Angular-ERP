import { UserService } from '../../../../../core/user/user.service';
import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormControl } from '@angular/forms';
import { MatSidenav } from '@angular/material/sidenav';
import { Subject } from 'rxjs';
import { FilterBase } from '../../../../../core/filters/filter-base';
import { IFilterBase } from '../../../../../core/types/filtro.types';
import { takeUntil, debounceTime, distinctUntilChanged } from 'rxjs/operators';
import { Cargo, CargoParameters } from 'app/core/types/cargo.types';
import { Perfil, PerfilParameters } from 'app/core/types/perfil.types';
import { CargoService } from 'app/core/services/cargo.service';
import { PerfilService } from 'app/core/services/perfil.service';


@Component({
	selector: 'app-usuario-filtro',
	templateUrl: './usuario-filtro.component.html'
})
export class UsuarioFiltroComponent extends FilterBase implements OnInit, IFilterBase {

	@Input() sidenav: MatSidenav;

	cargos: Cargo[] = [];
	cargoFilterCtrl: FormControl = new FormControl();

	protected _onDestroy = new Subject<void>();

	constructor(
		private _cargoService: CargoService,
		protected _userService: UserService,
		protected _formBuilder: FormBuilder
	) {
		super(_userService, _formBuilder, 'usuario');
	}

	ngOnInit(): void {
		this.createForm();
		this.loadData();
	}

	async loadData() {
		this.obterCargos();
		this.registrarEmitters();
	}

	createForm(): void {
		this.form = this._formBuilder.group({
			indAtivo: [undefined],
			codCargos: [undefined]

		});
		this.form.patchValue(this.filter?.parametros);
	}

	async obterCargos(filtro: string = '') {
		let params: CargoParameters = {
			filter: filtro,
			sortActive: 'nomeCargo',
			sortDirection: 'asc',
			pageSize: 1000
		};
		const data = await this._cargoService
			.obterPorParametros(params)
			.toPromise();
		this.cargos = data.items;
	}

	private registrarEmitters() {

		this.cargoFilterCtrl.valueChanges
			.pipe(
				takeUntil(this._onDestroy),
				debounceTime(700),
				distinctUntilChanged()
			)
			.subscribe(() => {
				this.obterCargos(this.cargoFilterCtrl.value);
			});

	}

	ngOnDestroy() {
		this._onDestroy.next();
		this._onDestroy.complete();
	}
}
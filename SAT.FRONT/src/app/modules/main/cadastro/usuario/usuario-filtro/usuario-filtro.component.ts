import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormControl } from '@angular/forms';
import { MatSidenav } from '@angular/material/sidenav';
import { CargoService } from 'app/core/services/cargo.service';
import { Cargo, CargoParameters } from 'app/core/types/cargo.types';
import { Subject } from 'rxjs';
import { debounceTime, distinctUntilChanged, takeUntil } from 'rxjs/operators';
import { FilterBase } from '../../../../../core/filters/filter-base';
import { IFilterBase } from '../../../../../core/types/filtro.types';
import { UserService } from '../../../../../core/user/user.service';

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
		const params: CargoParameters = {
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
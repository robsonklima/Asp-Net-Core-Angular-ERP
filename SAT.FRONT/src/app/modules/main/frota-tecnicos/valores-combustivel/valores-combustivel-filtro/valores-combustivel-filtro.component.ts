import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormControl } from '@angular/forms';
import { MatSidenav } from '@angular/material/sidenav';
import { FilterBase } from 'app/core/filters/filter-base';
import { FilialService } from 'app/core/services/filial.service';
import { UnidadeFederativaService } from 'app/core/services/unidade-federativa.service';
import { Filial, FilialParameters } from 'app/core/types/filial.types';
import { IFilterBase } from 'app/core/types/filtro.types';
import { UnidadeFederativa, UnidadeFederativaParameters } from 'app/core/types/unidade-federativa.types';
import { UserService } from 'app/core/user/user.service';
import { Subject } from 'rxjs';
import { takeUntil, debounceTime, distinctUntilChanged } from 'rxjs/operators';

@Component({
	selector: 'app-valores-combustivel-filtro',
	templateUrl: './valores-combustivel-filtro.component.html'
})
export class ValoresCombustivelFiltroComponent extends FilterBase implements OnInit, IFilterBase {

	@Input() sidenav: MatSidenav;

	unidadesFederativas: UnidadeFederativa[] = [];
	unidadeFederativaFilterCtrl: FormControl = new FormControl();
	filiais: Filial[] = [];
	filialFilterCtrl: FormControl = new FormControl();

	protected _onDestroy = new Subject<void>();

	constructor(
		private _unidadeFederativaService: UnidadeFederativaService,
		private _filialService: FilialService,
		protected _userService: UserService,
		protected _formBuilder: FormBuilder
	) {
		super(_userService, _formBuilder, '');

	}

	ngOnInit(): void {
		this.createForm();
		this.loadData();
	}

	async loadData() {
		this.obterUFs();
		this.obterFiliais();
		this.registrarEmitters();
	}

	createForm(): void {
		this.form = this._formBuilder.group({
			codUFs: [undefined],
			codFiliais: [undefined]
		});
		this.form.patchValue(this.filter?.parametros);
	}


	async obterUFs(filtro: string = '') {
		let params: UnidadeFederativaParameters = {
			filter: filtro,
			sortActive: 'siglaUF',
			sortDirection: 'asc',
			codPais: 1,
			pageSize: 1000
		};
		const data = await this._unidadeFederativaService
			.obterPorParametros(params)
			.toPromise();
		this.unidadesFederativas = data.items;		
	}

	async obterFiliais(filtro: string = '') {
		let params: FilialParameters = {
			filter: filtro,
			sortActive: 'nomeFilial',
			sortDirection: 'asc',
			pageSize: 1000
		};
		const data = await this._filialService
			.obterPorParametros(params)
			.toPromise();
		this.filiais = data.items;		
	}


	private registrarEmitters() {

		this.unidadeFederativaFilterCtrl.valueChanges
			.pipe(
				takeUntil(this._onDestroy),
				debounceTime(700),
				distinctUntilChanged()
			)
			.subscribe(() => {
				this.obterUFs(this.unidadeFederativaFilterCtrl.value);
			});

		this.filialFilterCtrl.valueChanges
			.pipe(
				takeUntil(this._onDestroy),
				debounceTime(700),
				distinctUntilChanged()
			)
			.subscribe(() => {
				this.obterFiliais(this.filialFilterCtrl.value);
			});
	}

	ngOnDestroy() {
		this._onDestroy.next();
		this._onDestroy.complete();
	}

}

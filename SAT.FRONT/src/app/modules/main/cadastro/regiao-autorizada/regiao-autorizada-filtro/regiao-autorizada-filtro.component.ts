import { UserService } from '../../../../../core/user/user.service';
import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormControl } from '@angular/forms';
import { MatSidenav } from '@angular/material/sidenav';
import { Subject } from 'rxjs';
import { FilterBase } from '../../../../../core/filters/filter-base';
import { IFilterBase } from '../../../../../core/types/filtro.types';
import { takeUntil, debounceTime, distinctUntilChanged } from 'rxjs/operators';
import { FilialService } from 'app/core/services/filial.service';
import { Filial, FilialParameters } from 'app/core/types/filial.types';
import { Autorizada, AutorizadaParameters } from 'app/core/types/autorizada.types';
import { Cidade, CidadeParameters } from 'app/core/types/cidade.types';
import { AutorizadaService } from 'app/core/services/autorizada.service';
import { CidadeService } from 'app/core/services/cidade.service';
import { statusConst } from 'app/core/types/status-types';


@Component({
	selector: 'app-regiao-autorizada-filtro',
	templateUrl: './regiao-autorizada-filtro.component.html'
})
export class RegiaoAutorizadaFiltroComponent extends FilterBase implements OnInit, IFilterBase {

	@Input() sidenav: MatSidenav;

	autorizadas: Autorizada[] = [];
	autorizadaFilterCtrl: FormControl = new FormControl();
	cidades: Cidade[] = [];
	cidadeFilterCtrl: FormControl = new FormControl();
	filiais: Filial[] = [];
	filialFilterCtrl: FormControl = new FormControl();

	protected _onDestroy = new Subject<void>();

	constructor(
		private _autorizadaService: AutorizadaService,
		private _cidadeService: CidadeService,
		private _filialService: FilialService,
		protected _userService: UserService,
		protected _formBuilder: FormBuilder
	) {
		super(_userService, _formBuilder, 'autorizada');
	}

	ngOnInit(): void {
		this.createForm();
		this.loadData();
	}

	async loadData() {
		this.obterFiliais();
		this.registrarEmitters();
	}

	createForm(): void {
		this.form = this._formBuilder.group({
			indAtivo: [undefined],
			codAutorizadas: [undefined],
			codCidades: [undefined],
			codFiliais: [undefined]
		});
		this.form.patchValue(this.filter?.parametros);
	}

	async obterAutorizadas(filtro: string = '') {
		let params: AutorizadaParameters = {
			filter: filtro,
			sortActive: 'nomeFantasia',
			sortDirection: 'asc',
			pageSize: 1000
		};
		const data = await this._autorizadaService
			.obterPorParametros(params)
			.toPromise();
		this.
		autorizadas = data.items;
	}
	async obterCidades(filtro: string = '') {
		let params: CidadeParameters = {
			filter: filtro,
			indAtivo: statusConst.ATIVO,
			sortActive: 'nomeCidade',
			sortDirection: 'asc',
			pageSize: 500
		};
		const data = await this._cidadeService
			.obterPorParametros(params)
			.toPromise();
		this.cidades = data.items;
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

		this.autorizadaFilterCtrl.valueChanges
			.pipe(
				takeUntil(this._onDestroy),
				debounceTime(700),
				distinctUntilChanged()
			)
			.subscribe(() => {
				this.obterAutorizadas(this.autorizadaFilterCtrl.value);
			});

		this.cidadeFilterCtrl.valueChanges
			.pipe(
				takeUntil(this._onDestroy),
				debounceTime(700),
				distinctUntilChanged()
			)
			.subscribe(() => {
				this.obterCidades(this.cidadeFilterCtrl.value);
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
import { UserService } from '../../../../../core/user/user.service';
import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormControl } from '@angular/forms';
import { MatSidenav } from '@angular/material/sidenav';
import { Subject } from 'rxjs';
import { FilterBase } from '../../../../../core/filters/filter-base';
import { IFilterBase } from '../../../../../core/types/filtro.types';
import { takeUntil, debounceTime, distinctUntilChanged } from 'rxjs/operators';
import { Feriado, FeriadoParameters } from 'app/core/types/feriado.types';
import { Cidade, CidadeParameters } from 'app/core/types/cidade.types';
import { UnidadeFederativa, UnidadeFederativaParameters } from 'app/core/types/unidade-federativa.types';
import { FeriadoService } from 'app/core/services/feriado.service';
import { UnidadeFederativaService } from 'app/core/services/unidade-federativa.service';
import { CidadeService } from 'app/core/services/cidade.service';
import Enumerable from 'linq';
import { statusConst } from 'app/core/types/status-types';


@Component({
	selector: 'app-feriado-filtro',
	templateUrl: './feriado-filtro.component.html'
})
export class FeriadoFiltroComponent extends FilterBase implements OnInit, IFilterBase {

	@Input() sidenav: MatSidenav;

	feriados: Feriado[] = [];
	feriadoFilterCtrl: FormControl = new FormControl();
	cidades: Cidade[] = [];
	ufs: UnidadeFederativa[] = [];
	
	
	protected _onDestroy = new Subject<void>();

	constructor(

		private _feriadoService: FeriadoService,
		private _unidadeFederativaService: UnidadeFederativaService,
		private _cidadeService: CidadeService,
		protected _userService: UserService,
		protected _formBuilder: FormBuilder
	) {
		super(_userService, _formBuilder, 'feriado');
	}

	ngOnInit(): void {
		this.createForm();
		this.loadData();
	}

	async loadData() {
		this.obterFeriados();
		this.obterCidades(); 
		this.obterUfs(); 
		this.registrarEmitters();
	}

	createForm(): void {
		this.form = this._formBuilder.group({
			indAtivo: [undefined],
			feriados: [undefined],
			codCidades: [undefined],
			codUfs: [undefined]
		});
		this.form.patchValue(this.filter?.parametros);
	}


	async obterFeriados(filtro: string = '') {
		let params: FeriadoParameters = {
			filter: filtro,
			sortActive: 'nomeFeriado',
			sortDirection: 'asc',
			pageSize: 1000
		};
		const data = await this._feriadoService
			.obterPorParametros(params)
			.toPromise();
		this.feriados = data.items;
	}

	async obterCidades(ufFilter: any = '') {
		let params: CidadeParameters = {
			indAtivo: statusConst.ATIVO,
			codUF: ufFilter,
			pageSize: 500
		};

		const data = await this._cidadeService
			.obterPorParametros(params)
			.toPromise();

		this.cidades = Enumerable.from(data.items).orderBy(i => i.nomeCidade).toArray();
	}

	async obterUfs() {
		let params: UnidadeFederativaParameters = {
			pageSize: 1000
		};

		const data = await this._unidadeFederativaService
			.obterPorParametros(params)
			.toPromise();

		this.ufs = Enumerable.from(data.items).orderBy(i => i.nomeUF).toArray();
	}


	private registrarEmitters() {

		this.feriadoFilterCtrl.valueChanges
			.pipe(
				takeUntil(this._onDestroy),
				debounceTime(700),
				distinctUntilChanged()
			)
			.subscribe(() => {
				this.obterFeriados(this.feriadoFilterCtrl.value);
			});

		this.form.controls['codUfs'].valueChanges
			.pipe(
				takeUntil(this._onDestroy),
				debounceTime(700),
				distinctUntilChanged()
			)
			.subscribe(() => {
				this.obterCidades(this.form.controls['codUfs'].value);
			});
	}

	ngOnDestroy() {
		this._onDestroy.next();
		this._onDestroy.complete();
	}
}
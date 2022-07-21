import { UserService } from '../../../../../core/user/user.service';
import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormControl } from '@angular/forms';
import { MatSidenav } from '@angular/material/sidenav';
import { Subject } from 'rxjs';
import { FilterBase } from '../../../../../core/filters/filter-base';
import { IFilterBase } from '../../../../../core/types/filtro.types';
import { takeUntil, debounceTime, distinctUntilChanged } from 'rxjs/operators';
import { Filial, FilialParameters } from 'app/core/types/filial.types';
import { FilialService } from 'app/core/services/filial.service';
import { UnidadeFederativa, UnidadeFederativaParameters } from 'app/core/types/unidade-federativa.types';
import { UnidadeFederativaService } from 'app/core/services/unidade-federativa.service';



@Component({
	selector: 'app-cidade-filtro',
	templateUrl: './cidade-filtro.component.html'
})
export class CidadeFiltroComponent extends FilterBase implements OnInit, IFilterBase {
	
	@Input() sidenav: MatSidenav;
	filiais: Filial[] = [];
	filialFilterCtrl: FormControl = new FormControl();
	ufs: UnidadeFederativa[] = [];	
	ufFilterCtrl: FormControl = new FormControl();
	protected _onDestroy = new Subject<void>();

	constructor(
		private _filialService: FilialService,
		private _uniaoFederaticaService: UnidadeFederativaService,
		protected _userService: UserService,
		protected _formBuilder: FormBuilder
	) {
		super(_userService, _formBuilder, 'cidade');
	}

	ngOnInit(): void {
		this.createForm();
		this.loadData();
		
	}

	async loadData() {
		this.obterFiliais();
		this.obterUFs();
		this.registrarEmitters();
	}

	createForm(): void {
		this.form = this._formBuilder.group({
			 codFiliais:[undefined],
			 codUFs:[undefined],
			 indAtivo: [undefined],
			 
			

		});
		this.form.patchValue(this.filter?.parametros);
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

	async obterUFs(filtro: string = '') {
		let params: UnidadeFederativaParameters = {
			filter: filtro,
			sortActive: 'nomeUf',
			sortDirection: 'asc',
			pageSize: 1000
		};
		const data = await this._uniaoFederaticaService
			.obterPorParametros(params)
			.toPromise();
		this.ufs = data.items;
	}




	private registrarEmitters() {
		this.filialFilterCtrl.valueChanges
		.pipe(
			takeUntil(this._onDestroy),
			debounceTime(700),
			distinctUntilChanged()
		)
		.subscribe(() => {
			this.obterFiliais(this.filialFilterCtrl.value);
		});
		
		this.ufFilterCtrl.valueChanges
		.pipe(
			takeUntil(this._onDestroy),
			debounceTime(700),
			distinctUntilChanged()
		)
		.subscribe(() => {
			this.obterUFs(this.ufFilterCtrl.value);
		});


	}

	limpar() {
		super.limpar();

	}

	ngOnDestroy() {
		this._onDestroy.next();
		this._onDestroy.complete();
	}
}
import { UserService } from '../../../../../core/user/user.service';
import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormControl } from '@angular/forms';
import { MatSidenav } from '@angular/material/sidenav';
import { Subject } from 'rxjs';
import { FilterBase } from '../../../../../core/filters/filter-base';
import { IFilterBase } from '../../../../../core/types/filtro.types';
import { Acao, AcaoParameters } from 'app/core/types/acao.types';
import { AcaoService } from 'app/core/services/acao.service';
import { Causa, CausaParameters } from 'app/core/types/causa.types';
import { CausaService } from 'app/core/services/causa.service';
import { takeUntil, debounceTime, distinctUntilChanged } from 'rxjs/operators';



@Component({
	selector: 'app-acao-causa-filtro',
	templateUrl: './acao-causa-filtro.component.html'
})
export class AcaoCausaFiltroComponent extends FilterBase implements OnInit, IFilterBase {
	
	@Input() sidenav: MatSidenav;
	acoes: Acao[] = [];
	acaoFilterCtrl: FormControl = new FormControl();
	causas: Causa[] = [];
	causaFilterCtrl: FormControl = new FormControl();
	protected _onDestroy = new Subject<void>();

	constructor(
		private _acaoService: AcaoService,
		private _causaService: CausaService,
		protected _userService: UserService,
		protected _formBuilder: FormBuilder
	) {
		super(_userService, _formBuilder, 'acao-causa');
	}

	ngOnInit(): void {
		this.createForm();
		this.loadData();
		
	}

	async loadData() {
		this.obterCausas();
		this.obterAcoes();
		this.registrarEmitters();
	}

	createForm(): void {
		this.form = this._formBuilder.group({
			 codEAcoes:[undefined],
			 codECausas:[undefined],
			 nomeAcoes:[undefined],
			 nomeCausas:[undefined],
			

		});
		this.form.patchValue(this.filter?.parametros);
	}

	async obterCausas(filtro: string = '') {
		let params: CausaParameters = {
			filter: filtro,
			sortActive: 'nomeCausa',
			sortDirection: 'asc',
			pageSize: 1000
		};
		const data = await this._causaService
			.obterPorParametros(params)
			.toPromise();
		this.causas = data.items;
	}

	async obterAcoes(filtro: string = '') {
		let params: AcaoParameters = {
			filter: filtro,
			sortActive: 'nomeAcao',
			sortDirection: 'asc',
			pageSize: 1000
		};
		const data = await this._acaoService
			.obterPorParametros(params)
			.toPromise();
		this.acoes = data.items;
	}



	private registrarEmitters() {
		this.causaFilterCtrl.valueChanges
		.pipe(
			takeUntil(this._onDestroy),
			debounceTime(700),
			distinctUntilChanged()
		)
		.subscribe(() => {
			this.obterCausas(this.causaFilterCtrl.value);
		});

	this.acaoFilterCtrl.valueChanges
		.pipe(
			takeUntil(this._onDestroy),
			debounceTime(700),
			distinctUntilChanged()
		)
		.subscribe(() => {
			this.obterAcoes(this.acaoFilterCtrl.value);
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
import { UserService } from '../../../../../core/user/user.service';
import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormControl } from '@angular/forms';
import { MatSidenav } from '@angular/material/sidenav';
import { Subject } from 'rxjs';
import { FilterBase } from '../../../../../core/filters/filter-base';
import { IFilterBase } from '../../../../../core/types/filtro.types';
import { takeUntil, debounceTime, distinctUntilChanged } from 'rxjs/operators';
import { Regiao, RegiaoParameters } from 'app/core/types/regiao.types';
import { RegiaoService } from 'app/core/services/regiao.service';


@Component({
	selector: 'app-regiao-filtro',
	templateUrl: './regiao-filtro.component.html'
})
export class RegiaoFiltroComponent extends FilterBase implements OnInit, IFilterBase {

	@Input() sidenav: MatSidenav;

	regioes: Regiao[] = [];
	regiaoFilterCtrl: FormControl = new FormControl();
	
	protected _onDestroy = new Subject<void>();

	constructor(
		private _regiaoService: RegiaoService,
		protected _userService: UserService,
		protected _formBuilder: FormBuilder
	) {
		super(_userService, _formBuilder, 'regiao');
	}

	ngOnInit(): void {
		this.createForm();
		this.loadData();
	}

	async loadData() {
		this.obterRegioes();
		this.registrarEmitters();
	}

	createForm(): void {
		this.form = this._formBuilder.group({
			indAtivo: [undefined],
			codRegioes: [undefined]
		});
		this.form.patchValue(this.filter?.parametros);
	}


	async obterRegioes(filtro: string = '') {
		let params: RegiaoParameters = {
			filter: filtro,
			sortActive: 'nomeRegiao',
			sortDirection: 'asc',
			pageSize: 1000
		};
		const data = await this._regiaoService
			.obterPorParametros(params)
			.toPromise();
		this.regioes = data.items;		
	}


	private registrarEmitters() {

		this.regiaoFilterCtrl.valueChanges
			.pipe(
				takeUntil(this._onDestroy),
				debounceTime(700),
				distinctUntilChanged()
			)
			.subscribe(() => {
				this.obterRegioes(this.regiaoFilterCtrl.value);
			});
	}

	ngOnDestroy() {
		this._onDestroy.next();
		this._onDestroy.complete();
	}
}
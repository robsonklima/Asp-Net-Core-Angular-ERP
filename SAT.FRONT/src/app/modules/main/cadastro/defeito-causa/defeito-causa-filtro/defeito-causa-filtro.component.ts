import { UserService } from '../../../../../core/user/user.service';
import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormControl } from '@angular/forms';
import { MatSidenav } from '@angular/material/sidenav';
import { Subject } from 'rxjs';
import { FilterBase } from '../../../../../core/filters/filter-base';
import { IFilterBase } from '../../../../../core/types/filtro.types';
import { takeUntil, debounceTime, distinctUntilChanged } from 'rxjs/operators';
import { Causa, CausaParameters } from 'app/core/types/causa.types';
import { Defeito, DefeitoParameters } from 'app/core/types/defeito.types';
import { CausaService } from 'app/core/services/causa.service';
import { DefeitoService } from 'app/core/services/defeito.service';


@Component({
	selector: 'app-defeito-causa-filtro',
	templateUrl: './defeito-causa-filtro.component.html'
})
export class DefeitoCausaFiltroComponent extends FilterBase implements OnInit, IFilterBase {

	@Input() sidenav: MatSidenav;

	causas: Causa[] = [];
	causaFilterCtrl: FormControl = new FormControl();
	defeitos: Defeito[] = [];
	defeitoFilterCtrl: FormControl = new FormControl();
	protected _onDestroy = new Subject<void>();

	constructor(

		private _causaService: CausaService,
		private _defeitoService: DefeitoService,
		protected _userService: UserService,
		protected _formBuilder: FormBuilder
	) {
		super(_userService, _formBuilder, 'defeito-causa');
	}

	ngOnInit(): void {
		this.createForm();
		this.loadData();
		
	}

	async loadData() {
		this.obterCausas();
		this.obterDefeitos();
		this.registrarEmitters();
	}

	createForm(): void {
		this.form = this._formBuilder.group({
			codCausas: [undefined],
			codDefeitos: [undefined],
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

	async obterDefeitos(filtro: string = '') {
		let params: DefeitoParameters = {
			filter: filtro,
			sortActive: 'nomeDefeito',
			sortDirection: 'asc',
			pageSize: 1000
		};
		const data = await this._defeitoService
			.obterPorParametros(params)
			.toPromise();
		this.defeitos = data.items;
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

			this.defeitoFilterCtrl.valueChanges
			.pipe(
				takeUntil(this._onDestroy),
				debounceTime(700),
				distinctUntilChanged()
			)
			.subscribe(() => {
				this.obterDefeitos(this.defeitoFilterCtrl.value);
			});
		
	}

	ngOnDestroy() {
		this._onDestroy.next();
		this._onDestroy.complete();
	}
}
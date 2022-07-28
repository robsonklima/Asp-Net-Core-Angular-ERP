import { UserService } from '../../../../../core/user/user.service';
import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormControl } from '@angular/forms';
import { MatSidenav } from '@angular/material/sidenav';
import { Subject } from 'rxjs';
import { FilterBase } from '../../../../../core/filters/filter-base';
import { IFilterBase } from '../../../../../core/types/filtro.types';
import { takeUntil, debounceTime, distinctUntilChanged } from 'rxjs/operators';
import { PecaService } from 'app/core/services/peca.service';
import { Peca, PecaParameters, PecaStatus, PecaStatusParameters } from 'app/core/types/peca.types';
import { PecaStatusService } from 'app/core/services/peca-status.service';


@Component({
	selector: 'app-peca-filtro',
	templateUrl: './peca-filtro.component.html'
})
export class PecaFiltroComponent extends FilterBase implements OnInit, IFilterBase {

	@Input() sidenav: MatSidenav;

	pecas: Peca[] = [];
	pecaFilterCtrl: FormControl = new FormControl();
	status: PecaStatus[] = [];
    statusFilterCtrl: FormControl = new FormControl();
	
	
	protected _onDestroy = new Subject<void>();

	constructor(
		private _pecaStatusService: PecaStatusService,
		private _pecaService: PecaService,
		protected _userService: UserService,
		protected _formBuilder: FormBuilder
	) {
		super(_userService, _formBuilder, 'peca');
	}

	ngOnInit(): void {
		this.createForm();
		this.loadData();
	}

	async loadData() {
		this.obterPecas();
		this.obterStatus();
		this.registrarEmitters();
	}

	createForm(): void {
		this.form = this._formBuilder.group({
			CodPecaStatus: [undefined],
			codPecas: [undefined]
		});
		this.form.patchValue(this.filter?.parametros);
	}


	async obterPecas(filtro: string = '') {
		let params: PecaParameters = {
			filter: filtro,
			sortActive: 'nomePeca',
			sortDirection: 'asc',
			pageSize: 1000
		};
		const data = await this._pecaService
			.obterPorParametros(params)
			.toPromise();
		this.pecas = data.items;		
	}

	async obterStatus(filtro: string = '') {
		let params: PecaStatusParameters = {
			filter: filtro,
			sortActive: 'nomeStatus',
			sortDirection: 'asc',
			pageSize: 1000
		};
		const data = await this._pecaStatusService
			.obterPorParametros(params)
			.toPromise();
		this.status = data.items;
	}


	private registrarEmitters() {

		this.pecaFilterCtrl.valueChanges
			.pipe(
				takeUntil(this._onDestroy),
				debounceTime(700),
				distinctUntilChanged()
			)
			.subscribe(() => {
				this.obterPecas(this.pecaFilterCtrl.value);
			});

			this.statusFilterCtrl.valueChanges
			.pipe(
				takeUntil(this._onDestroy),
				debounceTime(700),
				distinctUntilChanged()
			)
			.subscribe(() => {
			
			this.obterStatus(this.statusFilterCtrl.value);
			});
	}

	ngOnDestroy() {
		this._onDestroy.next();
		this._onDestroy.complete();
	}
}
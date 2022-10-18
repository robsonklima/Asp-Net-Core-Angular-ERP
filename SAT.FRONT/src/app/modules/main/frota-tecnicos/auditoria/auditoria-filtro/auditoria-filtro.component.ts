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
import { AuditoriaStatus, AuditoriaStatusParameters } from 'app/core/types/auditoria-status.types';
import { AuditoriaStatusService } from 'app/core/services/auditoria-status.service';

@Component({
	selector: 'app-auditoria-filtro',
	templateUrl: './auditoria-filtro.component.html'
})
export class AuditoriaFiltroComponent extends FilterBase implements OnInit, IFilterBase {

	@Input() sidenav: MatSidenav;
	filiais: Filial[] = [];
	filialFilterCtrl: FormControl = new FormControl();
	stats: AuditoriaStatus[] = [];
	statsFilterCtrl: FormControl = new FormControl();
	protected _onDestroy = new Subject<void>();

	constructor(
		private _filialService: FilialService,
		private _auditoriaStatusService: AuditoriaStatusService,
		protected _userService: UserService,
		protected _formBuilder: FormBuilder
	) {
		super(_userService, _formBuilder, 'auditoria');
	}

	ngOnInit(): void {
		this.createForm();
		this.loadData();

	}

	async loadData() {
		this.obterFiliais();
		this.obterAuditoriaStatus();
		this.registrarEmitters();
	}

	createForm(): void {
		this.form = this._formBuilder.group({
			codFiliais: [undefined],
			codAuditoriaStats: [undefined],
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

	async obterAuditoriaStatus(filtro: string = '') {
		let params: AuditoriaStatusParameters = {
			filter: filtro,
			sortActive: 'nome',
			sortDirection: 'asc',
			pageSize: 1000
		};
		const data = await this._auditoriaStatusService
			.obterPorParametros(params)
			.toPromise();
		this.stats = data.items;
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

		this.statsFilterCtrl.valueChanges
			.pipe(
				takeUntil(this._onDestroy),
				debounceTime(700),
				distinctUntilChanged()
			)
			.subscribe(() => {
				this.obterAuditoriaStatus(this.statsFilterCtrl.value);
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
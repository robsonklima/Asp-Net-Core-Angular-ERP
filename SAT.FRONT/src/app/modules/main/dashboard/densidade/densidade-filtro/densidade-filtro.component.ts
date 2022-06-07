import { Regiao } from 'app/core/types/regiao.types';
import { FilialService } from 'app/core/services/filial.service';
import { Filial, FilialParameters } from 'app/core/types/filial.types';
import { statusConst } from '../../../../../core/types/status-types';
import { UserService } from '../../../../../core/user/user.service';
import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { MatSidenav } from '@angular/material/sidenav';
import { Subject } from 'rxjs';
import { FilterBase } from '../../../../../core/filters/filter-base';
import { IFilterBase } from '../../../../../core/types/filtro.types';
import { RegiaoAutorizadaService } from 'app/core/services/regiao-autorizada.service';
import { RegiaoAutorizadaParameters } from 'app/core/types/regiao-autorizada.types';
import Enumerable from 'linq';
import { Autorizada, AutorizadaParameters } from 'app/core/types/autorizada.types';
import { AutorizadaService } from 'app/core/services/autorizada.service';
import { takeUntil, debounceTime, distinctUntilChanged } from 'rxjs/operators';


@Component({
	selector: 'app-densidade-filtro',
	templateUrl: './densidade-filtro.component.html'
})
export class DensidadeFiltroComponent extends FilterBase implements OnInit, IFilterBase {

	@Input() sidenav: MatSidenav;

	filiais: Filial[] = [];
	regioes: Regiao[] = [];
	autorizadas: Autorizada[] = [];
	protected _onDestroy = new Subject<void>();

	constructor(
		private _filialService: FilialService,
		private _regiaoAutorizadaService: RegiaoAutorizadaService,
		private _autorizadaService: AutorizadaService,
		protected _userService: UserService,
		protected _formBuilder: FormBuilder
	) {
		super(_userService, _formBuilder, 'dashboard-densidade');
	}

	ngOnInit(): void {
		this.createForm();
		this.loadData();

		this.form.controls['codFilial'].valueChanges
		.pipe(
			takeUntil(this._onDestroy),
			debounceTime(700),
			distinctUntilChanged()
		)
		.subscribe(() => {
			this.obterRegioesAutorizadas(this.form.controls['codFilial'].value);
			this.obterAutorizadas(this.form.controls['codFilial'].value);		});
	}

	async loadData() {
		this.obterFiliais();
		this.obterRegioesAutorizadas(this.form.controls['codFilial'].value);
		this.obterAutorizadas(this.form.controls['codFilial'].value);
	}

	createForm(): void {
		this.form = this._formBuilder.group({
			codFilial: [undefined],
			codRegiao: [undefined],
			codAutorizada: [undefined],
		});
		this.form.patchValue(this.filter);
	}

	async obterFiliais() {
		let params: FilialParameters = {
			indAtivo: statusConst.ATIVO,
			sortActive: 'nomeFilial',
			sortDirection: 'asc'
		};

		const data = await this._filialService
			.obterPorParametros(params)
			.toPromise();

		this.filiais = data.items;
	}

	async obterRegioesAutorizadas(filialFilter: any = '') {
		let params: RegiaoAutorizadaParameters = {
			indAtivo: statusConst.ATIVO,
			codFiliais: filialFilter,
			pageSize: 1000
		};

		const data = await this._regiaoAutorizadaService
			.obterPorParametros(params)
			.toPromise();

		this.regioes = Enumerable.from(data.items).where(ra => ra.regiao?.indAtivo == 1).select(ra => ra.regiao).distinct(r => r.codRegiao).orderBy(i => i.nomeRegiao).toArray();
	}

	async obterAutorizadas(filialFilter: any = '') {
		let params: AutorizadaParameters = {
			indAtivo: statusConst.ATIVO,
			codFiliais: filialFilter,
			pageSize: 1000
		};

		const data = await this._autorizadaService
			.obterPorParametros(params)
			.toPromise();

		this.autorizadas = Enumerable.from(data.items).orderBy(i => i.nomeFantasia).toArray();
	}

	limpar() {
		super.limpar();

		if (this.userSession?.usuario?.codFilial) {
			this.form.controls['codFiliais'].setValue([this.userSession.usuario.codFilial]);
			this.form.controls['codFiliais'].disable();
		}
	}

	ngOnDestroy() {
		this._onDestroy.next();
		this._onDestroy.complete();
	}
}
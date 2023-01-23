import { UserService } from '../../../../../core/user/user.service';
import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormControl } from '@angular/forms';
import { MatSidenav } from '@angular/material/sidenav';
import { Subject } from 'rxjs';
import { FilterBase } from '../../../../../core/filters/filter-base';
import { IFilterBase } from '../../../../../core/types/filtro.types';
import { takeUntil, debounceTime, distinctUntilChanged } from 'rxjs/operators';
import { FilialService } from 'app/core/services/filial.service';
import { AutorizadaService } from 'app/core/services/autorizada.service';
import { RegiaoService } from 'app/core/services/regiao.service';
import { Filial, FilialParameters } from 'app/core/types/filial.types';
import { Autorizada, AutorizadaParameters } from 'app/core/types/autorizada.types';
import { Regiao, RegiaoParameters } from 'app/core/types/regiao.types';
import { RegiaoAutorizadaParameters } from 'app/core/types/regiao-autorizada.types';
import { statusConst } from 'app/core/types/status-types';
import Enumerable from 'linq';
import { RegiaoAutorizadaService } from 'app/core/services/regiao-autorizada.service';

@Component({
	selector: 'app-tecnico-filtro',
	templateUrl: './tecnico-filtro.component.html'
})
export class TecnicoFiltroComponent extends FilterBase implements OnInit, IFilterBase {
	@Input() sidenav: MatSidenav;

	filiais: Filial[] = [];
	filialFilterCtrl: FormControl = new FormControl();
	autorizadas: Autorizada[] = [];
	autorizadaFilterCtrl: FormControl = new FormControl();
	regioes: Regiao[] = [];
	regiaoFilterCtrl: FormControl = new FormControl();

	protected _onDestroy = new Subject<void>();

	constructor(
		private _filialService: FilialService,
		private _autorizadaService: AutorizadaService,
		private _regiaoAutorizadaService: RegiaoAutorizadaService,
		protected _userService: UserService,
		protected _formBuilder: FormBuilder
	) {
		super(_userService, _formBuilder, 'tecnico');
	}

	ngOnInit(): void {
		this.createForm();
		this.loadData();
	}

	async loadData() {
		this.obterFiliais();
		this.obterAutorizadas();
		this.obterRegioesAutorizadas();
		this.registrarEmitters();
		this.aoSelecionarFilial();
	}

	createForm(): void {
		this.form = this._formBuilder.group({
			indAtivo: [undefined],
			indFerias: [undefined],
			codFiliais: [undefined],
			codAutorizadas: [undefined],
			codRegioes: [undefined]
		});
		this.form.patchValue(this.filter?.parametros);
	}

	aoSelecionarFilial() {
		this.form.controls['codFiliais']
			.valueChanges
			.subscribe(() => {
				if ((this.form.controls['codFiliais'].value && this.form.controls['codFiliais'].value != '')) {
					var filialFilter: any = this.form.controls['codFiliais'].value;

					this.obterRegioesAutorizadas(filialFilter);
					this.obterAutorizadas(filialFilter);

					this.form.controls['codRegioes'].enable();
					this.form.controls['codAutorizadas'].enable();
				}
				else {
					this.form.controls['codRegioes'].disable();
					this.form.controls['codAutorizadas'].disable();
				}
			});

		if (this.userSession.usuario.codFilial) {
			this.form.controls['codFiliais'].setValue([this.userSession.usuario.codFilial]);
			this.form.controls['codFiliais'].disable();
		}
		else {
			this.form.controls['codFiliais'].enable();
		}
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


	private registrarEmitters() {

		this.form.controls['codFiliais'].valueChanges
			.pipe(
				takeUntil(this._onDestroy),
				debounceTime(700),
				distinctUntilChanged()
			)
			.subscribe(() => {				
				this.obterAutorizadas(this.form.controls['codFiliais'].value);
				this.obterRegioesAutorizadas(this.form.controls['codFiliais'].value);
			});
	}

	ngOnDestroy() {
		this._onDestroy.next();
		this._onDestroy.complete();
	}
}
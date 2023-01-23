import { UserService } from '../../../../../core/user/user.service';
import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormControl } from '@angular/forms';
import { MatSidenav } from '@angular/material/sidenav';
import { Subject } from 'rxjs';
import { FilterBase } from '../../../../../core/filters/filter-base';
import { IFilterBase } from '../../../../../core/types/filtro.types';
import { takeUntil, debounceTime, distinctUntilChanged } from 'rxjs/operators';
import { statusConst } from 'app/core/types/status-types';
import { Cliente, ClienteParameters } from 'app/core/types/cliente.types';
import { ClienteService } from 'app/core/services/cliente.service';
import { FilialService } from 'app/core/services/filial.service';
import { AutorizadaService } from 'app/core/services/autorizada.service';
import { Filial, FilialParameters } from 'app/core/types/filial.types';
import { Autorizada, AutorizadaParameters } from 'app/core/types/autorizada.types';
import { Regiao } from 'app/core/types/regiao.types';
import { RegiaoAutorizadaParameters } from 'app/core/types/regiao-autorizada.types';
import Enumerable from 'linq';
import { RegiaoAutorizadaService } from 'app/core/services/regiao-autorizada.service';


@Component({
	selector: 'app-local-atendimento-filtro',
	templateUrl: './local-atendimento-filtro.component.html'
})
export class LocalAtendimentoFiltroComponent extends FilterBase implements OnInit, IFilterBase {

	@Input() sidenav: MatSidenav;

	clientes: Cliente[] = [];
	clienteFilterCtrl: FormControl = new FormControl();
	filiais: Filial[] = [];
	filialFilterCtrl: FormControl = new FormControl();
	autorizadas: Autorizada[] = [];
	autorizadaFilterCtrl: FormControl = new FormControl();
	regioes: Regiao[] = [];
	regiaoFilterCtrl: FormControl = new FormControl();
	
	protected _onDestroy = new Subject<void>();

	constructor(
		private _clienteService: ClienteService,
		private _filialService: FilialService,
		private _autorizadaService: AutorizadaService,
		private _regiaoAutorizadaService: RegiaoAutorizadaService,
		protected _userService: UserService,
		protected _formBuilder: FormBuilder
	) {
		super(_userService, _formBuilder, 'local-atendimento');
	}

	ngOnInit(): void {
		this.createForm();
		this.loadData();
	}

	async loadData() {
		this.obterClientes();
		this.obterFiliais();
		this.obterAutorizadas();
		this.obterRegioesAutorizadas();
		this.registrarEmitters();
		this.aoSelecionarFilial();
	}

	createForm(): void {
		this.form = this._formBuilder.group({
			indAtivo: [undefined],
			codClientes: [undefined],
			codFiliais: [undefined],
			codAutorizadas: [undefined],
			codRegioes: [undefined]
		});
		this.form.patchValue(this.filter?.parametros);
	}


	async obterClientes(filtro: string = '') {
		let params: ClienteParameters = {
			indAtivo: statusConst.ATIVO,
			filter: filtro,
			sortActive: 'nomeFantasia',
			sortDirection: 'asc',
			pageSize: 1000
		};
		const data = await this._clienteService
			.obterPorParametros(params)
			.toPromise();
		this.clientes = data.items;
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



	private registrarEmitters() {

		this.clienteFilterCtrl.valueChanges
			.pipe(
				takeUntil(this._onDestroy),
				debounceTime(700),
				distinctUntilChanged()
			)
			.subscribe(() => {
				this.obterClientes(this.clienteFilterCtrl.value);
			});

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

		this.autorizadaFilterCtrl.valueChanges
			.pipe(
				takeUntil(this._onDestroy),
				debounceTime(700),
				distinctUntilChanged()
			)
			.subscribe(() => {
				this.obterAutorizadas(this.autorizadaFilterCtrl.value);
			});

		this.regiaoFilterCtrl.valueChanges
			.pipe(
				takeUntil(this._onDestroy),
				debounceTime(700),
				distinctUntilChanged()
			)
			.subscribe(() => {
				this.obterRegioesAutorizadas(this.regiaoFilterCtrl.value);
			});
	
	}

	ngOnDestroy() {
		this._onDestroy.next();
		this._onDestroy.complete();
	}
}
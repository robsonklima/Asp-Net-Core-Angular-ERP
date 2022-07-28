import { UserService } from '../../../../../core/user/user.service';
import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormControl } from '@angular/forms';
import { MatSidenav } from '@angular/material/sidenav';
import { Subject } from 'rxjs';
import { FilterBase } from '../../../../../core/filters/filter-base';
import { IFilterBase } from '../../../../../core/types/filtro.types';
import { takeUntil, debounceTime, distinctUntilChanged } from 'rxjs/operators';
import { UsuarioService } from 'app/core/services/usuario.service';
import { statusConst } from 'app/core/types/status-types';
import { Cliente, ClienteParameters } from 'app/core/types/cliente.types';
import { ClienteService } from 'app/core/services/cliente.service';
import { FilialService } from 'app/core/services/filial.service';
import { AutorizadaService } from 'app/core/services/autorizada.service';
import { RegiaoService } from 'app/core/services/regiao.service';
import { Filial, FilialParameters } from 'app/core/types/filial.types';
import { Autorizada, AutorizadaParameters } from 'app/core/types/autorizada.types';
import { Regiao, RegiaoParameters } from 'app/core/types/regiao.types';


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
		private _regiaoService: RegiaoService,
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
		this.obterRegioes();
		this.registrarEmitters();
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

	async obterAutorizadas(filtro: string = '') {
		let params: AutorizadaParameters = {
			filter: filtro,
			sortActive: 'nomeFantasia',
			sortDirection: 'asc',
			pageSize: 1000
		};
		const data = await this._autorizadaService
			.obterPorParametros(params)
			.toPromise();
		this.autorizadas = data.items;
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

		this.clienteFilterCtrl.valueChanges
			.pipe(
				takeUntil(this._onDestroy),
				debounceTime(700),
				distinctUntilChanged()
			)
			.subscribe(() => {
				this.obterClientes(this.clienteFilterCtrl.value);
			});

		this.filialFilterCtrl.valueChanges
			.pipe(
				takeUntil(this._onDestroy),
				debounceTime(700),
				distinctUntilChanged()
			)
			.subscribe(() => {
				this.obterFiliais(this.filialFilterCtrl.value);
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
				this.obterRegioes(this.regiaoFilterCtrl.value);
			});
	
	}

	ngOnDestroy() {
		this._onDestroy.next();
		this._onDestroy.complete();
	}
}
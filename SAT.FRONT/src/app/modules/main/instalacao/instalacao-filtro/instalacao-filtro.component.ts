import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormControl } from '@angular/forms';
import { MatSidenav } from '@angular/material/sidenav';
import { FilterBase } from 'app/core/filters/filter-base';
import { ClienteService } from 'app/core/services/cliente.service';
import { Cliente, ClienteParameters } from 'app/core/types/cliente.types';
import { IFilterBase } from 'app/core/types/filtro.types';
import { UserService } from 'app/core/user/user.service';
import { Subject } from 'rxjs';
import { debounceTime, distinctUntilChanged, takeUntil } from 'rxjs/operators';

@Component({
	selector: 'app-instalacao-filtro',
	templateUrl: './instalacao-filtro.component.html'
})
export class InstalacaoFiltroComponent extends FilterBase implements OnInit, IFilterBase {
	@Input() sidenav: MatSidenav;
	clientes: Cliente[] = [];
	clienteFilterCtrl: FormControl = new FormControl();
	protected _onDestroy = new Subject<void>();

	constructor(
		private _clienteService: ClienteService,
		protected _userService: UserService,
		protected _formBuilder: FormBuilder
	) {
		super(_userService, _formBuilder, 'instalacao-contrato');
	}

	ngOnInit(): void {
		this.createForm();
		this.loadData();
	}

	createForm(): void {
		this.form = this._formBuilder.group({
			codClientes: [undefined],
		});
		this.form.patchValue(this.filter?.parametros);
	}

	loadData(): void {
		this.obterClientes();
		this.registrarEmitters();
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
	}

	async obterClientes(filtro: string = '') {
		let params: ClienteParameters = {
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

	limpar() {
		super.limpar();
	}

	ngOnDestroy() {
		this._onDestroy.next();
		this._onDestroy.complete();
	}
}

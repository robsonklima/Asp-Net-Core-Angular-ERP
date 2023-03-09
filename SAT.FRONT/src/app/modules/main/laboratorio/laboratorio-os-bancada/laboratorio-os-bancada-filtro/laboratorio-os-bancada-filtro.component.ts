import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormControl } from '@angular/forms';
import { MatSidenav } from '@angular/material/sidenav';
import { FilterBase } from 'app/core/filters/filter-base';
import { ClienteBancadaService } from 'app/core/services/cliente-bancada.service';
import { ClienteBancada, ClienteBancadaParameters } from 'app/core/types/cliente-bancada.types';
import { IFilterBase } from 'app/core/types/filtro.types';
import { LaudoStatus } from 'app/core/types/laudo-status.types';
import { statusConst } from 'app/core/types/status-types';
import { UserService } from 'app/core/user/user.service';
import _ from 'lodash';
import { Subject } from 'rxjs';
import { debounceTime, distinctUntilChanged, takeUntil } from 'rxjs/operators';

@Component({
	selector: 'app-laboratorio-os-bancada-filtro',
	templateUrl: './laboratorio-os-bancada-filtro.component.html'
})
export class LaboratorioOSBancadaFiltroComponent extends FilterBase implements OnInit, IFilterBase {
	@Input() sidenav: MatSidenav;
	clientes: ClienteBancada[] = [];
	status: LaudoStatus[] = [];
	clientesFiltro: FormControl = new FormControl();
	equipsFiltro: FormControl = new FormControl();
	protected _onDestroy = new Subject<void>();

	constructor(
		protected _userService: UserService,
		protected _formBuilder: FormBuilder,
		private _clienteBancadaService: ClienteBancadaService,
	) {
		super(_userService, _formBuilder, 'os-bancada');
	}

	ngOnInit(): void {
		this.createForm();
		this.loadData();
		this.registrarEmitters();
	}

	loadData(): void {
		this.obterClientes();
	}

	createForm(): void {
		this.form = this._formBuilder.group({
			CodClienteBancadas: [undefined],
			CodStatus: [undefined],
		});
		this.form.patchValue(this.filter?.parametros);
	}

	registrarEmitters() {
		this.clientesFiltro.valueChanges
			.pipe(
				takeUntil(this._onDestroy),
				debounceTime(700),
				distinctUntilChanged()
			)
			.subscribe(() => {
				this.obterClientes(this.clientesFiltro.value);
			});
	}
	

	async obterClientes(filtro: string = '') {
		let params: ClienteBancadaParameters = {
			filter: filtro,
			indAtivo: statusConst.ATIVO,
			sortActive: 'apelido',
			sortDirection: 'asc',
			pageSize: 1000
		};

		const data = await this._clienteBancadaService
			.obterPorParametros(params)
			.toPromise();

		this.clientes = data.items;
	}

	ngOnDestroy() {
		this._onDestroy.next();
		this._onDestroy.complete();
	  }

}

import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormControl } from '@angular/forms';
import { MatSidenav } from '@angular/material/sidenav';
import { Subject } from 'rxjs';
import { takeUntil, debounceTime, distinctUntilChanged } from 'rxjs/operators';
import { FilterBase } from 'app/core/filters/filter-base';
import { IFilterBase } from 'app/core/types/filtro.types';
import { UserService } from 'app/core/user/user.service';
import { InstalacaoTipoPleito, InstalacaoTipoPleitoParameters } from 'app/core/types/instalacao-tipo-pleito.types';
import { Contrato, ContratoParameters } from 'app/core/types/contrato.types';
import { ContratoService } from 'app/core/services/contrato.service';
import { InstalacaoTipoPleitoService } from 'app/core/services/instalacao-tipo-pleito-service';
import { statusConst } from 'app/core/types/status-types';
import { Cliente, ClienteParameters } from 'app/core/types/cliente.types';
import { ClienteService } from 'app/core/services/cliente.service';

@Component({
	selector: 'app-instalacao-pleito-filtro',
	templateUrl: './instalacao-pleito-filtro.component.html'
})
export class InstalacaoPleitoFiltroComponent extends FilterBase implements OnInit, IFilterBase {

	@Input() sidenav: MatSidenav;
	tipos: InstalacaoTipoPleito[] = [];
	tipoFilterCtrl: FormControl = new FormControl();
	contratos: Contrato[] = [];
	contratoFilterCtrl: FormControl = new FormControl();
	clientes: Cliente[] = [];
	clienteFilterCtrl: FormControl = new FormControl();
	protected _onDestroy = new Subject<void>();

	constructor(
		private _tipoPleitoService: InstalacaoTipoPleitoService,
		private _contratoService: ContratoService,
		private _clienteService: ClienteService,
		protected _userService: UserService,
		protected _formBuilder: FormBuilder
	) {
		super(_userService, _formBuilder, 'instalacao-pleito');
	}

	ngOnInit(): void {
		this.createForm();
		this.loadData();
	}


	async loadData() {
		this.obterClientes();
		this.obterTipos();
		this.registrarEmitters();

		this.aoSelecionarCliente();
	}
	
	createForm(): void {
		this.form = this._formBuilder.group({
			codClientes: [undefined],
			codContratos: [undefined],
			codInstalTipoPleitos: [undefined],
		});
		this.form.patchValue(this.filter?.parametros);
	}

	async obterTipos(filtro: string = '') {
		let params: InstalacaoTipoPleitoParameters = {
			filter: filtro,
			sortActive: 'nomeTipoPleito',
			sortDirection: 'asc',
			pageSize: 1000
		};
		const data = await this._tipoPleitoService
			.obterPorParametros(params)
			.toPromise();
		this.tipos = data.items;
	}

	async obterClientes(filtro: string = '') {
		let params: ClienteParameters = {
			filter: filtro,
			indAtivo: statusConst.ATIVO,
			sortActive: 'nomeFantasia',
			sortDirection: 'asc',
			codCliente: this.userSession?.usuario?.codCliente,
			pageSize: 1000
		};

		const data = await this._clienteService
			.obterPorParametros(params)
			.toPromise();

		this.clientes = data.items;
	}

	async obterContratos(filtro: string = '') {
		let params: ContratoParameters = {
			filter: filtro,
			indAtivo: statusConst.ATIVO,
			codClientes: this.form.controls['codClientes'].value.join(','),
			sortActive: 'nroContrato',
			sortDirection: 'asc',
			pageSize: 1000
		};
		const data = await this._contratoService
			.obterPorParametros(params)
			.toPromise();
		this.contratos = data.items;
	}

	aoSelecionarCliente() {
		if (
			this.form.controls['codClientes'].value &&
			this.form.controls['codClientes'].value != ''
		) {
			this.obterContratos();
			this.form.controls['codContratos'].enable();
		}
		else {
			this.form.controls['codContratos'].disable();
		}

		this.form.controls['codClientes']
			.valueChanges
			.subscribe(() => {
				if (this.form.controls['codClientes'].value && this.form.controls['codClientes'].value != '') {
					this.obterContratos();
					this.form.controls['codContratos'].enable();
				}
				else {
					this.form.controls['codContratos'].setValue(null);
					this.form.controls['codContratos'].disable();
				}
			});
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

		this.form.controls['codClientes'].valueChanges
			.pipe(
				takeUntil(this._onDestroy),
				debounceTime(700),
				distinctUntilChanged()
			)
			.subscribe(() => {
				this.obterContratos(this.clienteFilterCtrl.value);
			});

		this.tipoFilterCtrl.valueChanges
			.pipe(
				takeUntil(this._onDestroy),
				debounceTime(700),
				distinctUntilChanged()
			)
			.subscribe(() => {

				this.obterTipos(this.tipoFilterCtrl.value);
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
import { UserService } from '../../../../../core/user/user.service';
import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormControl } from '@angular/forms';
import { MatSidenav } from '@angular/material/sidenav';
import { Subject } from 'rxjs';
import { FilterBase } from '../../../../../core/filters/filter-base';
import { IFilterBase } from '../../../../../core/types/filtro.types';
import { takeUntil, debounceTime, distinctUntilChanged } from 'rxjs/operators';
import { ClienteService } from 'app/core/services/cliente.service';
import { TipoContratoService } from 'app/core/services/tipo-contrato.service';
import { TipoContrato, TipoContratoParameters } from 'app/core/types/tipo-contrato.types';
import { Cliente, ClienteParameters } from 'app/core/types/cliente.types';
import { statusConst } from 'app/core/types/status-types';




@Component({
	selector: 'app-contrato-filtro',
	templateUrl: './contrato-filtro.component.html'
})
export class ContratoFiltroComponent extends FilterBase implements OnInit, IFilterBase {
	
	@Input() sidenav: MatSidenav;
    tipos: TipoContrato[] = [];
    tipoFilterCtrl: FormControl = new FormControl();
    clientes: Cliente[] = [];
    clienteFilterCtrl: FormControl = new FormControl();
	protected _onDestroy = new Subject<void>();

	constructor(
        private _tipoContratoService: TipoContratoService,
        private _clienteService: ClienteService,
		protected _userService: UserService,
		protected _formBuilder: FormBuilder
	) {
		super(_userService, _formBuilder, 'contrato');
	}

	ngOnInit(): void {
		this.createForm();
		this.loadData();
		
	}
	  
	async loadData() {
        this.obterTipos();
        this.obterClientes();
		this.registrarEmitters();
	}

	createForm(): void {
		this.form = this._formBuilder.group({
			CodTipoContratos: [undefined],
      CodClientes: [undefined],
      indAtivo: [undefined],
			

		});
		this.form.patchValue(this.filter?.parametros);
	}

    async obterTipos(filtro: string = '') {
		let params: TipoContratoParameters = {
			filter: filtro,
			sortActive: 'nomeTipoContrato',
			sortDirection: 'asc',
			pageSize: 1000
		};
		const data = await this._tipoContratoService
			.obterPorParametros(params)
			.toPromise();
		this.tipos = data.items;
	}

    async obterClientes(filtro: string = '') {
		let params: ClienteParameters = {
			filter: filtro,
			indAtivo: statusConst.ATIVO,
			sortActive: 'razaoSocial',
			sortDirection: 'asc',
			pageSize: 1000
		};
		const data = await this._clienteService
			.obterPorParametros(params)
			.toPromise();
		this.clientes = data.items;
	}



	private registrarEmitters() {
        this.tipoFilterCtrl.valueChanges
		.pipe(
			takeUntil(this._onDestroy),
			debounceTime(700),
			distinctUntilChanged()
		)
		.subscribe(() => {
	    
        this.obterTipos(this.tipoFilterCtrl.value);
		});
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

	limpar() {
		super.limpar();

	}

	ngOnDestroy() {
		this._onDestroy.next();
		this._onDestroy.complete();
	}
}
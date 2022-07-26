import { UserService } from '../../../../../core/user/user.service';
import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormControl } from '@angular/forms';
import { MatSidenav } from '@angular/material/sidenav';
import { Subject } from 'rxjs';
import { FilterBase } from '../../../../../core/filters/filter-base';
import { IFilterBase } from '../../../../../core/types/filtro.types';
import { takeUntil, debounceTime, distinctUntilChanged } from 'rxjs/operators';
import { PecaStatus, PecaStatusParameters } from 'app/core/types/peca.types';
import { PecaStatusService } from 'app/core/services/peca-status.service';
import { Contrato, ContratoParameters } from 'app/core/types/contrato.types';
import { Cliente, ClienteParameters } from 'app/core/types/cliente.types';
import { ContratoService } from 'app/core/services/contrato.service';
import { ClienteService } from 'app/core/services/cliente.service';




@Component({
	selector: 'app-cliente-peca-filtro',
	templateUrl: './cliente-peca-filtro.component.html'
})
export class ClientePecaFiltroComponent extends FilterBase implements OnInit, IFilterBase {
	
	@Input() sidenav: MatSidenav;
    status: PecaStatus[] = [];
    statusFilterCtrl: FormControl = new FormControl();
    clientes: Cliente[] = [];
    clienteFilterCtrl: FormControl = new FormControl();
    contratos: Contrato[] = [];
    contratoFilterCtrl: FormControl = new FormControl();
	protected _onDestroy = new Subject<void>();

	constructor(
        private _pecaStatusService: PecaStatusService,
        private _clienteService: ClienteService,
        private _contratoService: ContratoService,
		protected _userService: UserService,
		protected _formBuilder: FormBuilder
	) {
		super(_userService, _formBuilder, 'cliente-peca');
	}

	ngOnInit(): void {
		this.createForm();
		this.loadData();
		
	}
	  
	async loadData() {
        this.obterStatus();
        this.obterContratos();
        this.obterClientes();
		this.registrarEmitters();
	}

	createForm(): void {
		this.form = this._formBuilder.group({
			CodPecaStatus: [undefined],
            CodContratos: [undefined],
            CodClientes: [undefined],

			

		});
		this.form.patchValue(this.filter?.parametros);
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

    async obterContratos(filtro: string = '') {
		let params: ContratoParameters = {
			filter: filtro,
			sortActive: 'nomeContrato',
			sortDirection: 'asc',
			pageSize: 1000
		};
		const data = await this._contratoService
			.obterPorParametros(params)
			.toPromise();
		this.contratos = data.items;
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



	private registrarEmitters() {
        this.statusFilterCtrl.valueChanges
		.pipe(
			takeUntil(this._onDestroy),
			debounceTime(700),
			distinctUntilChanged()
		)
		.subscribe(() => {
	    
        this.obterStatus(this.statusFilterCtrl.value);
		});

        this.contratoFilterCtrl.valueChanges
		.pipe(
			takeUntil(this._onDestroy),
			debounceTime(700),
			distinctUntilChanged()
		)
		.subscribe(() => {
	    
        this.obterContratos(this.contratoFilterCtrl.value);
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
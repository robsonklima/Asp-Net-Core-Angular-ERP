import { UserService } from '../../../../../core/user/user.service';
import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormControl } from '@angular/forms';
import { MatSidenav } from '@angular/material/sidenav';
import { Subject } from 'rxjs';
import { FilterBase } from '../../../../../core/filters/filter-base';
import { IFilterBase } from '../../../../../core/types/filtro.types';
import { takeUntil, debounceTime, distinctUntilChanged } from 'rxjs/operators';
import { Cidade, CidadeParameters } from 'app/core/types/cidade.types';
import { CidadeService } from 'app/core/services/cidade.service';
import { ClienteBancada, ClienteBancadaParameters } from 'app/core/types/cliente-bancada.types';
import { ClienteBancadaService } from 'app/core/services/cliente-bancada.service';
import { statusConst } from 'app/core/types/status-types';




@Component({
	selector: 'app-cliente-bancada-filtro',
	templateUrl: './cliente-bancada-filtro.component.html'
})
export class ClienteBancadaFiltroComponent extends FilterBase implements OnInit, IFilterBase {
	
	@Input() sidenav: MatSidenav;
    clienteBancadas: ClienteBancada[] = [];
    clienteBancadaFilterCtrl: FormControl = new FormControl();
    cidades: Cidade[] = [];
    cidadeFilterCtrl: FormControl = new FormControl();
	protected _onDestroy = new Subject<void>();

	constructor(
        private _clienteBancadaService: ClienteBancadaService,
        private _cidadeService: CidadeService,
		protected _userService: UserService,
		protected _formBuilder: FormBuilder
	) {
		super(_userService, _formBuilder, 'cliente-bancada');
	}

	ngOnInit(): void {
		this.createForm();
		this.loadData();
		
	}
	  
	async loadData() {
        this.obterClienteBancadas();
        this.obterCidades();
		this.registrarEmitters();
	}

	createForm(): void {
		this.form = this._formBuilder.group({
			CodClienteBancadas: [undefined],
            CodCidades: [undefined],
			

		});
		this.form.patchValue(this.filter?.parametros);
	}

    async obterClienteBancadas(filtro: string = '') {
		let params: ClienteBancadaParameters = {
			filter: filtro,
			sortActive: 'nomeCliente',
			sortDirection: 'asc',
			pageSize: 1000
		};
		const data = await this._clienteBancadaService
			.obterPorParametros(params)
			.toPromise();
		this.clienteBancadas = data.items;
	}

    async obterCidades(filtro: string = '') {
		let params: CidadeParameters = {
			filter: filtro,
			sortActive: 'nomeCidade',
			sortDirection: 'asc',
			pageSize: 1000,
			indAtivo: statusConst.ATIVO,
		};
		const data = await this._cidadeService
			.obterPorParametros(params)
			.toPromise();
		this.cidades = data.items;
	}



	private registrarEmitters() {
        this.clienteBancadaFilterCtrl.valueChanges
		.pipe(
			takeUntil(this._onDestroy),
			debounceTime(700),
			distinctUntilChanged()
		)
		.subscribe(() => {
	    
        this.obterClienteBancadas(this.clienteBancadaFilterCtrl.value);
		});
        this.cidadeFilterCtrl.valueChanges
		.pipe(
			takeUntil(this._onDestroy),
			debounceTime(700),
			distinctUntilChanged()
		)
		.subscribe(() => {
			this.obterCidades(this.cidadeFilterCtrl.value);
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
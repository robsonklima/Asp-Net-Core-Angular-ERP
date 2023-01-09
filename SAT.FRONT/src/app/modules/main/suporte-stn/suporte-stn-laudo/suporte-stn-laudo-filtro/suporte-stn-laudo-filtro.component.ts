import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormControl } from '@angular/forms';
import { MatSidenav } from '@angular/material/sidenav';
import { FilterBase } from 'app/core/filters/filter-base';
import { ClienteService } from 'app/core/services/cliente.service';
import { EquipamentoService } from 'app/core/services/equipamento.service';
import { LaudoStatusService } from 'app/core/services/laudo-status.service';
import { Cliente, ClienteParameters } from 'app/core/types/cliente.types';
import { Equipamento, EquipamentoParameters } from 'app/core/types/equipamento.types';
import { IFilterBase } from 'app/core/types/filtro.types';
import { LaudoStatus, LaudoStatusParameters } from 'app/core/types/laudo-status.types';
import { statusConst } from 'app/core/types/status-types';
import { UserService } from 'app/core/user/user.service';
import { Subject } from 'rxjs';
import { debounceTime, filter, map, takeUntil } from 'rxjs/operators';

@Component({
	selector: 'app-suporte-stn-laudo-filtro',
	templateUrl: './suporte-stn-laudo-filtro.component.html'
})
export class SuporteStnLaudoFiltroComponent extends FilterBase implements OnInit, IFilterBase {
	@Input() sidenav: MatSidenav;
	clientes: Cliente[] = [];
	equips: Equipamento[] = [];
	status: LaudoStatus[] = [];
	clientesFiltro: FormControl = new FormControl();
	equipsFiltro: FormControl = new FormControl();
	protected _onDestroy = new Subject<void>();

	constructor(
		protected _userService: UserService,
		protected _formBuilder: FormBuilder,
		private _clienteService: ClienteService,
		private _equipamentoService: EquipamentoService,
		private _laudoStatusService: LaudoStatusService,
	) {
		super(_userService, _formBuilder, 'suporte-stn');
	}

	ngOnInit(): void {
		this.createForm();
		this.loadData();
		this.registrarEmitters();
	}

	loadData(): void {
		this.obterClientes();
		this.obterEquipamentos();
		this.obterLaudoStatus();
	}

	createForm(): void {
		this.form = this._formBuilder.group({
			CodClientes: [undefined],
			CodEquips: [undefined],
			CodLaudosStatus: [undefined],
		});
		this.form.patchValue(this.filter?.parametros);
	}

	registrarEmitters() {
		this.clientesFiltro.valueChanges
      .pipe(
        filter(query => !!query),
        takeUntil(this._onDestroy),
        debounceTime(700),
        map(async query => {
          const data = await this._clienteService.obterPorParametros({
            sortActive: 'codCliente',
            sortDirection: 'asc',
            indAtivo: statusConst.ATIVO,
            filter: query,
            pageSize: 100,
          }).toPromise();

          return data.items.slice();
        }),
        takeUntil(this._onDestroy)
      )
      .subscribe(async data => {
        this.clientes = await data;
      });

	  this.equipsFiltro.valueChanges
      .pipe(
        filter(query => !!query),
        takeUntil(this._onDestroy),
        debounceTime(700),
        map(async query => {
          const data = await this._equipamentoService.obterPorParametros({
            sortActive: 'codEquip',
            sortDirection: 'asc',
            filter: query,
            pageSize: 100,
          }).toPromise();

          return data.items.slice();
        }),
        takeUntil(this._onDestroy)
      )
      .subscribe(async data => {
        this.equips = await data;
      });
		}
	

	async obterClientes(filtro: string = '') {
		let params: ClienteParameters = {
			filter: filtro,
			indAtivo: statusConst.ATIVO,
			sortActive: 'nomeFantasia',
			sortDirection: 'asc',
			pageSize: 1000
		};

		const data = await this._clienteService
			.obterPorParametros(params)
			.toPromise();

		this.clientes = data.items;
	}

	async obterEquipamentos(filtro: string = '') {
		let params: EquipamentoParameters = {
			filter: filtro,
			sortActive: 'nomeEquip',
			sortDirection: 'asc',
			pageSize: 100
		};

		const data = await this._equipamentoService.obterPorParametros(params).toPromise();
		this.equips = data.items;
	}

	async obterLaudoStatus(filtro: string = ''){
		let params: LaudoStatusParameters = {
			filter: filtro,
			sortActive: 'nomeStatus',
			sortDirection: 'asc'
		};

		const data = await this._laudoStatusService.obterPorParametros(params).toPromise();
		this.status = data.items;
	}

	ngOnDestroy() {
		this._onDestroy.next();
		this._onDestroy.complete();
	  }

}

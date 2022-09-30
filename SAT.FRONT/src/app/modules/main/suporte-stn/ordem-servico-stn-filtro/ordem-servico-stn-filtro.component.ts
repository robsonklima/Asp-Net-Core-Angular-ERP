import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { MatSidenav } from '@angular/material/sidenav';
import { FilterBase } from 'app/core/filters/filter-base';
import { ClienteService } from 'app/core/services/cliente.service';
import { Cliente, ClienteParameters } from 'app/core/types/cliente.types';
import { IFilterBase } from 'app/core/types/filtro.types';
import { statusConst } from 'app/core/types/status-types';
import { UserService } from 'app/core/user/user.service';

@Component({
  selector: 'app-ordem-servico-stn-filtro',
  templateUrl: './ordem-servico-stn-filtro.component.html'
})
export class OrdemServicoSTNFiltroComponent extends FilterBase implements OnInit, IFilterBase {
  @Input() sidenav: MatSidenav;
  clientes: Cliente[] = [];

  constructor(
    protected _userService: UserService,
		protected _formBuilder: FormBuilder,
		private _clienteService: ClienteService,
  ) {
    super(_userService, _formBuilder, 'orcamento');
  }

  ngOnInit(): void {
		this.createForm();
		this.loadData();
	}

  loadData(): void {
		this.obterClientes();
	}

	createForm(): void {
		this.form = this._formBuilder.group({
			codClientes: [undefined],
		});
		this.form.patchValue(this.filter?.parametros);
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
}

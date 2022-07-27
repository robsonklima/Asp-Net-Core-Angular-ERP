import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormControl } from '@angular/forms';
import { MatSidenav } from '@angular/material/sidenav';
import { FilterBase } from 'app/core/filters/filter-base';
import { ClienteService } from 'app/core/services/cliente.service';
import { FilialService } from 'app/core/services/filial.service';
import { StatusServicoService } from 'app/core/services/status-servico.service';
import { Cliente, ClienteParameters } from 'app/core/types/cliente.types';
import { Filial, FilialParameters } from 'app/core/types/filial.types';
import { IFilterBase } from 'app/core/types/filtro.types';
import { StatusServico, StatusServicoParameters } from 'app/core/types/status-servico.types';
import { statusConst } from 'app/core/types/status-types';
import { UserService } from 'app/core/user/user.service';

@Component({
  selector: 'app-orcamento-filtro',
  templateUrl: './orcamento-filtro.component.html'
})
export class OrcamentoFiltroComponent extends FilterBase implements OnInit, IFilterBase {
  @Input() sidenav: MatSidenav;
  statusServicos: StatusServico[] = [];
  filiais: Filial[] = [];
  clientes: Cliente[] = [];
  
  constructor(
    protected _userService: UserService,
    protected _formBuilder: FormBuilder,
    private _statusServicoService: StatusServicoService,
		private _filialService: FilialService,
		private _clienteService: ClienteService,

  ) {
    super(_userService, _formBuilder, 'orcamento');
  }

  ngOnInit(): void {
    this.createForm();
    this.loadData();
  }

  createForm(): void {
    this.form = this._formBuilder.group({
      codStatusServicos: [undefined],
      codFiliais: [undefined],
      codClientes: [undefined],
    });
    this.form.patchValue(this.filter?.parametros);
  }  

  loadData(): void {
    this.obterStatusServicos();
    this.obterClientes();
    this.obterFiliais();
  }

  async obterStatusServicos() {
    let params: StatusServicoParameters = {
      indAtivo: statusConst.ATIVO,
      sortActive: 'nomeStatusServico',
      sortDirection: 'asc'
    }

    const data = await this._statusServicoService
      .obterPorParametros(params)
      .toPromise();

    this.statusServicos = data.items;
  }

	async obterFiliais(nomeFilial: string = '') {
		let params: FilialParameters = {
      indAtivo: 1,
      sortActive: 'nomeFilial',
      sortDirection: 'asc'      
		};

		const data = await this._filialService
			.obterPorParametros(params)
			.toPromise();

		this.filiais = data.items;
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


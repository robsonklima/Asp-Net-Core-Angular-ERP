import { LaudoStatus } from './../../../../core/types/laudo.types';
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
import { TipoIntervencaoService } from 'app/core/services/tipo-intervencao.service';
import { TipoIntervencao, TipoIntervencaoParameters } from 'app/core/types/tipo-intervencao.types';

@Component({
	selector: 'app-orcamento-filtro',
	templateUrl: './orcamento-filtro.component.html'
})
export class OrcamentoFiltroComponent extends FilterBase implements OnInit, IFilterBase {
	@Input() sidenav: MatSidenav;
	statusServicos: StatusServico[] = [];
	laudosStatus: LaudoStatus[] = [];
	filiais: Filial[] = [];
	clientes: Cliente[] = [];
	tiposIntervencao: TipoIntervencao[] = [];

	constructor(
		protected _userService: UserService,
		protected _formBuilder: FormBuilder,
		private _statusServicoService: StatusServicoService,
		private _filialService: FilialService,
		private _clienteService: ClienteService,
		private _tipoIntervencaoService: TipoIntervencaoService

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
			codLaudosStatus: [undefined],
			codFiliais: [undefined],
			codClientes: [undefined],
			codTiposIntervencao: [undefined],
			numSerie: [undefined],
			codigoOrdemServico: [undefined],
			numOSCliente: [undefined],
			dataInicio: [undefined],
			dataFim: [undefined],
			dataAberturaInicio: [undefined],
			dataAberturaFim: [undefined],
		});
		this.form.patchValue(this.filter?.parametros);
	}
	
	loadData(): void {
		this.obterStatusServicos();
		this.obterClientes();
		this.obterFiliais();
		this.obterTiposIntervencao();
		this.obterLaudoStatus();
	}

	obterLaudoStatus() {
		this.laudosStatus = [
			{
				codLaudoStatus: 1,
				nomeStatus: 'PENDENTE',
				indAtivo: 1
			},
			{
				codLaudoStatus: 2,
				nomeStatus: 'APROVADO',
				indAtivo: 1
			},
			{
				codLaudoStatus: 3,
				nomeStatus: 'APROVADO C/ RESSALVAS',
				indAtivo: 1
			},
			{
				codLaudoStatus: 4,
				nomeStatus: 'REPROVADO',
				indAtivo: 1
			},
		];
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

	async obterTiposIntervencao(filtro: string = '') {
		let params: TipoIntervencaoParameters = {
			filter: filtro,
			indAtivo: 1,
			sortActive: 'nomTipoIntervencao',
			sortDirection: 'asc'
		}

		const data = await this._tipoIntervencaoService
			.obterPorParametros(params)
			.toPromise();

		this.tiposIntervencao = data.items;
	}

}


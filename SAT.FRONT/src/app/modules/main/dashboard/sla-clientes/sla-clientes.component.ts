import { ViewDashboardSLAClientes } from './../../../../core/types/dashboard.types';
import { Component, Input, OnInit } from "@angular/core";
import { MatSidenav } from "@angular/material/sidenav";
import { Filterable } from "app/core/filters/filterable";
import { DashboardService } from "app/core/services/dashboard.service";
import { DashboardViewEnum } from "app/core/types/dashboard.types";
import { IFilterable } from "app/core/types/filtro.types";
import { UsuarioSessao } from "app/core/types/usuario.types";
import { UserService } from "app/core/user/user.service";
import Enumerable from "linq";
import _ from "lodash";

@Component({
	selector: 'app-sla-clientes',
	templateUrl: './sla-clientes.component.html'
})
export class SlaClientesComponent extends Filterable implements OnInit, IFilterable {
	@Input() sidenav: MatSidenav;
	public usuarioSessao: UsuarioSessao;
	public loading: boolean;
	public redColor: string = "#cc0000";
	public greenColor: string = "#009900";
	public yellowColor: string = "#ffcc00";
	public grayColor: string = "#546E7A";
	public pieData: ViewDashboardSLAClientes[];

	constructor(
		private _dashboardService: DashboardService,
		protected _userService: UserService
		) {
		super(_userService, 'dashboard-filtro')
	}
	ngOnInit(): void {
		this.carregarGrafico();
		this.registerEmitters();
	}

	registerEmitters(): void {
		this.sidenav.closedStart.subscribe(() => {
			this.onSidenavClosed();
			this.carregarGrafico();
		})
	}

	loadFilter(): void {
		super.loadFilter();
	}

	public async carregarGrafico() {
		this.loading = true;

		let orderData = Enumerable.from((await this._dashboardService.obterViewPorParametros({ dashboardViewEnum: DashboardViewEnum.SLA_CLIENTES }).toPromise())
			.viewDashboardSLAClientes);

		this.pieData = [
			...orderData.where(data => data.cliente == 'GLOBAL'),
			...orderData.where(data => data.cliente != 'GLOBAL')
		];

		this.loading = false;
	}
}
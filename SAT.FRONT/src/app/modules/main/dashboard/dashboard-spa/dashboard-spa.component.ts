import { ViewDashboardSPA } from './../../../../core/types/dashboard.types';
import { Component, Input, OnInit, ViewChild } from "@angular/core";
import { MatSidenav } from "@angular/material/sidenav";
import { Filterable } from "app/core/filters/filterable";
import { DashboardService } from "app/core/services/dashboard.service";
import { DashboardViewEnum } from "app/core/types/dashboard.types";
import { IFilterable } from "app/core/types/filtro.types";
import { UsuarioSessao } from "app/core/types/usuario.types";
import { UserService } from "app/core/user/user.service";
import {
	ApexChart,
	ChartComponent,
	ApexNonAxisChartSeries,
	ApexResponsive
} from "ng-apexcharts";

export type ChartOptions = {
	series: ApexNonAxisChartSeries;
	chart: ApexChart;
	responsive: ApexResponsive[];
	labels: any;
	title: any;
};

@Component({
	selector: 'app-dashboard-spa',
	templateUrl: './dashboard-spa.component.html'
})
export class DashboardSpaComponent extends Filterable implements OnInit, IFilterable {
	@Input() sidenav: MatSidenav;
	@ViewChild("chart") chart: ChartComponent;
	public usuarioSessao: UsuarioSessao;
	public chartOptions: Partial<ChartOptions>;
	public loading: boolean;
	public haveData: boolean;
	public dados: ViewDashboardSPA[];
	public redColor: string = "#cc0000";
	public greenColor: string = "#009900";
	public yellowColor: string = "#ffcc00";
	public grayColor: string = "#546E7A";
  
	responsiveOptions = [
		{
			breakpoint: 480,
			options: {
				chart: {
					width: 200
				},
				legend: {
					position: "bottom"
				}
			}
		}
	]

	constructor(
		private _dashboardService: DashboardService,
		protected _userService: UserService
	) {
		super(_userService, 'dashboard-filtro');
		this.usuarioSessao = JSON.parse(this._userService.userSession);
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
		
		this.dados = (await this._dashboardService.obterViewPorParametros({ dashboardViewEnum: DashboardViewEnum.SPA }).toPromise())
			.viewDashboardSPA;
		
		if (this.usuarioSessao.usuario?.codFilial) {
			this.dados = this.dados.filter(d => d.codFilial == this.usuarioSessao.usuario?.codFilial || d.codFilial == 0);
		}
		console.log(this.dados);
		
		this.loading = false;
	}
}
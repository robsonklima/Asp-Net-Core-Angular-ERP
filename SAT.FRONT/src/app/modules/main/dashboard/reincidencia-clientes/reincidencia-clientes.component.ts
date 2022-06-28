import { ViewDashboardReincidenciaClientes } from './../../../../core/types/dashboard.types';
import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { MatSidenav } from '@angular/material/sidenav';
import { Filterable } from 'app/core/filters/filterable';
import { DashboardService } from 'app/core/services/dashboard.service';
import { DashboardViewEnum } from 'app/core/types/dashboard.types';
import { IFilterable } from 'app/core/types/filtro.types';
import { UsuarioSessao } from 'app/core/types/usuario.types';
import { UserService } from 'app/core/user/user.service';
import Enumerable from 'linq';
import {
	ApexChart,
	ApexAxisChartSeries,
	ChartComponent,
	ApexDataLabels,
	ApexPlotOptions,
	ApexYAxis,
	ApexLegend,
	ApexGrid,
	ApexStates,
	ApexXAxis,
	ApexStroke
} from "ng-apexcharts";

export type ChartOptions =
	{
		series: ApexAxisChartSeries;
		chart: ApexChart;
		dataLabels: ApexDataLabels;
		plotOptions: ApexPlotOptions;
		yaxis: ApexYAxis | ApexYAxis[];
		xaxis: ApexXAxis;
		grid: ApexGrid;
		colors: any[];
		legend: ApexLegend;
		states: ApexStates;
		title: any;
		stroke: ApexStroke;
		labels: string[];
	};

@Component({
	selector: 'app-reincidencia-clientes',
	templateUrl: './reincidencia-clientes.component.html'
})
export class ReincidenciaClientesComponent extends Filterable implements OnInit, IFilterable {
	@Input() sidenav: MatSidenav;
	@ViewChild("chart") chart: ChartComponent;

	public usuarioSessao: UsuarioSessao;
	public chartOptions: Partial<ChartOptions>;
	public loading: boolean;
	public haveData: boolean;
	public dados: ViewDashboardReincidenciaClientes[];
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

		let orderData = Enumerable.from((await this._dashboardService.obterViewPorParametros({ dashboardViewEnum: DashboardViewEnum.REINCIDENCIA_CLIENTES }).toPromise())
			.viewDashboardReincidenciaClientes).orderByDescending(i => i.percentual);

		this.dados = [
			...orderData.where(data => data.cliente == 'GLOBAL'),
			...orderData.where(data => data.cliente != 'GLOBAL')
		];

		this.loading = false;
	}
}

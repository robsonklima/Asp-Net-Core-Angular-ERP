import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { MatSidenav } from '@angular/material/sidenav';
import { Filterable } from 'app/core/filters/filterable';
import { DashboardService } from 'app/core/services/dashboard.service';
import { DashboardViewEnum, ViewDashboardPendenciaGlobal } from 'app/core/types/dashboard.types';
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
  selector: 'app-pendencia-filiais',
  templateUrl: './pendencia-filiais.component.html'
})
export class PendenciaFiliaisComponent extends Filterable implements OnInit, IFilterable {
  @Input() sidenav: MatSidenav;
  @ViewChild("chart") chart: ChartComponent;

  public loading: boolean;
  public haveData: boolean;
  public usuarioSessao: UsuarioSessao;
  public chartOptions: Partial<ChartOptions>;
  public pendenciaGlobal: ViewDashboardPendenciaGlobal;

  private chartMax: number = 30;
  private meta: number = 5;
  private redColor: string = "#cc0000";
  private greenColor: string = "#009900";

  constructor(private _dashboardService: DashboardService,
    protected _userService: UserService) {
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

    this.pendenciaGlobal = Enumerable.from((await this._dashboardService.obterViewPorParametros({ dashboardViewEnum: DashboardViewEnum.PENDENCIA_GLOBAL }).toPromise())
      .viewDashboardPendenciaGlobal).firstOrDefault();
    let data = (await this._dashboardService.obterViewPorParametros({ dashboardViewEnum: DashboardViewEnum.PENDENCIA_FILIAIS }).toPromise())
      .viewDashboardPendenciaFiliais;
    
    if (data?.length) {
      data = Enumerable.from(data).orderByDescending(ord => ord.pendencia).toArray();
      let labels = data.map(d => d.filial);
      let valoresColuna = data.map(d => d.pendencia);
      let valoresLinha: number[] = [];
      valoresColuna.forEach(element => { valoresLinha.push(this.meta); });
      this.haveData = true;

      this.inicializarGrafico(labels, valoresColuna, valoresLinha, this.meta, this.greenColor, this.redColor);
    }
  }

  private inicializarGrafico(labels: string[], valoresColuna: number[], valoresLinha: number[], meta: number, greenColor: string, redColor: string) {
    this.chartOptions = {
      series: [
        {
          name: "Percentual",
          type: "column",
          data: valoresColuna
        },
        {
          name: "Meta de Pendência",
          type: "line",
          data: valoresLinha,
          color: redColor
        }
      ],
      dataLabels:
      {
        enabled: false
      },
      title: {
        text: '* Meta de Pendência deve ser menor ou igual a ' + meta + '%'
      },
      colors: [
        function ({ value }) {
          if (value < meta) {
            return greenColor;
          } else {
            return redColor;
          }
        }
      ],
      chart: {
        height: 350,
        type: "line",
        toolbar: {
          tools: {
            download: true,
            selection: false,
            zoom: false,
            zoomin: false,
            zoomout: false,
            pan: false,
            reset: false
          }
        }
      },
      stroke: {
        width: [0, 2]
      },
      grid:
      {
        show: true
      },
      states:
      {
        active:
        {
          filter:
          {
            type: "none"
          }
        }
      },
      xaxis:
      {
        categories: labels,
        labels:
        {
          style:
          {
            colors: this.greenColor,
            fontSize: "12px"
          }
        }
      },
      yaxis:
      {
        max: this.chartMax,
        min: 0,
        tickAmount: 8,
        labels:
        {
          formatter: (value) => {
            return (value + "%").replace('.', ',');
          }
        }
      }
    };

    this.loading = false;
  }
}
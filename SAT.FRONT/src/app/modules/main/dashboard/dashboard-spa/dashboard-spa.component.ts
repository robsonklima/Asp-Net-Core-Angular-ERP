import { Component, Input, OnInit, ViewChild } from "@angular/core";
import { MatSidenav } from "@angular/material/sidenav";
import { Filterable } from "app/core/filters/filterable";
import { DashboardService } from "app/core/services/dashboard.service";
import { DashboardViewEnum } from "app/core/types/dashboard.types";
import { IFilterable } from "app/core/types/filtro.types";
import { UsuarioSessao } from "app/core/types/usuario.types";
import { UserService } from "app/core/user/user.service";
import Enumerable from "linq";
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
  ApexStroke
} from "ng-apexcharts";

export type ChartOptions =
  {
    series: ApexAxisChartSeries;
    chart: ApexChart;
    dataLabels: ApexDataLabels;
    plotOptions: ApexPlotOptions;
    yaxis: ApexYAxis;
    xaxis: any;
    grid: ApexGrid;
    colors: any[];
    legend: ApexLegend;
    states: ApexStates;
    stroke: ApexStroke;
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

  private chartMax: number = 100;
  private meta: number = 85;
  private redColor: string = "#cc0000";
  private greenColor: string = "#009900";

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

    debugger

    let data = (await this._dashboardService.obterViewPorParametros({ dashboardViewEnum: DashboardViewEnum.SPA }).toPromise())
      .viewDashboardSPA;

    if (data?.length) {
      data = Enumerable.from(data).orderByDescending(ord => ord.percentual).toArray();
      const labels = data.map(d => d.filial);
      let valoresColuna = data.map(d => (this.chartMax / 100) * d.percentual);
      let valoresLinha: number[] = [];
      valoresColuna.forEach(element => { valoresLinha.push(this.meta); });
      this.haveData = true;

      this.inicializarGrafico(labels, valoresColuna, valoresLinha, this.meta, this.greenColor, this.redColor);
    }

    this.loading = false;
  }

  private inicializarGrafico(labels: string[], valoresColuna: number[], valoresLinha: number[], meta: number, greenColor: string, redColor: string) {
    this.chartOptions = {
      series: [
        {
          name: "Percentual",
          type: "column",
          data: valoresColuna,

        },
        {
          name: "Meta de Reincidência",
          type: "line",
          data: valoresLinha,
          color: redColor
        }
      ],
      dataLabels:
      {
        enabled: false
      },
      colors: [
        function ({ value }) {
          if (value < meta) {
            return redColor;
          } else {
            return greenColor;
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
      title: {
        text: '* Meta de SPA deve ser maior ou igual a ' + meta + '%'
      },
      xaxis:
      {
        categories: labels,
        labels:
        {
          style:
          {
            //colors: cores,
            fontSize: "12px"
          }
        }
      },
      yaxis:
      {
        max: this.chartMax,
        min: 0,
        tickAmount: 10,
        labels:
        {
          formatter: (value) => {
            return (value + "%").replace('.', ',');
          }
        }
      }
    };
  }
}
import { Component, Input, OnInit, ViewChild } from "@angular/core";
import { MatSidenav } from "@angular/material/sidenav";
import { Filterable } from "app/core/filters/filterable";
import { DashboardService } from "app/core/services/dashboard.service";
import { DashboardViewEnum } from "app/core/types/dashboard.types";
import { IFilterable } from "app/core/types/filtro.types";
import { UsuarioSessao } from "app/core/types/usuario.types";
import { UserService } from "app/core/user/user.service";
import _ from "lodash";
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
  selector: 'app-sla-clientes',
  templateUrl: './sla-clientes.component.html'
})
export class SlaClientesComponent extends Filterable implements OnInit, IFilterable {
  @Input() sidenav: MatSidenav;
  @ViewChild("chart") chart: ChartComponent;
  public usuarioSessao: UsuarioSessao;
  public chartOptions: Partial<ChartOptions>;
  public loading: boolean;
  public haveData: boolean;

  private chartMax: number = 100;
  private meta: number = 95;
  private redColor: string = "#cc0000";
  private greenColor: string = "#009900";
  private yellowColor: string = "#ffcc00";

  constructor(
    private _dashboardService: DashboardService,
    protected _userService: UserService) {
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
    let data = (await this._dashboardService.obterViewPorParametros({ dashboardViewEnum: DashboardViewEnum.SLA_CLIENTES }).toPromise())
      .viewDashboardSLAClientes;

    if (data?.length) {
      data = _.orderBy(data, ['percentual'], 'desc');
      const labels = data.map(d => d.cliente);
      let valoresColuna = data.map(d => (this.chartMax / 100) * d.percentual);
      let valoresLinha: number[] = [];
      valoresColuna.forEach(element => { valoresLinha.push(this.meta); });
      this.haveData = true;
      this.inicializarGrafico(labels, valoresColuna, valoresLinha, this.meta, this.greenColor, this.redColor, this.yellowColor);
    }
  }

  private inicializarGrafico(labels: string[], valoresColuna: number[], valoresLinha: number[], meta: number, greenColor: string, redColor: string, yellowColor: string) {
    this.chartOptions = {
      series: [
        {
          name: "Percentual",
          type: "bar",
          data: valoresColuna,
        },
        {
          name: "Meta de SLA",
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
          if (value >= meta) {
            return greenColor;
          } else {
                    if (value > 90) {
                      return yellowColor;
                    } else {
                      return redColor;
                    }
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
        text: '* Meta de SLA deve ser maior ou igual a ' + meta + '%'
      },
      xaxis:
      {
        categories: labels,
        labels:
        {
          rotate: -45,
          rotateAlways: true,
          trim: true,
          style:
          {
            fontSize: "8px"
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

    this.loading = false;
  }
}
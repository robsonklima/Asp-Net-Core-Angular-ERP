import { Component, OnInit, ViewChild } from '@angular/core';
import { DashboardService } from 'app/core/services/dashboard.service';
import { DashboardViewEnum } from 'app/core/types/dashboard.types';
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
  selector: 'app-reincidencia-quadrimestre-filial',
  templateUrl: './reincidencia-quadrimestre-filial.component.html',
})
export class ReincidenciaQuadrimestreFilialComponent implements OnInit {
  @ViewChild("chart") chart: ChartComponent;
  public loading: boolean;
  public haveData: boolean;
  public usuarioSessao: UsuarioSessao;
  public chartOptions: Partial<ChartOptions>;

  private chartMax: number = 40;
  private meta: number = 35;
  private redColor: string = "#cc0000";
  private greenColor: string = "#009900";
  private blackColor: string = "#000000";

  constructor(
    private _dashboardService: DashboardService,
    protected _userService: UserService
  ) {
    this.usuarioSessao = JSON.parse(this._userService.userSession);
  }

  ngOnInit(): void {
    this.carregarGrafico();
  }

  public async carregarGrafico() {
    this.loading = true;

    let data = (await this._dashboardService
      .obterViewPorParametros({ dashboardViewEnum: DashboardViewEnum.REINCIDENCIA_QUADRIMESTRE_FILIAIS, codFilial: this.usuarioSessao.usuario.codFilial }).toPromise())
      .viewDashboardReincidenciaQuadrimestreFiliais;

    if (data?.length) {
      data = Enumerable.from(data).orderBy(ord => ord.anoMes).toArray();
      let labels = data.map(d => d.anoMes);
      let valoresColuna = data.map(d => d.percentual);
      let valoresLinha: number[] = [];
      valoresColuna.forEach(() => { valoresLinha.push(this.meta); });
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
          name: "Meta de reincidencia",
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
        text: '* Meta de reincidencia deve ser menor ou igual a ' + meta + '%'
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
            colors: this.blackColor,
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

import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { DashboardLabService } from 'app/core/services/dashboard-lab.service';
import _ from 'lodash';

import {
  ChartComponent,
  ApexAxisChartSeries,
  ApexChart,
  ApexXAxis,
  ApexDataLabels,
  ApexTooltip,
  ApexStroke
} from "ng-apexcharts";

export type ChartOptions = {
  series: ApexAxisChartSeries;
  chart: ApexChart;
  xaxis: ApexXAxis;
  stroke: ApexStroke;
  tooltip: ApexTooltip;
  dataLabels: ApexDataLabels;
};

@Component({
  selector: 'app-laboratorio-dashboard-itens-recebidos-separados',
  templateUrl: './laboratorio-dashboard-itens-recebidos-separados.component.html'
})
export class LaboratorioDashboardItensRecebidosSeparadosComponent implements AfterViewInit {
  @ViewChild("chart") chart: ChartComponent;
  public chartOptions: Partial<ChartOptions>;
  loading: boolean = true;

  constructor(
    private _dashboardLabService: DashboardLabService
  ) { }

  async ngAfterViewInit() {
    this.montarGrafico();
  }

  private async montarGrafico() {
    const data = await this._dashboardLabService
      .obterRecebidosReparados({ ano: 2022 })
      .toPromise();

    const meses = _.uniq(data.map(d => d.mesExtenso));
    const recebidos = data.filter(d => d.tipo == 'RECEBIDOS').map(d => d.qtd);

    console.log(recebidos, meses);
    
    // const reparos;
    // const sucatas;

    this.chartOptions = {
      series: [
        {
          name: "series1",
          data: [31, 40, 28, 51, 42, 109, 100]
        },
        {
          name: "series2",
          data: [11, 32, 45, 32, 34, 52, 41]
        }
      ],
      chart: {
        height: 350,
        type: "area"
      },
      dataLabels: {
        enabled: false
      },
      stroke: {
        curve: "smooth"
      },
      xaxis: {
        type: "datetime",
        categories: [
          "2018-09-19T00:00:00.000Z",
          "2018-09-19T01:30:00.000Z",
          "2018-09-19T02:30:00.000Z",
          "2018-09-19T03:30:00.000Z",
          "2018-09-19T04:30:00.000Z",
          "2018-09-19T05:30:00.000Z",
          "2018-09-19T06:30:00.000Z"
        ]
      },
      tooltip: {
        x: {
          format: "dd/MM/yy HH:mm"
        }
      }
    };

    this.loading = false;
  }
}

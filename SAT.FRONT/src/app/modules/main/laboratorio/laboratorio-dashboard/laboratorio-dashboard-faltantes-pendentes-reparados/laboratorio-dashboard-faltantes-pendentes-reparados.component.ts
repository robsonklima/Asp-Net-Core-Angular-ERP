import { Component, OnInit, ViewChild } from '@angular/core';
import { DashboardLabService } from 'app/core/services/dashboard-lab.service';
import { ViewDashboardLabTopFaltantes } from 'app/core/types/dashboard-lab.types';

import {
  ApexAxisChartSeries,
  ApexChart,
  ChartComponent,
  ApexDataLabels,
  ApexPlotOptions,
  ApexYAxis,
  ApexLegend,
  ApexStroke,
  ApexXAxis,
  ApexFill,
  ApexTooltip
} from "ng-apexcharts";

export type ChartOptions = {
  series: ApexAxisChartSeries;
  chart: ApexChart;
  dataLabels: ApexDataLabels;
  plotOptions: ApexPlotOptions;
  yaxis: ApexYAxis;
  xaxis: ApexXAxis;
  fill: ApexFill;
  tooltip: ApexTooltip;
  stroke: ApexStroke;
  legend: ApexLegend;
};

@Component({
  selector: 'app-laboratorio-dashboard-faltantes-pendentes-reparados',
  templateUrl: './laboratorio-dashboard-faltantes-pendentes-reparados.component.html'
})
export class LaboratorioDashboardFaltantesPendentesReparadosComponent implements OnInit {
  @ViewChild("chart") chart: ChartComponent;
  public chartOptions: Partial<ChartOptions>;
  loading: boolean = true;

  constructor(
    private _dashboardLabService: DashboardLabService
  ) { }

  ngOnInit(): void {
    this.montarGrafico();
  }

  private async montarGrafico() {
    const data: ViewDashboardLabTopFaltantes[] = await this._dashboardLabService
      .obterTopFaltantes({})
      .toPromise();

    const labels = data.map(d => d.nomePecaAbrev);
    const qtd = data.map(d => d.qtd);
    const qtdHoras = data.map(d => d.qtdhoras);

    this.chartOptions = {
      series: [
        {
          name: "Quantidade",
          data: qtd
        },
        {
          name: "Qtd Horas",
          data: qtdHoras
        }
      ],
      chart: {
        type: "bar",
        height: 350
      },
      plotOptions: {
        bar: {
          horizontal: false,
          columnWidth: "55%"
        }
      },
      dataLabels: {
        enabled: false
      },
      stroke: {
        show: true,
        width: 2,
        colors: ["transparent"]
      },
      xaxis: {
        categories: labels
      },
      yaxis: {
        title: {
          text: "$ (thousands)"
        }
      },
      fill: {
        opacity: 1
      },
      tooltip: {
        y: {
          formatter: function(val) {
            return "$ " + val + " thousands";
          }
        }
      }
    };

    this.loading = false;
  }
}

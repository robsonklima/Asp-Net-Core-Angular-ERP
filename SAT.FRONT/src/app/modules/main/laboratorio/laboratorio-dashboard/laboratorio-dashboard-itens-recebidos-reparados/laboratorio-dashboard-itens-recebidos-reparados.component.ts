import { AfterViewInit, ChangeDetectorRef, Component, OnInit, ViewChild } from '@angular/core';
import { DashboardLabService } from 'app/core/services/dashboard-lab.service';
import _ from 'lodash';
import moment from 'moment';

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
  selector: 'app-laboratorio-dashboard-itens-recebidos-reparados',
  templateUrl: './laboratorio-dashboard-itens-recebidos-reparados.component.html'
})
export class LaboratorioDashboardItensRecebidosReparadosComponent implements AfterViewInit {
  @ViewChild("chart") chart: ChartComponent;
  public chartOptions: Partial<ChartOptions>;
  loading: boolean = true;

  constructor(
    private _dashboardLabService: DashboardLabService,
    private _cdr: ChangeDetectorRef
  ) { }

  async ngAfterViewInit() {
    this.montarGrafico();
  }

  private async montarGrafico() {
    const data = await this._dashboardLabService.obterRecebidosReparados({  }).toPromise();
    const labels = data.map(d => d.anoMes);
    const sucatas = data.filter(d => d.tipo.toLowerCase() == 'sucatas').map(d => d.qtd)
    const recebidos = data.filter(d => d.tipo.toLowerCase() == 'recebidos').map(d => d.qtd)
    const reparos = data.filter(d => d.tipo.toLowerCase() == 'reparos').map(d => d.qtd)

    this.chartOptions = {
      series: [
        {
          name: "Sucatas",
          data: sucatas
        },
        {
          name: "Recebidos",
          data: recebidos
        },
        {
          name: "Reparos",
          data: reparos
        }
      ],
      chart: {
        type: "bar",
        height: 350
      },
      plotOptions: {
        bar: {
          horizontal: false,
          columnWidth: "55%",
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
          text: ""
        }
      },
      fill: {
        opacity: 1
      },
      tooltip: {
        y: {
          formatter: function(val) {
            return "" + val + "";
          }
        }
      }
    };

    this.loading = false;
    this._cdr.detectChanges();
  }
}

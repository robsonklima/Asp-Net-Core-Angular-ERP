import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { DashboardLabService } from 'app/core/services/dashboard-lab.service';
import _ from 'lodash';
import moment from 'moment';

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
      .obterRecebidosReparados({ ano: +moment().format('YYYY') })
      .toPromise();

    const meses = _.uniq(_.orderBy(data, ['mes'], ['asc']).map(d => d.mesExtenso));
    const recebidos = data.filter(d => d.tipo == 'RECEBIDOS').map(d => d.qtd);
    const reparados = data.filter(d => d.tipo == 'REPAROS').map(d => d.qtd);
    const sucatas = data.filter(d => d.tipo == 'SUCATAS').map(d => d.qtd);

    this.chartOptions = {
      series: [
        {
          name: "Recebidos",
          data: recebidos
        },
        {
          name: "Reparos",
          data: reparados
        },
        {
          name: "Sucata",
          data: sucatas
        }
      ],
      chart: {
        height: 450,
        type: "area"
      },
      dataLabels: {
        enabled: true
      },
      stroke: {
        curve: "smooth"
      },
      xaxis: {
        type: "category",
        categories: meses
      }
    };

    this.loading = false;
  }
}

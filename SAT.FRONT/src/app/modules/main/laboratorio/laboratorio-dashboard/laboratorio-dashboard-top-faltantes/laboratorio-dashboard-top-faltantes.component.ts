import { ChangeDetectorRef, Component, OnInit, ViewChild } from '@angular/core';
import { DashboardLabService } from 'app/core/services/dashboard-lab.service';
import {
  ApexAxisChartSeries,
  ApexChart,
  ChartComponent,
  ApexDataLabels,
  ApexXAxis,
  ApexPlotOptions,
  ApexStroke,
  ApexTitleSubtitle,
  ApexYAxis,
  ApexTooltip,
  ApexFill,
  ApexLegend
} from "ng-apexcharts";

export type ChartOptions = {
  series: ApexAxisChartSeries;
  chart: ApexChart;
  dataLabels: ApexDataLabels;
  plotOptions: ApexPlotOptions;
  xaxis: ApexXAxis;
  yaxis: ApexYAxis;
  stroke: ApexStroke;
  title: ApexTitleSubtitle;
  tooltip: ApexTooltip;
  fill: ApexFill;
  legend: ApexLegend;
};

@Component({
  selector: 'app-laboratorio-dashboard-top-faltantes',
  templateUrl: './laboratorio-dashboard-top-faltantes.component.html'
})
export class LaboratorioDashboardTopFaltantesComponent implements OnInit {
  @ViewChild("chart") chart: ChartComponent;
  public chartOptions: Partial<ChartOptions>;
  loading: boolean = true;

  constructor(
    private _dashboardLabService: DashboardLabService,
    private _cdr: ChangeDetectorRef
  ) { }

  ngOnInit(): void {
    this.montarGrafico();
  }

  private async montarGrafico() {
    const data = await this._dashboardLabService.obterTopFaltantes({}).toPromise();
    const labels = data.map(d => d.nomePecaAbrev);
    const qtdFaltante = data.map(d => d.qtdFaltante);
    const qtdPendente = data.map(d => d.qtdPendente);
    const qtdEmReparo = data.map(d => d.qtdEmReparo);

    this.chartOptions = {
      series: [
        {
          name: "Quantidade Faltante",
          data: qtdFaltante
        },
        {
          name: "Quantidade Pendente",
          data: qtdPendente
        },
        {
          name: "Quantidade em Reparo",
          data: qtdEmReparo
        }
      ],
      chart: {
        type: "bar",
        height: 350,
        stacked: true
      },
      plotOptions: {
        bar: {
          horizontal: true
        }
      },
      stroke: {
        width: 1,
        colors: ["#fff"]
      },
      xaxis: {
        categories: labels,
        labels: {
          formatter: function(val) {
            return val + "";
          }
        }
      },
      yaxis: {
        title: {
          text: undefined
        }
      },
      tooltip: {
        y: {
          formatter: function(val) {
            return val + "";
          }
        }
      },
      fill: {
        opacity: 1
      },
      legend: {
        position: "top",
        horizontalAlign: "left",
        offsetX: 40
      }
    };

    this.loading = false;
    this._cdr.detectChanges();
  }
}

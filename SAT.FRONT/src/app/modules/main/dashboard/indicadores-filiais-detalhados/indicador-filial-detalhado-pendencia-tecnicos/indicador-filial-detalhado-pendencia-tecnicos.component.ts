import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { DashboardService } from 'app/core/services/dashboard.service';
import { DashboardViewEnum } from 'app/core/types/dashboard.types';
import { UserService } from 'app/core/user/user.service';
import { UserSession } from 'app/core/user/user.types';
import {
  ApexAxisChartSeries,
  ApexChart,
  ChartComponent,
  ApexTitleSubtitle,
  ApexDataLabels,
  ApexStroke,
  ApexYAxis,
  ApexXAxis,
  ApexPlotOptions,
  ApexTooltip,
  ApexLegend
} from "ng-apexcharts";

export type ChartOptions = {
  series: ApexAxisChartSeries;
  chart: ApexChart;
  xaxis: ApexXAxis;
  stroke: ApexStroke;
  dataLabels: ApexDataLabels;
  plotOptions: ApexPlotOptions;
  yaxis: ApexYAxis;
  tooltip: ApexTooltip;
  colors: string[];
  title: ApexTitleSubtitle;
  subtitle: ApexTitleSubtitle;
  legend: ApexLegend
};

@Component({
  selector: 'app-indicador-filial-detalhado-pendencia-tecnicos',
  templateUrl: './indicador-filial-detalhado-pendencia-tecnicos.component.html'
})
export class IndicadorFilialDetalhadoPendenciaTecnicosComponent implements OnInit {
  @ViewChild("chart") chart: ChartComponent;
  public tecnicoChart: Partial<ChartOptions>;
  loading: boolean = true;
  @Input() codFilial;
  ordemCrescente: boolean = false;

  constructor(
    private _dashboardService: DashboardService
  ) { }

  async ngOnInit() {
    this.carregarDados();
  }

  public async carregarDados(reordenar: boolean = false) {
    if (reordenar) this.ordemCrescente = !this.ordemCrescente;

    const data = await this._dashboardService.obterViewPorParametros({
      dashboardViewEnum: DashboardViewEnum.INDICADORES_DETALHADOS_PENDENCIA_TECNICO,
      codFilial: this.codFilial
    }).toPromise();

    let pendenciaTecnico = data.viewDashboardIndicadoresDetalhadosPendenciaTecnico;

    if (this.ordemCrescente)
      pendenciaTecnico = pendenciaTecnico
        .sort((a, b) => (a.percentual > b.percentual) ? 1 : -1)
        .slice(0, 10);
    else
      pendenciaTecnico = pendenciaTecnico
        .sort((a, b) => (a.percentual < b.percentual) ? 1 : -1)
        .slice(0, 10);

    const labels = pendenciaTecnico.map(s => s.nomeTecnico.trim());
    const values = pendenciaTecnico.map(s => s.percentual);
    const colors = pendenciaTecnico.map(s => s.percentual > 5 ? '#F44336' : '#4CAF50');

    this.tecnicoChart = {
      series: [
        {
          data: values
        }
      ],
      chart: { type: "bar", height: 320, toolbar: { show: false } },
      plotOptions: {
        bar: {
          barHeight: "100%",
          distributed: true,
          horizontal: true,
          dataLabels: {
            position: "bottom"
          }
        },
      },
      colors: colors,
      legend: {
        show: false
      },
      dataLabels: {
        enabled: true,
        textAnchor: "start",
        style: {
          colors: ["#212121"]
        },
        formatter: function (val, opt) {
          return opt.w.globals.labels[opt.dataPointIndex] + ":  " + val + "%";
        },
        offsetX: 0,
        dropShadow: {
          enabled: false
        }
      },
      stroke: {
        width: 1,
        colors: ["#fff"]
      },
      xaxis: {
        categories: labels,
        labels: {
          show: true
        },
      },
      yaxis: {
        labels: {
          show: false,
        },
      },
      tooltip: {
        theme: "dark",
        x: {
          show: false
        },
        y: {
          title: {
            formatter: () => {
              return "";
            }
          }
        }
      },
    };

    this.loading = false;
  }
}

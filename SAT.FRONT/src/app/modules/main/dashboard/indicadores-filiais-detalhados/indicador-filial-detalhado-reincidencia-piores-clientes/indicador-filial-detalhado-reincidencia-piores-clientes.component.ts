import { Component, OnInit, ViewChild } from '@angular/core';
import { DashboardService } from 'app/core/services/dashboard.service';
import { DashboardViewEnum } from 'app/core/types/dashboard.types';
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
  ApexTooltip
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
};

@Component({
  selector: 'app-indicador-filial-detalhado-reincidencia-piores-clientes',
  templateUrl: './indicador-filial-detalhado-reincidencia-piores-clientes.component.html'
})
export class IndicadorFilialDetalhadoReincidenciaPioresClientesComponent implements OnInit {
  @ViewChild("chart") chart: ChartComponent;
  public clienteChart: Partial<ChartOptions>;
  loading: boolean = true;

  constructor(
    private _dashboardService: DashboardService
  ) { }

  async ngOnInit() {
    const data = await this._dashboardService.obterViewPorParametros({ 
        dashboardViewEnum: DashboardViewEnum.INDICADORES_DETALHADOS_REINCIDENCIA_CLIENTE,
        codFilial: 4
      }).toPromise();

    const slaRegiao = data.viewDashboardIndicadoresDetalhadosReincidenciaCliente
      .sort((a, b) => (a.percentual < b.percentual) ? 1 : -1)
      .filter(s => s.percentual > 0)
      .slice(0, 10);  
    
    const labels = slaRegiao.map(s => s.nomeFantasia);
    const values = slaRegiao.map(s => s.percentual);
    
    this.clienteChart = {
      series: [{ data: values }],
      chart: { type: "bar", height: 320, toolbar: { show: false }},
      plotOptions: {
        bar: {
          barHeight: "100%",
          distributed: false,
          horizontal: true,
          dataLabels: {
            position: "bottom"
          }
        }
      },
      dataLabels: {
        enabled: true,
        textAnchor: "start",
        style: {
          colors: ["#fff"]
        },
        formatter: function(val, opt) {
          return opt.w.globals.labels[opt.dataPointIndex] + ":  " + val + "%";
        },
        offsetX: 0,
        dropShadow: {
          enabled: true
        }
      },
      stroke: {
        width: 1,
        colors: ["#fff"]
      },
      xaxis: {
        categories: labels
      },
      yaxis: {
        labels: {
          show: false
        }
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
      }
    };

    this.loading = false;
  }
}

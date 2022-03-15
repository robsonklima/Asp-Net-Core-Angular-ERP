import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { DashboardService } from 'app/core/services/dashboard.service';
import { DashboardViewEnum, ViewDashboardIndicadoresDetalhadosPerformanceTipoEnum } from 'app/core/types/dashboard.types';
import { UserService } from 'app/core/user/user.service';
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
  selector: 'app-indicador-filial-detalhado-performance',
  templateUrl: './indicador-filial-detalhado-performance.component.html'
})
export class IndicadorFilialDetalhadoPerformanceComponent implements OnInit {
  @ViewChild("chart") chart: ChartComponent;
  public chartOptions: Partial<ChartOptions>;
  @Input() codFilial: number;
  loading: boolean;
  
  constructor(
    protected _userService: UserService,
    private _dashboardService: DashboardService
  ) { }

  async ngOnInit() {
    const data = await this._dashboardService.obterViewPorParametros({ 
      dashboardViewEnum: DashboardViewEnum.INDICADORES_DETALHADOS_PERFORMANCE,
      codFilial: this.codFilial
    }).toPromise();

    const performance = data.viewDashboardIndicadoresDetalhadosPerformance;
    const meses = [...new Set(performance.map(p => this.formatarAnoMes(p.anoMes)))];
    const series: any = [
      {
        name: 'Pendência',
        data: performance.filter(p => p.tipo === ViewDashboardIndicadoresDetalhadosPerformanceTipoEnum.PENDENCIA).map(p => p.percentual)
      },
      {
        name: 'Reincidência',
        data: performance.filter(p => p.tipo === ViewDashboardIndicadoresDetalhadosPerformanceTipoEnum.REINCIDENCIA).map(p => p.percentual)
      },
      {
        name: 'SLA',
        data: performance.filter(p => p.tipo === ViewDashboardIndicadoresDetalhadosPerformanceTipoEnum.SLA).map(p => p.percentual)
      },
    ];

    this.chartOptions = {
      series: series,
      chart: { type: "bar", height: 320, toolbar: { show: false }},
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
        categories: meses
      },
      fill: {
        opacity: 1
      },
      tooltip: {
        y: {
          formatter: (val) => {
            return val + "%";
          }
        }
      },
      legend: {
        show: false
      }
    };
  }

  private formatarAnoMes(anoMes: string): string {
    switch (anoMes.slice(-2)) {
      case '01':
        return 'Janeiro';

      case '02':
        return 'Fevereiro';
    
      case '03':
        return 'Março';

      case '04':
        return 'Abril';

      case '05':
        return 'maio';

      case '06':
        return 'Junho';

      case '07':
        return 'Julho';

      case '08':
        return 'Agosto';

      case '09':
        return 'Setembro';

      case '10':
        return 'Outubro';

      case '11':
        return 'Novembro';

      case '12':
        return 'Dezembro';
      
      default:
        return ''
    }
  }
}

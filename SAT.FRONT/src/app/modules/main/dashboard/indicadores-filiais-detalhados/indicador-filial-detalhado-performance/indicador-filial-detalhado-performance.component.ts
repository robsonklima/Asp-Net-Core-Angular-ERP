import { Component, OnInit, ViewChild } from '@angular/core';
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
  
  constructor(
    protected _userService: UserService
  ) { }

  ngOnInit(): void {
    this.inicializarGrafico();
  }

  private inicializarGrafico() {
    this.chartOptions = {
      series: [
        {
          name: "Net Profit",
          data: [44, 55, 57]
        },
        {
          name: "Revenue",
          data: [76, 85, 101]
        },
        {
          name: "Free Cash Flow",
          data: [35, 41, 36]
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
        categories: [
          "Feb",
          "Mar",
          "Apr"
        ]
      },
      yaxis: {
        title: {
          text: "Percentual"
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
  }
}

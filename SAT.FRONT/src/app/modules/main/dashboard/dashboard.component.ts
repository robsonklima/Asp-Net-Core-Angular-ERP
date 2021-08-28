import { AfterViewInit, Component, ViewChild } from '@angular/core';
import { IndicadorService } from 'app/core/services/indicador.service';
import {
  ApexChart,
  ApexAxisChartSeries,
  ChartComponent,
  ApexDataLabels,
  ApexPlotOptions,
  ApexYAxis,
  ApexLegend,
  ApexGrid
} from "ng-apexcharts";

type ApexXAxis = {
  type?: "category" | "datetime" | "numeric";
  categories?: any;
  labels?: {
    style?: {
      colors?: string | string[];
      fontSize?: string;
    };
  };
};

export type ChartOptions = {
  series: ApexAxisChartSeries;
  chart: ApexChart;
  dataLabels: ApexDataLabels;
  plotOptions: ApexPlotOptions;
  yaxis: ApexYAxis;
  xaxis: ApexXAxis;
  grid: ApexGrid;
  colors: string[];
  legend: ApexLegend;
};

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html'
})
export class DashboardComponent implements AfterViewInit {
  @ViewChild("chart") chart: ChartComponent;
  chartOptions: Partial<ChartOptions>;
  loading: boolean = true;
  
  constructor(
    private _indicadorService: IndicadorService
  ) {}

  async ngAfterViewInit() {
    this.chartOptions = {
      series: [
        {
          name: "Chamados",
          data: []
        }
      ],
      chart: {
        height: 350,
        toolbar: {
          show: false
        },
        type: "bar",
        events: {
          click: function(chart, w, e) {}
        }
      },
      colors: [],
      plotOptions: {
        bar: {
          columnWidth: "45%",
          distributed: true
        }
      },
      dataLabels: {
        enabled: false
      },
      legend: {
        show: false
      },
      grid: {
        show: false
      },
      xaxis: {
        categories: [],
        labels: {
          style: {
            colors: '#616161',
            fontSize: "12px"
          }
        }
      }
    };
  }
}

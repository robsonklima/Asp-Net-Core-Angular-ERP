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
  selector: 'app-sla-cliente',
  templateUrl: './sla-cliente.component.html'
})
export class SlaClienteComponent implements AfterViewInit {
  @ViewChild("chart") chart: ChartComponent;
  chartOptions: Partial<ChartOptions>;
  data: any[] = [];
  clientes: string[] = [];
  chamados: number[] = [];
  colors: string[] = ['#F44336', '#E91E63', '#9C27B0', '#D50000', '#CE93D8', '#4A148C', '#D500F9', '#EC407A', '#DAF7A6']

  constructor(
    private _indicadorService: IndicadorService
  ) {}

  async ngAfterViewInit() {
    this.data = await this._indicadorService.obter().toPromise();
    this.data = this.data.filter(c => c.qtdOS > 400);

    this.data = this.data.sort((a, b) => (a.qtdOS < b.qtdOS) ? 1 : ((b.qtdOS < a.qtdOS) ? -1 : 0));

    this.clientes = this.data.map(c => c.cliente.split(" ")[0]);
    this.chamados = this.data.map(c => c.qtdOS);

    this.chartOptions = {
      series: [
        {
          name: "Chamados",
          data: this.chamados
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
      colors: this.colors,
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
        categories: this.clientes,
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

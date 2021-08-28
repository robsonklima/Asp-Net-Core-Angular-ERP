import { AfterViewInit, ChangeDetectorRef, Component, ViewChild } from '@angular/core';
import { Indicador } from 'app/core/types/indicador.types';
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
  data: Indicador[] = [];
  loading: boolean = true;
  
  constructor(
    private _cdr: ChangeDetectorRef
  ) {}

  async ngAfterViewInit() {
   this.popularGrafico();
  }

  popularGrafico(): void {
    this.data = this.obterDados(); // --> Carrega dados da API

    const tipos: string[] = [];
    const val: number[] = [];

    this.data.shift().filho.shift().filho.shift().filho.map(i => { // --> Seleciona os dados para compor o grafico
      tipos.push(i.nome);
      val.push(i.valor);
    });

    this.chartOptions = {
      series: [
        {
          name: "Chamados",
          data: val
        }
      ],
      chart: {
        height: 350,
        toolbar: {
          show: false
        },
        type: "bar",
        events: {
          click: function(chart, w, e) {
            console.log(e);
          }
        }
      },
      colors: ['#1976D2'],
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
        categories: tipos,
        labels: {
          style: {
            colors: '#616161',
            fontSize: "12px"
          }
        }
      }
    };

    this.loading = false;
    this._cdr.detectChanges();
  }

  private obterDados(): Indicador[] {
    var data: Indicador[] = [
      {
        nome: "OS",
        valor: 250,
        filho: [
          {
            nome: "Corretiva",
            valor: 100,
            filho: [
              {
                nome: "Aberto",
                valor: 24,
                filho: [
                  {
                    nome: "FRS",
                    valor: 5
                  },
                  {
                    nome: "FSC",
                    valor: 9
                  },
                  {
                    nome: "FSP",
                    valor: 10
                  }
                ]
              },
              {
                nome: "Fechado",
                valor: 67,
                filho: [
                  {
                    nome: "FRS",
                    valor: 27
                  },
                  {
                    nome: "FSC",
                    valor: 10
                  },
                  {
                    nome: "FSP",
                    valor: 37
                  }
                ]
              }
            ]
          },
          {
            nome: "Instalação",
            valor: 90,
            filho: [
              {
                nome: "Aberto",
                valor: 55,
                filho: [
                  {
                    nome: "FRS",
                    valor: 5
                  },
                  {
                    nome: "FSC",
                    valor: 15
                  },
                  {
                    nome: "FSP",
                    valor: 35
                  }
                ]
              },
              {
                nome: "Fechado",
                valor: 23,
                filho: [
                  {
                    nome: "FRS",
                    valor: 3
                  },
                  {
                    nome: "FSC",
                    valor: 10
                  },
                  {
                    nome: "FSP",
                    valor: 10
                  }
                ]
              }
            ]
          },
          {
            nome: "Preventiva",
            valor: 60,
            filho: [
              {
                nome: "Aberto",
                valor: 49,
                filho: [
                  {
                    nome: "FRS",
                    valor: 19
                  },
                  {
                    nome: "FSC",
                    valor: 10
                  },
                  {
                    nome: "FSP",
                    valor: 20
                  }
                ]
              },
              {
                nome: "Fechado",
                valor: 32,
                filho: [
                  {
                    nome: "FRS",
                    valor: 2
                  },
                  {
                    nome: "FSC",
                    valor: 11
                  },
                  {
                    nome: "FSP",
                    valor: 19
                  }
                ]
              }
            ]
          }
        ]
      }
    ];

    return data;
  }
}

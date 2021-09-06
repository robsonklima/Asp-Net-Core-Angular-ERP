import { Component, Input, OnChanges, ViewChild } from "@angular/core";
import { IndicadorService } from "app/core/services/indicador.service";
import { IndicadorAgrupadorEnum, IndicadorParameters, IndicadorTipoEnum } from "app/core/types/indicador.types";
import { UsuarioSessao } from "app/core/types/usuario.types";
import { UserService } from "app/core/user/user.service";
import moment from "moment";
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
  selector: "app-grafico-spa-cliente",
  templateUrl: "./grafico-spa-cliente.component.html"
})
export class GraficoSPAClienteComponent implements OnChanges {
  @ViewChild("chart") chart: ChartComponent;
  @Input() filtro: any;
  usuarioSessao: UsuarioSessao;
  public chartOptions: Partial<ChartOptions>;
  isLoading: boolean;
  haveData: boolean;

  constructor(
    private _indicadorService: IndicadorService,
    private _userService: UserService
  ) {
    this.usuarioSessao = JSON.parse(this._userService.userSession);
  }

  async ngOnInit() {
    
  }

  ngOnChanges() {
    if (this.filtro) {
      this.carregarGrafico();
    }
  }

  public async carregarGrafico() {
    this.isLoading = true;

    const params: IndicadorParameters = {
      ...{
        agrupador: IndicadorAgrupadorEnum.CLIENTE,
        tipo: IndicadorTipoEnum.SPA,
        codTiposIntervencao: "1,2,3,4,6,7",
        codAutorizadas: "8, 10, 13, 40, 48, 102, 108, 119, 130, 132, 141, 169, 172, 177, 178, 182, 183, 189, 190, 191, 192, 202",
        codTiposGrupo: "1,3,5,7,8,9,10,11"
      },
      ...this.filtro?.parametros
    }

    const data = await this._indicadorService.obterPorParametros(params).toPromise();

    if (data?.length) {
      const labels = data.map(d => d.label.split(" ").shift());
      const valores = data.map(d => d.valor);
      const cores = data.map(d => d.valor <= 95 ? "#FF4560" : "#26a69a");
      this.haveData = true;
      this.inicializarGrafico(labels, valores, cores);
    } 

    this.isLoading = false;
  }

  private inicializarGrafico(labels: string[], valores: number[], cores: string[]) {
    this.chartOptions = {
      series: [
        {
          name: "Percentual",
          data: valores
        }
      ],
      chart: {
        height: 350,
        type: "bar",
        events: {
          click: function(chart, w, e) {}
        }
      },
      colors: cores,
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
        categories: labels,
        labels: {
          style: {
            colors: cores,
            fontSize: "10px"
          }
        }
      },
      yaxis: {
        max: 100,
        labels: {
          formatter: (value) => {
            return (value + "%").replace('.', ',');
          }
        }
      }
    };
  }
}

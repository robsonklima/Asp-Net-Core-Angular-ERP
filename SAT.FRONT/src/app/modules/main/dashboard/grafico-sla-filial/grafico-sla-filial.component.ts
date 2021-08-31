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
  selector: "app-grafico-sla-filial",
  templateUrl: "./grafico-sla-filial.component.html"
})
export class GraficoSLAFilialComponent implements OnChanges {
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
    this.configurarFiltro();
    this.carregarGrafico();
  }

  public async carregarGrafico() {
    this.isLoading = true;

    const params: IndicadorParameters = {
      ...{
        agrupador: IndicadorAgrupadorEnum.FILIAL,
        tipo: IndicadorTipoEnum.SLA,
      },
      ...this.filtro?.parametros
    }

    if (!params.dataInicio || !params.dataFim) {
      params.dataInicio = moment().startOf('month').format('YYYY-MM-DD 00:00');
      params.dataFim = moment().endOf('month').format('YYYY-MM-DD 23:59');
    }

    if (this.filtro) {
      const data = await this._indicadorService.obterPorParametros(params).toPromise();

      if (data.length) {
        const labels = data.map(d => d.label);
        const valores = data.map(d => d.valor);
        const cores = data.map(d => d.valor <= 95 ? "#FF4560" : "#26a69a");
        this.haveData = true;
        this.inicializarGrafico(labels, valores, cores);
      } 

      this.isLoading = false;
    }
  }

  public configurarFiltro(): void {
    if (!this.filtro) {
        return;
    }

    // Filtro obrigatorio de filial quando o usuario esta vinculado a uma filial
    if (this.usuarioSessao?.usuario?.codFilial) {
        this.filtro.parametros.codFiliais = [this.usuarioSessao.usuario.codFilial]
    }

    Object.keys(this.filtro?.parametros).forEach((key) => {
        if (this.filtro.parametros[key] instanceof Array) {
            this.filtro.parametros[key] = this.filtro.parametros[key].join()
        };
    });
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
        show: true
      },
      xaxis: {
        categories: labels,
        labels: {
          style: {
            colors: cores,
            fontSize: "12px"
          },
        }
      },
      yaxis: {
        max: 100
      }
    };
  }
}

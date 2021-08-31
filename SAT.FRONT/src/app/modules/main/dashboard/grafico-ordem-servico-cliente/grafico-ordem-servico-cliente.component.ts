import { ChangeDetectorRef, Component, Input, OnChanges, ViewChild } from "@angular/core";
import { IndicadorService } from "app/core/services/indicador.service";
import { IndicadorAgrupadorEnum, IndicadorParameters, IndicadorTipoEnum } from "app/core/types/indicador.types";
import { UsuarioSessao } from "app/core/types/usuario.types";
import { UserService } from "app/core/user/user.service";
import moment from "moment";
import {
  ApexAxisChartSeries,
  ApexChart,
  ChartComponent,
  ApexDataLabels,
  ApexPlotOptions,
  ApexYAxis,
  ApexTitleSubtitle,
  ApexXAxis,
  ApexFill
} from "ng-apexcharts";

export type ChartOptions = {
  series: ApexAxisChartSeries;
  chart: ApexChart;
  dataLabels: ApexDataLabels;
  plotOptions: ApexPlotOptions;
  yaxis: ApexYAxis;
  xaxis: ApexXAxis;
  fill: ApexFill;
  title: ApexTitleSubtitle;
};

@Component({
  selector: "app-grafico-ordem-servico-cliente",
  templateUrl: "./grafico-ordem-servico-cliente.component.html"
})
export class GraficoOrdemServicoClienteComponent implements OnChanges {
  @ViewChild("chart") chart: ChartComponent;
  @Input() filtro: any;
  usuarioSessao: UsuarioSessao;
  public chartOptions: Partial<ChartOptions>;
  isLoading: boolean;
  haveData: boolean;

  constructor(
    private _indicadorService: IndicadorService,
    private _userService: UserService,
    private _cdr: ChangeDetectorRef
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
        agrupador: IndicadorAgrupadorEnum.CLIENTE,
        tipo: IndicadorTipoEnum.ORDEM_SERVICO,
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
        this.haveData = true;
        this.inicializarGrafico(labels, valores);
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

  private inicializarGrafico(labels: string[], valores: number[]) {
    this.chartOptions = {
      series: [
        {
          name: "Chamados",
          data: valores
        }
      ],
      chart: {
        height: 350,
        type: "bar"
      },
      plotOptions: {
        bar: {
          dataLabels: {
            position: "top"
          }
        }
      },
      dataLabels: {
        enabled: true,
        formatter: function(val) {
          return val + "";
        },
        offsetY: -20,
        style: {
          fontSize: "12px",
          colors: ["#304758"]
        }
      },

      xaxis: {
        categories: labels,
        position: "top",
        labels: {
          offsetY: -18
        },
        axisBorder: {
          show: false
        },
        axisTicks: {
          show: false
        },
        crosshairs: {
          fill: {
            type: "gradient",
            gradient: {
              colorFrom: "#D8E3F0",
              colorTo: "#BED1E6",
              stops: [0, 100],
              opacityFrom: 0.4,
              opacityTo: 0.5
            }
          }
        },
        tooltip: {
          enabled: true,
          offsetY: -35
        }
      },
      fill: {
        type: "gradient",
        gradient: {
          shade: "light",
          type: "horizontal",
          shadeIntensity: 0.25,
          gradientToColors: undefined,
          inverseColors: true,
          opacityFrom: 1,
          opacityTo: 1,
          stops: [50, 0, 100, 100]
        }
      },
      yaxis: {
        axisBorder: {
          show: false
        },
        axisTicks: {
          show: false
        },
        labels: {
          show: false,
          formatter: function(val) {
            return val + "";
          }
        }
      },
      title: {
        text: "Título do Gráfico",
        offsetY: 320,
        align: "center",
        style: {
          color: "#444"
        }
      }
    };
  }
}
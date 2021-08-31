import { Component, Input, OnChanges, ViewChild } from "@angular/core";
import { IndicadorService } from "app/core/services/indicador.service";
import { IndicadorAgrupadorEnum, IndicadorParameters, IndicadorTipoEnum } from "app/core/types/indicador.types";
import { UsuarioSessao } from "app/core/types/usuario.types";
import { UserService } from "app/core/user/user.service";
import moment from "moment";
import {
  ApexAxisChartSeries,
  ApexTitleSubtitle,
  ApexDataLabels,
  ApexChart,
  ApexPlotOptions,
  ApexLegend,
  ChartComponent
} from "ng-apexcharts";

export type ChartOptions = {
  series: ApexAxisChartSeries;
  chart: ApexChart;
  dataLabels: ApexDataLabels;
  title: ApexTitleSubtitle;
  plotOptions: ApexPlotOptions;
  legend: ApexLegend;
  colors: string[];
};

@Component({
  selector: "app-grafico-ordem-servico-filial",
  templateUrl: "./grafico-ordem-servico-filial.component.html"
})
export class GraficoOrdemServicoFilialComponent implements OnChanges {
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
        let gData: any[] = [];

        data.forEach(el => {
          gData.push({
            x: el.label,
            y: el.valor
          });
        });

        this.haveData = true;
        this.inicializarGrafico(gData);
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

  private inicializarGrafico(data: any[]) {
    this.chartOptions = {
      series: [
        {
          data: data
        }
      ],

      legend: {
        show: false
      },
      chart: {
        height: 350,
        type: "treemap"
      },
      title: {
        //text: "Multi-dimensional Treemap",
        align: "center"
      }
    };
  }
}
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

  ngOnChanges() {
    if (this.filtro) {
      this.carregarGrafico();
    }
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

    if (this.filtro) {
      const data = await this._indicadorService.obterPorParametros(params).toPromise();
      
      if (data?.length) {
        let gData: any[] = [];

        data.forEach(el => {
          gData.push({
            x: el.label,
            y: el.valor
          });
        });
        const cores = data.map(d => d.valor > 100 ? "#388E3C" : "#FFC107");

        this.haveData = true;
        this.inicializarGrafico(gData, cores);
      } 

      this.isLoading = false;
    }
  }

  private inicializarGrafico(data: any[], cores: string[]) {
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
        text: "Chamados por Filial",
        align: "center"
      },
      colors: cores,
      plotOptions: {
        treemap: {
          distributed: true,
          enableShades: false
        }
      }
    };
  }
}
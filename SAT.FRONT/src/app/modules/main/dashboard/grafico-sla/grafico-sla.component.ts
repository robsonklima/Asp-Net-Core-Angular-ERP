import { Component, Input, OnChanges, ViewChild } from "@angular/core";
import { IndicadorService } from "app/core/services/indicador.service";
import { IndicadorAgrupadorEnum, IndicadorParameters, IndicadorTipoEnum } from "app/core/types/indicador.types";
import { UsuarioSessao } from "app/core/types/usuario.types";
import { UserService } from "app/core/user/user.service";
import {
  ApexNonAxisChartSeries,
  ApexPlotOptions,
  ApexChart,
  ChartComponent
} from "ng-apexcharts";

export type ChartOptions = {
  series: ApexNonAxisChartSeries;
  chart: ApexChart;
  labels: string[];
  colors: string[];
  plotOptions: ApexPlotOptions;
};

@Component({
  selector: "app-grafico-sla",
  templateUrl: "./grafico-sla.component.html"
})
export class GraficoSLAComponent implements OnChanges {
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
        tipo: IndicadorTipoEnum.SLA,
      },
      ...this.filtro?.parametros
    }

    let data = await this._indicadorService.obterPorParametros(params).toPromise();
    
    if (data?.length) {
      const valor = data.map(d => d.valor).reduce((a, b) => a + b, 0);
      const cor = valor <= 95 ? "#FF4560" : "#26a69a";
      this.haveData = true;
      this.inicializarGrafico(valor / data.length ? data.length : 1, cor);
    } 

    this.isLoading = false;
  }

  private inicializarGrafico(valor: number, cor: string) {
    this.chartOptions = {
      series: [ valor ],
      chart: {
        height: 350,
        type: "radialBar"
      },
      plotOptions: {
        radialBar: {
          hollow: {
            size: "70%"
          }
        }
      },
      colors: [ cor ],
      labels: ["SLA"]
    };
  }
}

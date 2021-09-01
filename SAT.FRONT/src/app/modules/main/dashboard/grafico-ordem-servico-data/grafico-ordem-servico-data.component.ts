import { Component, Input, OnChanges, ViewChild } from "@angular/core";
import { IndicadorService } from "app/core/services/indicador.service";
import { IndicadorAgrupadorEnum, IndicadorParameters, IndicadorTipoEnum } from "app/core/types/indicador.types";
import moment from "moment";
import {
  ChartComponent,
  ApexAxisChartSeries,
  ApexChart,
  ApexXAxis,
  ApexDataLabels,
  ApexTooltip,
  ApexStroke
} from "ng-apexcharts";

export type ChartOptions = {
  series: ApexAxisChartSeries;
  chart: ApexChart;
  xaxis: ApexXAxis;
  stroke: ApexStroke;
  tooltip: ApexTooltip;
  dataLabels: ApexDataLabels;
};

@Component({
  selector: "app-grafico-ordem-servico-data",
  templateUrl: "./grafico-ordem-servico-data.component.html"
})
export class GraficoOrdemServicoDataComponent implements OnChanges {
  @Input() filtro: any;
  @ViewChild("chart") chart: ChartComponent;
  public chartOptions: Partial<ChartOptions>;
  isLoading: boolean;
  haveData: boolean;

  constructor(
    private _indicadorService: IndicadorService
  ) {}

  ngOnChanges() {
    this.carregarGrafico();
  }

  private async carregarGrafico() {
    this.isLoading = true;

    const params: IndicadorParameters = {
      ...{
        agrupador: IndicadorAgrupadorEnum.DATA,
        tipo: IndicadorTipoEnum.ORDEM_SERVICO,
      },
      ...this.filtro?.parametros
    }

    let data = await this._indicadorService.obterPorParametros(params).toPromise();
    
    if (data?.length) {
      const labels = data.map(d => moment(d.label, "DD/MM/YYYY").format('DD/MM'));
      const valores = data.map(d => d.valor);
      this.inicializarGrafico(labels, valores);
      this.haveData = true;
    }

    this.isLoading = false;
  }

  private inicializarGrafico(labels: string[], valores: number[]): void {
    this.chartOptions = {
      series: [
        {
          name: "Chamados",
          data: valores
        }
      ],
      chart: {
        height: 350,
        type: "area"
      },
      dataLabels: {
        enabled: false
      },
      stroke: {
        curve: "smooth"
      },
      xaxis: {
        type: "category",
        categories: labels
      },
    };
  }
}

import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { MatSidenav } from '@angular/material/sidenav';
import { Filterable } from 'app/core/filters/filterable';
import { IndicadorService } from 'app/core/services/indicador.service';
import { Filtro, IFilterable } from 'app/core/types/filtro.types';
import { IndicadorAgrupadorEnum, IndicadorParameters, IndicadorTipoEnum } from 'app/core/types/indicador.types';
import { OrdemServicoFilterEnum, OrdemServicoIncludeEnum } from 'app/core/types/ordem-servico.types';
import { UsuarioSessao } from 'app/core/types/usuario.types';
import { UserService } from 'app/core/user/user.service';
import Enumerable from 'linq';
import moment from 'moment';
import {
  ApexChart,
  ApexAxisChartSeries,
  ChartComponent,
  ApexDataLabels,
  ApexPlotOptions,
  ApexYAxis,
  ApexLegend,
  ApexGrid,
  ApexStates,
  ApexXAxis,
  ApexStroke
} from "ng-apexcharts";

export type ChartOptions =
  {
    series: ApexAxisChartSeries;
    chart: ApexChart;
    dataLabels: ApexDataLabels;
    plotOptions: ApexPlotOptions;
    yaxis: ApexYAxis | ApexYAxis[];
    xaxis: ApexXAxis;
    grid: ApexGrid;
    colors: any[];
    legend: ApexLegend;
    states: ApexStates;
    title: any;
    stroke: ApexStroke;
    labels: string[];
  };

@Component({
  selector: 'app-reincidencia-filiais',
  templateUrl: './reincidencia-filiais.component.html'
})
export class ReincidenciaFiliaisComponent extends Filterable implements OnInit, IFilterable {
  @Input() sidenav: MatSidenav;
  @ViewChild("chart") chart: ChartComponent;

  public loading: boolean;
  public haveData: boolean;
  public usuarioSessao: UsuarioSessao;
  public chartOptions: Partial<ChartOptions>;

  private chartMax: number = 40;
  private meta: number = 35;
  private redColor: string = "#cc0000";
  private greenColor: string = "#009900";

  constructor(
    private _indicadorService: IndicadorService,
    protected _userService: UserService) {
    super(_userService, 'dashboard-filtro');
    this.usuarioSessao = JSON.parse(this._userService.userSession);
  }

  ngOnInit(): void {
    this.carregarGrafico();
    this.registerEmitters();
  }

  registerEmitters(): void {
    this.sidenav.closedStart.subscribe(() => {
      this.onSidenavClosed();
      this.carregarGrafico();
    })
  }

  loadFilter(): void {
    super.loadFilter();
  }

  public async carregarGrafico() {
    this.loading = true;

    const params: IndicadorParameters =
    {
      agrupador: IndicadorAgrupadorEnum.FILIAL,
      tipo: IndicadorTipoEnum.REINCIDENCIA,
      include: OrdemServicoIncludeEnum.OS_RAT_FILIAL_PRAZOS_ATENDIMENTO,
      filterType: OrdemServicoFilterEnum.FILTER_INDICADOR,
      dataInicio: this.filter?.parametros.dataInicio || moment().startOf('month').format('YYYY-MM-DD hh:mm'),
      dataFim: this.filter?.parametros.dataFim || moment().endOf('month').format('YYYY-MM-DD hh:mm')
    }

    let data = await this._indicadorService.obterPorParametros(params).toPromise();

    if (data?.length) {
      data = Enumerable.from(data).orderByDescending(ord => ord.valor).toArray();
      let labels = data.map(d => d.label);
      let valoresColuna = data.map(d => (this.chartMax / 100) * d.valor);
      let valoresLinha: number[] = [];
      valoresColuna.forEach(element => { valoresLinha.push(this.meta); });
      this.haveData = true;

      this.inicializarGrafico(labels, valoresColuna, valoresLinha, this.meta, this.greenColor, this.redColor);
    }

    this.loading = false;
  }

  private inicializarGrafico(labels: string[], valoresColuna: number[], valoresLinha: number[], meta: number, greenColor: string, redColor: string) {
    this.chartOptions = {
      series: [
        {
          name: "Percentual",
          type: "column",
          data: valoresColuna
        },
        {
          name: "Meta de Reincidência",
          type: "line",
          data: valoresLinha,
          color: redColor
        }
      ],
      dataLabels:
      {
        enabled: false
      },
      title: {
        text: '* Meta de Reincidência deve ser menor ou igual a ' + meta + '%'
      },
      colors: [
        function ({ value }) {
          if (value < meta) {
            return greenColor;
          } else {
            return redColor;
          }
        }
      ],
      chart: {
        height: 350,
        type: "line",
        toolbar: {
          tools: {
            download: true,
            selection: false,
            zoom: false,
            zoomin: false,
            zoomout: false,
            pan: false,
            reset: false
          }
        }
      },
      stroke: {
        width: [0, 2]
      },
      grid:
      {
        show: true
      },
      states:
      {
        active:
        {
          filter:
          {
            type: "none"
          }
        }
      },
      xaxis:
      {
        categories: labels,
        labels:
        {
          style:
          {
            colors: this.greenColor,
            fontSize: "12px"
          }
        }
      },
      yaxis:
      {
        max: this.chartMax,
        min: 0,
        tickAmount: 8,
        labels:
        {
          formatter: (value) => {
            return (value + "%").replace('.', ',');
          }
        }
      }
    };
  }
}

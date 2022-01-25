import { Component, Input, OnInit, ViewChild } from "@angular/core";
import { MatSidenav } from "@angular/material/sidenav";
import { Filterable } from "app/core/filters/filterable";
import { IndicadorService } from "app/core/services/indicador.service";
import { Filtro, IFilterable } from "app/core/types/filtro.types";
import { IndicadorAgrupadorEnum, IndicadorParameters, IndicadorTipoEnum } from "app/core/types/indicador.types";
import { OrdemServicoFilterEnum, OrdemServicoIncludeEnum } from "app/core/types/ordem-servico.types";
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
  ApexGrid,
  ApexStates,
  ApexStroke
} from "ng-apexcharts";

export type ChartOptions =
  {
    series: ApexAxisChartSeries;
    chart: ApexChart;
    dataLabels: ApexDataLabels;
    plotOptions: ApexPlotOptions;
    yaxis: ApexYAxis;
    xaxis: any;
    grid: ApexGrid;
    colors: any[];
    legend: ApexLegend;
    states: ApexStates;
    stroke: ApexStroke;
    title: any;
  };

@Component({
  selector: 'app-sla-clientes',
  templateUrl: './sla-clientes.component.html'
})
export class SlaClientesComponent extends Filterable implements OnInit, IFilterable {
  @Input() sidenav: MatSidenav;
  @ViewChild("chart") chart: ChartComponent;
  public usuarioSessao: UsuarioSessao;
  public chartOptions: Partial<ChartOptions>;
  public loading: boolean;
  public haveData: boolean;

  private chartMax: number = 100;
  private meta: number = 95;
  private redColor: string = "#cc0000";
  private greenColor: string = "#009900";

  constructor(
    private _indicadorService: IndicadorService,
    protected _userService: UserService) {
    super(_userService, 'dashboard-filtro')
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
      agrupador: IndicadorAgrupadorEnum.CLIENTE,
      tipo: IndicadorTipoEnum.SLA,
      include: OrdemServicoIncludeEnum.OS_RAT_CLIENTE_PRAZOS_ATENDIMENTO,
      filterType: OrdemServicoFilterEnum.FILTER_INDICADOR,
      //dataInicio: this.filter?.parametros.dataInicio || moment().startOf('month').format('YYYY-MM-DD hh:mm'),
      //dataFim: this.filter?.parametros.dataFim || moment().endOf('month').format('YYYY-MM-DD hh:mm')
      dataInicio: moment().add(-30, 'days').format('YYYY-MM-DD 00:00'),
      dataFim: moment().format('YYYY-MM-DD 23:59')
    }

    let data = await this._indicadorService.obterPorParametros(params).toPromise();

    if (data?.length) {
      data = data.sort((a, b) => (a.valor > b.valor) ? 1 : ((b.valor > a.valor) ? -1 : 0));
      const labels = data.map(d => d.label);
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
          type: "bar",
          data: valoresColuna,
        },
        {
          name: "Meta de SLA",
          type: "line",
          data: valoresLinha,
          color: redColor
        }
      ],
      dataLabels:
      {
        enabled: false
      },
      colors: [
        function ({ value }) {
          if (value < meta) {
            return redColor;
          } else {
            return greenColor;
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
      title: {
        text: '* Meta de SLA deve ser maior ou igual a ' + meta + '%'
      },
      xaxis:
      {
        categories: labels,
        labels:
        {
          rotate: -45,
          rotateAlways: true,
          trim: true,
          style:
          {
            fontSize: "8px"
          }
        }
      },
      yaxis:
      {
        max: this.chartMax,
        min: 0,
        tickAmount: 10,
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
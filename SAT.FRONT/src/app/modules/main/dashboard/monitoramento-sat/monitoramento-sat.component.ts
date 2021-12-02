import { Component, OnInit, ViewChild } from '@angular/core';
import { MonitoramentoService } from 'app/core/services/monitoramento.service';
import { Monitoramento, MonitoramentoStorage } from 'app/core/types/monitoramento.type';
import { UsuarioSessao } from 'app/core/types/usuario.types';
import { UserService } from 'app/core/user/user.service';
import Enumerable from 'linq';
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
  selector: 'app-monitoramento-sat',
  templateUrl: './monitoramento-sat.component.html',
  styleUrls: ['./monitoramento-sat.component.css']
})
export class MonitoramentoSatComponent implements OnInit {
  @ViewChild("chart") chart: ChartComponent;
  public usuarioSessao: UsuarioSessao;
  public chartOptionsAPL: Partial<ChartOptions>;
  public chartOptionsINT: Partial<ChartOptions>;
  public loading: boolean;
  public haveData: boolean;
  public listaMonitoramento: Monitoramento;

  private chartMax: number = 100;
  private meta: number = 70;
  private redColor: string = "#cc0000";
  private greenColor: string = "#009900";

  constructor(
    private _userService: UserService,
    private _monitoramentoService: MonitoramentoService
  ) {
    this.usuarioSessao = JSON.parse(this._userService.userSession);
  }
  ngOnInit(): void {
    this.carregarDados();
  }

  public async carregarDados() {
    this.loading = true;
    this.listaMonitoramento = (await this._monitoramentoService.obterListaMonitoramento().toPromise());
    if (this.listaMonitoramento != null) {
      this.haveData = true;
      let aplMeta: number[] = [];
      let intMeta: number[] = [];
      this.listaMonitoramento.storageAPL1.forEach(e => { aplMeta.push(this.meta); })
      this.listaMonitoramento.storageINT1.forEach(e => { intMeta.push(this.meta); })
      this.inicializarGrafico(this.greenColor, this.redColor, this.meta, this.listaMonitoramento.storageAPL1, this.listaMonitoramento.storageINT1, aplMeta, intMeta);
    }
  }

  private async inicializarGrafico(greenColor: string, redColor: string, meta: number, apl1: MonitoramentoStorage[], int1: MonitoramentoStorage[], aplMeta: number[], intMeta: number[]) {
    this.chartOptionsAPL = {
      series: [
        {
          name: "Percentual",
          type: "bar",
          data: apl1.map(m => m.valor)
        },
        {
          name: "Meta de estabilidade",
          type: "line",
          data: aplMeta,
          color: redColor
        }
      ],
      dataLabels:
      {
        enabled: true
      },
      legend: {
        show: false
      },
      colors: [
        function ({ value }) {
          return (value < meta) ? redColor : greenColor
        }
      ],
      chart: {
        height: 250,
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
        text: 'SAT-APL1',
        align: 'center'
      },
      xaxis:
      {
        categories: Enumerable.from(apl1.map(m => m.unidade)).orderBy(ord => ord)
      },
      yaxis:
      {
        max: this.chartMax,
        min: 0,
        tickAmount: 5,
        labels:
        {
          formatter: (value) => {
            return (value + "%").replace('.', ',');
          }
        }
      }
    };

    this.chartOptionsINT = {
      series: [
        {
          name: "Percentual",
          type: "bar",
          data: int1.map(m => m.valor)
        },
        {
          name: "Meta de estabilidade",
          type: "line",
          data: intMeta,
          color: redColor
        }
      ],
      dataLabels:
      {
        enabled: true
      },
      legend: {
        show: false
      },
      colors: [
        function ({ value }) {
          return (value < meta) ? redColor : greenColor
        }
      ],
      chart: {
        height: 250,
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
        text: 'SAT-INT1',
        align: 'center'
      },
      xaxis:
      {
        categories: Enumerable.from(int1.map(m => m.unidade)).orderBy(ord => ord)
      },
      yaxis:
      {
        max: this.chartMax,
        min: 0,
        tickAmount: 5,
        labels:
        {
          formatter: (value) => {
            return (value + "%").replace('.', ',');
          }
        }
      }
    };

    this.loading = false;
  }
}
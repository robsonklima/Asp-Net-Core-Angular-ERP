import { Component, OnInit, ViewChild } from '@angular/core';
import { IndicadorService } from 'app/core/services/indicador.service';
import { IndicadorAgrupadorEnum, IndicadorParameters, IndicadorTipoEnum } from 'app/core/types/indicador.types';
import { UsuarioSessao } from 'app/core/types/usuario.types';
import { UserService } from 'app/core/user/user.service';
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
  selector: 'app-monitoramento-sat',
  templateUrl: './monitoramento-sat.component.html',
  styleUrls: ['./monitoramento-sat.component.scss']
})
export class MonitoramentoSatComponent implements OnInit {

  @ViewChild("chart") chart: ChartComponent;
  public usuarioSessao: UsuarioSessao;
  public chartOptionsAPL: Partial<ChartOptions>;
  public chartOptionsINT: Partial<ChartOptions>;
  public loading: boolean;
  public haveData: boolean;
  public monitoramentoModel: MonitoramentoModel[] = [];

  private chartMax: number = 100;
  private meta: number = 70;
  private redColor: string = "#cc0000";
  private greenColor: string = "#009900";

  constructor(
    private _indicadorService: IndicadorService,
    private _userService: UserService
  ) {
    this.usuarioSessao = JSON.parse(this._userService.userSession);
  }
  ngOnInit(): void {
    this.carregarGrafico();
  }

  public async carregarGrafico() {

    this.loading = true;

    const params: IndicadorParameters =
    {
      agrupador: IndicadorAgrupadorEnum.CLIENTE,
      tipo: IndicadorTipoEnum.SLA,
      codTiposIntervencao: "1,2,3,4,6,7",
      codAutorizadas: "8, 10, 13, 40, 48, 102, 108, 119, 130, 132, 141, 169, 172, 177, 178, 182, 183, 189, 190, 191, 192, 202",
      codTiposGrupo: "1,3,5,7,8,9,10,11",
      //dataInicio: moment().startOf('month').toISOString(),
      //dataFim: moment().endOf('month').toISOString()
      dataInicio: moment().startOf('month').toISOString(),
      dataFim: moment().endOf('month').toISOString(),
    }

    // let m: MonitoramentoModel = new MonitoramentoModel();
    // m.DataProcessamento = moment().date();

    // this.MonitoramentoModel.push(new MonitoramentoModel(){

    // });

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
    this.chartOptionsAPL = {
      series: [
        {
          name: "Percentual",
          type: "bar",
          data: [30, 10, 40, 90]
        },
        {
          name: "Meta de estabilidade",
          type: "line",
          data: [this.meta, this.meta, this.meta, this.meta],
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
        text: 'Serviços SAT-APL1',
        align: 'center'
      },
      xaxis:
      {
        categories: ['C:', 'D:', 'E:', 'F:']
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
          data: [60, 5]
        },
        {
          name: "Meta de estabilidade",
          type: "line",
          data: [this.meta, this.meta],
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
        text: 'Integrações SAT-INT1',
        align: 'center'
      },
      xaxis:
      {
        categories: ['C:', 'D:']
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
  }
}

export class MonitoramentoModel {
  public Status: boolean;
  public Tipo: string;
  public Servidor: string;
  public Item: string;
  public Mensagem: string;
  public DataProcessamento: Date;
  public Ociosidade: string;
}

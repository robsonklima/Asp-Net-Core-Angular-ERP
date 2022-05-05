import { Component, OnInit } from '@angular/core';
import { MonitoramentoHistoricoService } from 'app/core/services/monitoramento-historico.service';
import { monitoramentoTipoConst } from 'app/core/types/monitoramento.types';
import moment from 'moment';
import { ApexOptions } from 'ng-apexcharts';

@Component({
  selector: 'app-disponibilidade',
  templateUrl: './disponibilidade.component.html'
})
export class DisponibilidadeComponent implements OnInit {
  historico: any = { labels: [], cpu: [], memory: [] }
  opcoesDatas: any[] = [];
  chartLine: ApexOptions;
  loading: boolean;
  dataAtual = moment().format('yyyy-MM-DD HH:mm:ss');

  constructor(
    private _monitoramentoHistoricoService: MonitoramentoHistoricoService
  ) { }

  async ngOnInit() {
    this.obterDados(this.dataAtual).then(() => this.obterOpcoesDatas());
  }

  private obterDados(data: string = ''): Promise<any> {
    return new Promise((resolve, reject) => {
      this.loading = true;
      if (data) this.dataAtual = data;

      this._monitoramentoHistoricoService.obterPorParametros({
        servidor: 'SATAPLPROD',
        dataHoraProcessamento: this.dataAtual,
        sortActive: "dataHoraProcessamento",
        sortDirection: "asc",
      }).subscribe((data) => {
        console.log(data);
        

        this.historico.labels = data.items.filter(d => d.tipo == 'CPU').map((hist) => moment(hist.dataHoraProcessamento).format('HH:mm'));
        this.historico.cpu = data.items.filter(d => d.tipo == 'CPU').map((cpu) => cpu.emUso);
        this.historico.memory = data.items.filter(d => d.tipo == 'MEMORY').map((memoria) => Number((memoria.emUso / memoria.total * 100).toFixed(0)));

        console.log(this.historico.labels);
        console.log(this.historico.cpu);
        console.log(this.historico.memory);
        

        this.prepararDadosGraficos();
        this.loading = false;
        resolve(data);
      }, () => {
        this.loading = false;
        reject();
      });
    })
  }

  private prepararDadosGraficos() {
    this.chartLine = {
      series: [
        {
          name: "Processador",
          data: this.historico.cpu
        },
        {
          name: "Memória",
          data: this.historico.memory
        }
      ],
      chart: {
        height: 350,
        type: "line",
        dropShadow: {
          enabled: true,
          color: "#000",
          top: 18,
          left: 7,
          blur: 10,
          opacity: 0.2
        },
        zoom: {
          enabled: false,
        },
        toolbar: {
          show: false
        }
      },
      colors: ["#77B6EA", "#00796B"],
      stroke: {
        curve: "smooth"
      },
      grid: {
        borderColor: "#e7e7e7",
        row: {
          colors: ["#f3f3f3", "transparent"],
          opacity: 0.5
        }
      },
      markers: {
        size: 0
      },
      xaxis: {
        categories: this.historico.labels,
        labels: {
          show: false
        }
      },
      yaxis: {
        min: 0,
        max: 100,
        labels: {
          formatter: (value) => {
            return value + "%";
          }
        }
      },
      legend: {
        position: "top",
        horizontalAlign: "right",
        floating: true,
        offsetY: -25,
        offsetX: -5
      }
    };
  }

  pesquisarDadosPorData(data: string) {
    this.obterDados(data);
  }

  obterOpcoesDatas() {
    for (let i = 4; i >= 0; i--)
    {
      this.opcoesDatas.push({
        data: moment().add(-i, 'days').format('yyyy-MM-DD HH:mm:ss'),
        prompt: moment().add(-i, 'days').locale('pt').format('dddd').replace('-feira', '')
      });
    }
  }
}

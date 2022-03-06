import { Component, OnInit } from '@angular/core';
import { MonitoramentoHistoricoService } from 'app/core/services/monitoramento-historico.service';
import { monitoramentoTipoConst } from 'app/core/types/monitoramento.types';
import moment from 'moment';
import { ApexOptions } from 'ng-apexcharts';

@Component({
  selector: 'app-disponibilidade',
  templateUrl: './disponibilidade.component.html'
})
export class DisponibilidadeComponent implements OnInit
{
  historico: any = { labels: [], cpu: [], memory: [] }
  opcoesDatas: any[] = [];
  chartLine: ApexOptions;
  dataAtual = moment().format('yyyy-MM-DD HH:mm:ss');

  constructor(
    private _monitoramentoHistoricoService: MonitoramentoHistoricoService
  ) { }

  async ngOnInit()
  {
    await this.obterMonitoramentoHistorico('');
    this.prepararDadosGraficos();
  }

  private obterMonitoramentoHistorico(tipo: string, data: string = ''): Promise<any>
  {
    return new Promise((resolve, reject) =>
    {
      if (data) this.dataAtual = data;

      this._monitoramentoHistoricoService.obterPorParametros({
        servidor: 'SATAPLPROD',
        tipo: tipo,
        dataHoraProcessamento: data || this.dataAtual,
        sortActive: "dataHoraProcessamento",
        sortDirection: "asc",
      }).subscribe((data) =>
      {
        this.historico.labels =
          data.items.map((hist) => moment(hist.dataHoraProcessamento).format('HH:mm'));

        if (tipo == monitoramentoTipoConst.CPU)
          this.historico.cpu =
            data.items.map((cpu) => cpu.emUso);

        if (tipo == monitoramentoTipoConst.MEMORY)
          this.historico.memory =
            data.items.map((memoria) => Number((memoria.emUso / memoria.total * 100).toFixed(0)));

        resolve(data);
      }, () =>
      {
        reject();
      });
    })
  }

  private prepararDadosGraficos()
  {
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
          formatter: (value) =>
          {
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
}

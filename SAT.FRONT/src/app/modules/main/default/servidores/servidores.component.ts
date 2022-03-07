import { Component, OnInit } from '@angular/core';
import { MonitoramentoService } from 'app/core/services/monitoramento.service';
import { Monitoramento } from 'app/core/types/monitoramento.types';
import { ApexOptions } from 'ng-apexcharts';

@Component({
  selector: 'app-servidores',
  templateUrl: './servidores.component.html'
})
export class ServidoresComponent implements OnInit {
  listaMonitoramento: Monitoramento[] = [];
  chartPie: ApexOptions;

  constructor(
    private _monitoramentoService: MonitoramentoService,
  ) { }

  async ngOnInit() {
    await this.obterMonitoramentos();

    this.prepararGraficos();
  }

  private obterMonitoramentos(): Promise<any>
  {
      return new Promise((resolve, reject) =>
      {
          this._monitoramentoService.obterPorParametros({
              sortActive: "dataHoraProcessamento",
              sortDirection: "asc",
          }).subscribe((data) =>
          {
              this.listaMonitoramento = data.items;

              for (let i = 0; i < data.items.length; i++)
              {
                  this.listaMonitoramento[i].status = this._monitoramentoService.obterStatus(this.listaMonitoramento[i]);
                  this.listaMonitoramento[i].descricao = this._monitoramentoService.obterDescricao(this.listaMonitoramento[i]);
              }
              resolve(data);
          }, () =>
          {
              reject();
          });
      })
  }

  filtrarMonitoramento(tipo: string)
  {
      return this.listaMonitoramento.filter(m => m.tipo == tipo)
  }

  private prepararGraficos()
    {
        this.chartPie = {
            chart: {
                animations: {
                    speed: 400,
                    animateGradually: {
                        enabled: true
                    }
                },
                fontFamily: 'inherit',
                foreColor: 'inherit',
                height: '100%',
                type: 'donut',
                sparkline: {
                    enabled: true
                }
            },
            colors: [],
            labels: [],
            plotOptions: {
                pie: {
                    customScale: 0.9,
                    expandOnClick: false,
                    donut: {
                        size: '70%'
                    }
                }
            },
            series: [],
            states: {
                hover: {
                    filter: {
                        type: 'none'
                    }
                },
                active: {
                    filter: {
                        type: 'none'
                    }
                }
            },
            tooltip: {
                enabled: true,
                fillSeriesColor: false,
                theme: 'light',
                custom: ({
                    seriesIndex,
                    w
                }): string => `<div class="flex items-center h-8 min-h-8 max-h-8 px-3">
                                <div class="w-3 h-3 rounded-full" style="background-color: ${w.config.colors[seriesIndex]};"></div>
                                <div class="ml-2 text-md leading-none">${w.config.labels[seriesIndex]}:</div>
                                <div class="ml-2 text-md font-bold leading-none">${w.config.series[seriesIndex].toFixed(2)}%</div>
                               </div>`
            }
        };
    }
}

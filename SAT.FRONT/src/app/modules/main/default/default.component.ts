import { Component, OnDestroy, OnInit, ViewEncapsulation } from '@angular/core';
import { UsuarioSessao } from 'app/core/types/usuario.types';
import { UserService } from 'app/core/user/user.service';
import { MonitoramentoService } from 'app/core/services/monitoramento.service';
import { Monitoramento } from 'app/core/types/monitoramento.types';
import moment from 'moment';
import { interval, Subject } from 'rxjs';
import { startWith, takeUntil } from 'rxjs/operators';
import { ApexOptions } from 'ng-apexcharts';
import { fuseAnimations } from '@fuse/animations';
import { MonitoramentoHistorico } from 'app/core/types/monitoramento-historico.types';
import { MonitoramentoHistoricoService } from 'app/core/services/monitoramento-historico.service';
import _ from 'lodash';

@Component({
    selector: 'default',
    templateUrl: './default.component.html',
    encapsulation: ViewEncapsulation.None,
    animations   : fuseAnimations,
})

export class DefaultComponent implements OnInit, OnDestroy
{
    sessionData: UsuarioSessao;
    ultimoProcessamento: string;
    loading: boolean;
    listaMonitoramento: Monitoramento[] = [];
    historicoLabels: string[] = [];
    historicoCPUData: number[] = [];
    historicoMemoryData: number[] = [];
    chartPie: ApexOptions;
    chartLine: ApexOptions;
    protected _onDestroy = new Subject<void>();

    constructor (
        private _userService: UserService,
        private _monitoramentoService: MonitoramentoService,
        private _monitoramentoHistoricoService: MonitoramentoHistoricoService
    )
    {
        this.sessionData = JSON.parse(this._userService.userSession);
    }

    ngOnInit(): void
    {
        interval(2 * 60 * 1000)
            .pipe(
                startWith(0),
                takeUntil(this._onDestroy)
            )
            .subscribe(() =>
            {
                this.obterMonitoramentos();
            });
    }

    async obterMonitoramentos(data: string='')
    {
        this.loading = true;
        const monitoramentoData = await this._monitoramentoService.obterPorParametros({
            sortActive: "dataHoraProcessamento",
            sortDirection: "asc",
        }).toPromise();
        this.listaMonitoramento = monitoramentoData.items;

        const historicoCPUData = await this._monitoramentoHistoricoService.obterPorParametros({
            servidor: 'SATAPLPROD',
            tipo: 'CPU',
            dataHoraProcessamentoInicio: data ||moment().format('yyyy-MM-DD HH:mm:ss'),
            dataHoraProcessamentoFim: data || moment().format('yyyy-MM-DD HH:mm:ss'),
            sortActive: "dataHoraProcessamento",
            sortDirection: "asc",
        }).toPromise();

        const historicoMemoryData = await this._monitoramentoHistoricoService.obterPorParametros({
            servidor: 'SATAPLPROD',
            tipo: 'MEMORY',
            dataHoraProcessamentoInicio: data || moment().format('yyyy-MM-DD HH:mm:ss'),
            dataHoraProcessamentoFim: data || moment().format('yyyy-MM-DD HH:mm:ss'),
            sortActive: "dataHoraProcessamento",
            sortDirection: "asc",
        }).toPromise();

        this.historicoLabels = _.union(this.historicoLabels, historicoCPUData.items.map((historico) => {
            return moment(historico.dataHoraProcessamento).format('HH:mm');
        }));

        this.historicoCPUData = historicoCPUData.items.map((cpu) => {
            return cpu.emUso;
        });

        this.historicoLabels = _.union(this.historicoLabels, historicoCPUData.items.map((historico) => {
            return moment(historico.dataHoraProcessamento).format('HH:mm');
        }));

        this.historicoMemoryData = historicoMemoryData.items.map((memoria) => {
            return Number((memoria.emUso / memoria.total * 100).toFixed(0));
        });

        this.prepararDadosGraficos();
        this.ultimoProcessamento = moment().format('HH:mm:ss');
        this.loading = false;
    }

    filtrarMonitoramento(tipo: string) {
        return this.listaMonitoramento.filter(m => m.tipo == tipo)
    }

    private prepararDadosGraficos()
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

        this.chartLine = {
            series: [
              {
                name: "Processador",
                data: this.historicoCPUData
              },
              {
                name: "Memória",
                data: this.historicoMemoryData
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
              toolbar: {
                show: true
              }
            },
            colors: ["#77B6EA", "#545454"],
            dataLabels: {
              enabled: false
            },
            stroke: {
              curve: "smooth"
            },
            title: {
              text: "",
              align: "left"
            },
            grid: {
              borderColor: "#e7e7e7",
              row: {
                colors: ["#f3f3f3", "transparent"], // takes an array which will be repeated on columns
                opacity: 0.5
              }
            },
            markers: {
              size: 1
            },
            xaxis: {
              categories:this.historicoLabels,
              title: {
                text: "Horário"
              }
            },
            yaxis: {
              title: {
                text: "Percentual de Uso"
              },
              min: 0,
              max: 100
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

    obterOciosidadePorExtenso(dataHora: string): string
    {
        return moment(dataHora).locale('pt').fromNow();
    }

    obterOciosidadeEmHoras(dataHora: string): number
    {
        return moment().diff(moment(dataHora), 'hours');
    }

    pesquisarPorData(n: number) {
        const data = moment().add(n*-1, 'days').format('yyyy-MM-DD HH:mm:ss');
        this.obterMonitoramentos(data);
    }

    ngOnDestroy()
    {
        this._onDestroy.next();
        this._onDestroy.complete();
    }
}
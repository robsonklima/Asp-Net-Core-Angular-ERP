import { Component, OnDestroy, OnInit, ViewEncapsulation } from '@angular/core';
import { interval, Subject } from 'rxjs';
import { startWith, takeUntil } from 'rxjs/operators';
import { ApexOptions } from 'ng-apexcharts';
import { fuseAnimations } from '@fuse/animations';
import { UsuarioSessao } from 'app/core/types/usuario.types';
import { UserService } from 'app/core/user/user.service';
import { MonitoramentoService } from 'app/core/services/monitoramento.service';
import { Monitoramento, monitoramentoConst } from 'app/core/types/monitoramento.types';
import { MonitoramentoHistoricoService } from 'app/core/services/monitoramento-historico.service';
import moment from 'moment';
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
    historico: any = { labels: [], cpu: [], memory: [] }
    opcoesDatas: any[] = []; 
    chartPie: ApexOptions;
    chartLine: ApexOptions;
    dataAtual = moment().format('HH:mm:ss');
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
        this.obterOpcoesDatas();

        interval(2 * 60 * 1000)
            .pipe(
                startWith(0),
                takeUntil(this._onDestroy)
            )
            .subscribe(() =>
            {
                this.obterDados();
            });
    }

    async obterDados(data: string='')
    {
        this.loading = true;

        await this.obterMonitoramentos();
        await this.obterMonitoramentoHistorico('CPU', data);
        await this.obterMonitoramentoHistorico('MEMORY', data);
        this.prepararDadosGraficos();
        this.ultimoProcessamento = this.dataAtual;

        this.loading = false;
    }

    private obterMonitoramentos(): Promise<any> {
        return new Promise((resolve, reject) => {
            this._monitoramentoService.obterPorParametros({
                sortActive: "dataHoraProcessamento",
                sortDirection: "asc",
            }).subscribe((data) => {
                this.listaMonitoramento = data.items;
                resolve(data);
            }, () => {
                reject();
            });
        })
    }

    private obterMonitoramentoHistorico(tipo: string, data: string=''): Promise<any> {
        return new Promise((resolve, reject) => {
            this._monitoramentoHistoricoService.obterPorParametros({
                servidor: 'SATAPLPROD',
                tipo: tipo,
                dataHoraProcessamentoInicio: data || this.dataAtual,
                dataHoraProcessamentoFim: data || this.dataAtual,
                sortActive: "dataHoraProcessamento",
                sortDirection: "asc",
            }).subscribe((data) => {
                this.historico.labels = _.union(this.historico.labels, data.items.map((hist) => {
                    return moment(hist.dataHoraProcessamento).format('HH:mm');
                }));
        
                if (tipo == monitoramentoConst.CPU)
                    this.historico.cpu = data.items.map((cpu) => {
                        return cpu.emUso;
                    });

                if (tipo == monitoramentoConst.MEMORY)
                    this.historico.memory = data.items.map((memoria) => {
                        return Number((memoria.emUso / memoria.total * 100).toFixed(0));
                    });

                resolve(data);
            }, () => {
                reject();
            });
        })
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
              toolbar: {
                show: true
              }
            },
            colors: ["#77B6EA", "#00796B"],
            dataLabels: {
              enabled: false
            },
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
              size: 1
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

    private obterOpcoesDatas() {
        for (let i = 4; i >= 0; i--) {
            this.opcoesDatas.push({
                data: moment().add(-i, 'days').format('yyyy-MM-DD HH:mm:ss'),
                prompt: moment().add(-i, 'days').locale('pt').format('dddd').replace('-feira', '')
            });
        }

        console.log(this.opcoesDatas);
        
    }

    pesquisarHistoricoPorData(data: string) {
        console.log(data)

        this.obterDados(data);
    }

    obterOciosidadePorExtenso(dataHora: string): string
    {
        return moment(dataHora).locale('pt').fromNow();
    }

    obterOciosidadeEmHoras(dataHora: string): number
    {
        return moment().diff(moment(dataHora), 'hours');
    }

    ngOnDestroy()
    {
        this._onDestroy.next();
        this._onDestroy.complete();
    }
}
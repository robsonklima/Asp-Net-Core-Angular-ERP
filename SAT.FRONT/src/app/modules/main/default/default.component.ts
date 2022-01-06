import { Component, OnDestroy, OnInit, ViewEncapsulation } from '@angular/core';
import { UsuarioSessao } from 'app/core/types/usuario.types';
import { UserService } from 'app/core/user/user.service';
import { MonitoramentoService } from 'app/core/services/monitoramento.service';
import { Monitoramento, MonitoramentoTipoEnum } from 'app/core/types/monitoramento.types';
import moment from 'moment';
import { interval, Subject } from 'rxjs';
import { startWith, takeUntil } from 'rxjs/operators';
import { ApexOptions } from 'ng-apexcharts';

@Component({
    selector: 'default',
    templateUrl: './default.component.html',
    encapsulation: ViewEncapsulation.None
})

export class DefaultComponent implements OnInit, OnDestroy
{
    sessionData: UsuarioSessao;
    ultimoProcessamento: string;
    public loading: boolean;
    public listaMonitoramento: Monitoramento[] = [];
    chartData: any = {
        uniqueVisitors: 46085,
        series        : [25, 75],
        labels        : [
            'English',
            'Other'
        ]
    }
    chart: ApexOptions;
    protected _onDestroy = new Subject<void>();

    constructor (
        private _userService: UserService,
        private _monitoramentoService: MonitoramentoService
    )
    {
        this.sessionData = JSON.parse(this._userService.userSession);
    }

    ngOnInit(): void
    {
        interval(1 * 60 * 1000)
            .pipe(
                startWith(0),
                takeUntil(this._onDestroy)
            )
            .subscribe(() =>
            {
                this.obterMonitoramentos();
            });
    }

    obterMonitoramentos()
    {
        this.loading = true;
        this._monitoramentoService.obterPorParametros
            ({
                tipo: MonitoramentoTipoEnum.CHAMADO,
                sortActive: "dataHoraProcessamento",
                sortDirection: "desc"
            }).subscribe(data =>
            {
                this.listaMonitoramento = data.items;
                this.ultimoProcessamento = moment().format('HH:mm:ss');
                this.loading = false;
            }, () =>
            {
                this.loading = false;
            });
    }

    private prepararDadosGraficos() {
        this.chart = {
            chart      : {
                animations: {
                    speed           : 400,
                    animateGradually: {
                        enabled: false
                    }
                },
                fontFamily: 'inherit',
                foreColor : 'inherit',
                height    : '100%',
                type      : 'donut',
                sparkline : {
                    enabled: true
                }
            },
            colors     : ['#3182CE', '#63B3ED'],
            labels     : [
                'A',
                'B'
            ],
            plotOptions: {
                pie: {
                    customScale  : 0.9,
                    expandOnClick: false,
                    donut        : {
                        size: '70%'
                    }
                }
            },
            series     : [80, 20],
            states     : {
                hover : {
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
            tooltip    : {
                enabled        : true,
                fillSeriesColor: false,
                theme          : 'dark',
                custom         : ({
                                      seriesIndex,
                                      w
                                  }): string => `<div class="flex items-center h-8 min-h-8 max-h-8 px-3">
                                                    <div class="w-3 h-3 rounded-full" style="background-color: ${w.config.colors[seriesIndex]};"></div>
                                                    <div class="ml-2 text-md leading-none">${w.config.labels[seriesIndex]}:</div>
                                                    <div class="ml-2 text-md font-bold leading-none">${w.config.series[seriesIndex]}%</div>
                                                </div>`
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

    ngOnDestroy()
    {
        this._onDestroy.next();
        this._onDestroy.complete();
    }
}
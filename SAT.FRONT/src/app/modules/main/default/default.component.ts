import { Component, OnDestroy, OnInit, ViewEncapsulation } from '@angular/core';
import { UsuarioSessao } from 'app/core/types/usuario.types';
import { UserService } from 'app/core/user/user.service';
import { MonitoramentoService } from 'app/core/services/monitoramento.service';
import { MonitoramentoCliente } from 'app/core/types/monitoramento.types';
import moment from 'moment';
import { interval, Subject } from 'rxjs';
import { startWith, takeUntil } from 'rxjs/operators';

@Component({
    selector: 'default',
    templateUrl: './default.component.html',
    encapsulation: ViewEncapsulation.None
})

export class DefaultComponent implements OnInit, OnDestroy
{
    sessionData: UsuarioSessao;
    horaAtualizacaoMonitoramentoClientes: string;
    public loading: boolean;
    public listaMonitoramentoClientes: MonitoramentoCliente[] = [];
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
                this.obterMonitoramentoClientes();
            });
    }

    async obterMonitoramentoClientes()
    {
        this.loading = true;
        this._monitoramentoService.obterPorParametros().subscribe(data =>
        {
            this.listaMonitoramentoClientes = data;
            this.horaAtualizacaoMonitoramentoClientes = moment().format('HH:mm:ss');
            this.loading = false;
        }, () =>
        {
            this.loading = false;
        });
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
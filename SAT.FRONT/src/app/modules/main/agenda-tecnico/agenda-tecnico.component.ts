import { Component, OnInit } from '@angular/core';
import { setOptions, MbscEventcalendarView, MbscCalendarEvent, localePtBR, Notifications } from '@mobiscroll/angular';
import { OrdemServicoService } from 'app/core/services/ordem-servico.service';
import { TecnicoService } from 'app/core/services/tecnico.service';
import moment from 'moment';

setOptions({
    locale: localePtBR,
    theme: 'windows',
    themeVariant: 'light',
    clickToCreate: true,
    dragToCreate: true,
    dragToMove: true,
    dragToResize: true
});

@Component({
    selector: 'app-agenda-tecnico',
    templateUrl: './agenda-tecnico.component.html',
    styleUrls: ['./agenda-tecnico.component.scss'],
})
export class AgendaTecnicoComponent implements OnInit {
    constructor(
        private _notify: Notifications,
        private _tecnicoSvc: TecnicoService,
        private _osSvc: OrdemServicoService
    ) { }

    view: MbscEventcalendarView = {
        timeline: {
            type: 'day',
            allDay: false,
            startDay: 1,
            startTime: '08:00',
            endTime: '19:00'
        }
    };

    events: MbscCalendarEvent[] = [];
    resources = [];

    ngOnInit(): void {
        this.obterTecnicos();
        this.obterChamados();
    }

    private async obterChamados() {
        const data = this._osSvc.obterPorParametros({
            codFiliais: "4",
            codStatusServicos: "8"
        }).toPromise();

        this.events = (await data).items.map(os => {
                return {
                    start: moment(os.dataHoraTransf),
                    end: moment(os.dataHoraTransf).add('hour', 1),
                    title: os.codOS.toString(),
                    color: '#388E3C',
                    editable: true,
                    resource: os.tecnico.codTecnico,
                }
            }
        )
    }

    private async obterTecnicos() {
        const data = this._tecnicoSvc.obterPorParametros({
            indAtivo: 1,
            codFilial: 4,
            codPerfil: 35,
            sortActive: 'nome',
            sortDirection: 'asc'
        }).toPromise();

        this.resources = (await data).items.map(tecnico => {
            return {
                id: tecnico.codTecnico,
                name: tecnico.nome,
            }
        });

        console.log((await data).items);
    }

    onCellDoubleClick(event: any): void {
        this._notify.alert({
            title: 'Click',
            message: event.date + ' resource ' + event.resource
        });
    }
}
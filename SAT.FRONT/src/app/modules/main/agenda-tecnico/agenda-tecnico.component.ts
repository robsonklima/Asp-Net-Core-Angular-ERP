import { Component, OnInit } from '@angular/core';
import { setOptions, MbscEventcalendarView, MbscCalendarEvent, localePtBR, Notifications } from '@mobiscroll/angular';
import { TecnicoService } from 'app/core/services/tecnico.service';

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
        private _tecnicoSvc: TecnicoService
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

    events: MbscCalendarEvent[] = [
        {
            start: "2021-10-01T08:00:00.000Z",
            end: "2021-10-01T09:00:00.000Z", 
            title: "Business of Software Conference", 
            color: '#388E3C',
            editable: false,
            resource: 1,
        },
        {
            start: "2021-10-01T11:00:00.000Z",
            end: "2021-10-01T15:00:00.000Z",
            title: "Status Update mtg.",
            color: '#388E3C',
            editable: true,
            resource: 1,
        },
        {
            "start": "2021-09-28T11:00:00.000Z",
            end: "2021-09-28T14:30:00.000Z",
            title: "Product team mtg.",
            resource: 1
        },
        {
            "start": "2021-09-28T14:00:00.000Z",
            end: "2021-09-28T17:00:00.000Z",
            title: "Wrapup mtg.",
            resource: 2
        },
        {
            "start": "2021-09-28T12:00:00.000Z",
            end: "2021-09-28T15:00:00.000Z",
            title: "Board mtg.",
            resource: 6
        },
        {
            "start": "2021-09-30T12:00:00.000Z",
            end: "2021-09-30T17:00:00.000Z",
            title: "Decision Making mtg.",
            resource: 1
        },
        {
            "start": "2021-09-29T11:00:00.000Z",
            end: "2021-09-29T15:30:00.000Z",
            title: "Shaping the Future",
            resource: 2
        },
        {
            "start": "2021-09-29T14:00:00.000Z",
            end: "2021-09-29T17:00:00.000Z",
            title: "Client Training",
            resource: 4
        },
        {
            "start": "2021-09-29T09:00:00.000Z",
            end: "2021-09-29T12:30:00.000Z",
            title: "Innovation mtg.",
            resource: 5
        },
        {
            "start": "2021-09-30T08:00:00.000Z",
            end: "2021-09-30T13:00:00.000Z",
            title: "Innovation mtg.",
            resource: 6
        },
        {
            "start": "2021-09-30T11:00:00.000Z",
            end: "2021-09-30T16:00:00.000Z",
            title: "Decision Making mtg.",
            resource: 4
        },
        {
            "start": "2021-09-30T10:00:00.000Z",
            end: "2021-09-30T13:00:00.000Z",
            title: "Stakeholder mtg.",
            resource: 5
        },
        {
            "start": "2021-10-01T09:00:00.000Z",
            end: "2021-10-01T14:00:00.000Z",
            title: "Impact Training",
            resource: 2
        },
        {
            "start": "2021-10-01T11:00:00.000Z",
            end: "2021-10-01T17:00:00.000Z",
            title: "Portraits of Success",
            resource: 3
        }
    ];

    resources = [];

    ngOnInit(): void {
        this.obterTecnicos();
    }

    private async obterTecnicos() {
        const data = this._tecnicoSvc.obterPorParametros({indAtivo:1, pageSize: 10}).toPromise();
        this.resources = (await data).items.map(tecnico => {
            return {
                id: tecnico.codTecnico,
                name: tecnico.nome,
            }
        });
    }

    onCellDoubleClick(event: any): void { 
        this._notify.alert({
            title: 'Click',
            message: event.date
        });
    }
}
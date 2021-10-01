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
            message: event.date + ' resource ' + event.resource
        });

        console.log(event);
        
    }
}
import { Component, OnInit } from '@angular/core';
import { setOptions, MbscEventcalendarView, MbscCalendarEvent } from '@mobiscroll/angular';
import { HttpClient } from '@angular/common/http';

setOptions({
    //locale: localePtBR,
    theme: 'windows',
    themeVariant: 'light',
    clickToCreate: true,
    dragToCreate: true,
    dragToMove: true,
    dragToResize: true,
    calendarWidth: 600
    //height: 500
});

@Component({
    selector: 'app-agenda-tecnico',
    templateUrl: './agenda-tecnico.component.html',
    styleUrls: ['./agenda-tecnico.component.scss'],
})
export class AgendaTecnicoComponent implements OnInit {

    constructor(
        private http: HttpClient
    ) {}

    view: MbscEventcalendarView = {
        timeline: {
            type: 'week',
            startDay: 1,
            endDay: 5
        }
    };

    myEvents: MbscCalendarEvent[] = [];

    myResources = [
        {
            id: 1,
            name: 'Nome do Técnico',
            color: '#fdf500'
        }, {
            id: 2,
            name: 'Nome do Técnico',
            color: '#ff0101'
        }, {
            id: 3,
            name: 'Nome do Técnico',
            color: '#01adff'
        }, {
            id: 4,
            name: 'Nome do Técnico',
            color: '#239a21'
        }, {
            id: 5,
            name: 'Nome do Técnico',
            color: '#ff4600'
        }, {
            id: 6,
            name: 'Nome do Técnico',
            color: '#8f1ed6'
        }, {
            id: 7,
            name: 'Nome do Técnico',
            color: '#8f1ed6'
        }, {
            id: 8,
            name: 'Nome do Técnico',
            color: '#8f1ed6'
        }, {
            id: 9,
            name: 'Nome do Técnico',
            color: '#8f1ed6'
        }, {
            id: 10,
            name: 'Nome do Técnico',
            color: '#8f1ed6'
        }, {
            id: 11,
            name: 'Nome do Técnico',
            color: '#8f1ed6'
        }, {
            id: 12,
            name: 'Nome do Técnico',
            color: '#8f1ed6'
        }, {
            id: 13,
            name: 'Nome do Técnico',
            color: '#8f1ed6'
        }, {
            id: 14,
            name: 'Nome do Técnico',
            color: '#8f1ed6'
        }, {
            id: 15,
            name: 'Nome do Técnico',
            color: '#8f1ed6'
        }, {
            id: 16,
            name: 'Nome do Técnico',
            color: '#8f1ed6'
        }, {
            id: 17,
            name: 'Nome do Técnico',
            color: '#8f1ed6'
        }
    ];

    ngOnInit(): void {
        this.http.jsonp < MbscCalendarEvent[] > ('https://trial.mobiscroll.com/daily-weekly-events/', 'callback').subscribe((resp) => {
            this.myEvents = resp;
        });
    }
}
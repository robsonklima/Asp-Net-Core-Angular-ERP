import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, Resolve, RouterStateSnapshot } from '@angular/router';
import { Observable, of } from 'rxjs';
import { AgendaTecnicoService } from 'app/core/services/agenda-tecnico.service';
import { AgendaTecnicoParameters, Calendar, CalendarSettings, CalendarWeekday } from 'app/core/types/agenda-tecnico.types';
import { UserService } from '../user/user.service';
import { UsuarioSessao } from '../types/usuario.types';

@Injectable({
    providedIn: 'root'
})
export class CalendarCalendarsResolver implements Resolve<any>
{
    userSession: UsuarioSessao;

    constructor(
        private _agendaTecnicoService: AgendaTecnicoService,
        private _userService: UserService
    ) {
        this.userSession = JSON.parse(this._userService.userSession)
    }

    resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<Calendar[]>
    {
        var params: AgendaTecnicoParameters = { codFilial: this.userSession.usuario?.codFilial, pageSize: 5000 };

        return this._agendaTecnicoService.obterCalendariosEEventos(params);
    }
}

@Injectable({
    providedIn: 'root'
})
export class CalendarWeekdaysResolver implements Resolve<any>
{
    constructor(private _agendaTecnicoService: AgendaTecnicoService) { }

    resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<CalendarWeekday[]>
    {
        return of([
            {
                abbr : 'S',
                label: 'Segunda',
                value: 'Se'
            },
            {
                abbr : 'T',
                label: 'Terça',
                value: 'Te'
            },
            {
                abbr : 'Q',
                label: 'Quarta',
                value: 'Qu'
            },
            {
                abbr : 'Q',
                label: 'Quinta',
                value: 'Qu'
            },
            {
                abbr : 'S',
                label: 'Sexta',
                value: 'Se'
            },
            {
                abbr : 'S',
                label: 'Sábado',
                value: 'Sa'
            },
            {
                abbr : 'D',
                label: 'Domingo',
                value: 'Do'
            }
        ]);
    }
}

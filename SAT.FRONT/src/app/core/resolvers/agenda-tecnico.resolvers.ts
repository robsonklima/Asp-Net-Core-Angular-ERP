import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, Resolve, RouterStateSnapshot } from '@angular/router';
import { Observable } from 'rxjs';
import { AgendaTecnicoService } from 'app/core/services/agenda-tecnico.service';
import { Calendar, CalendarSettings, CalendarWeekday } from 'app/core/types/agenda-tecnico.types';

@Injectable({
    providedIn: 'root'
})
export class CalendarCalendarsResolver implements Resolve<any>
{
    constructor(private _agendaTecnicoService: AgendaTecnicoService)
    {
    }

    resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<Calendar[]>
    {
        return this._agendaTecnicoService.getCalendars();
    }
}

@Injectable({
    providedIn: 'root'
})
export class CalendarSettingsResolver implements Resolve<any>
{
    constructor(private _agendaTecnicoService: AgendaTecnicoService)
    {
    }

    resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<CalendarSettings>
    {
        return this._agendaTecnicoService.getSettings();
    }
}

@Injectable({
    providedIn: 'root'
})
export class CalendarWeekdaysResolver implements Resolve<any>
{
    constructor(private _agendaTecnicoService: AgendaTecnicoService)
    {
    }

    resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<CalendarWeekday[]>
    {
        return this._agendaTecnicoService.getWeekdays();
    }
}

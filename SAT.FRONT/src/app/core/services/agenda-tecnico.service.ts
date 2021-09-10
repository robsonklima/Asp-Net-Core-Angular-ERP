import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { BehaviorSubject, Observable, of } from 'rxjs';
import { map, switchMap, take, tap } from 'rxjs/operators';
import { Moment } from 'moment';
import { appConfig as c } from 'app/core/config/app.config'
import {
    AgendaTecnicoParameters, Calendar, CalendarEvent, CalendarEventEditMode, CalendarSettings, CalendarWeekday
} from 'app/core/types/agenda-tecnico.types';

@Injectable({
    providedIn: 'root'
})
export class AgendaTecnicoService
{
    private _calendars: BehaviorSubject<Calendar[] | null> = new BehaviorSubject(null);
    private _events: BehaviorSubject<CalendarEvent[] | null> = new BehaviorSubject(null);
    
    constructor(
        private _httpClient: HttpClient
    ) {}
    
    get calendars$(): Observable<Calendar[]>
    {
        return this._calendars.asObservable();
    }
    
    get events$(): Observable<CalendarEvent[]>
    {
        return this._events.asObservable();
    }

    obterCalendariosEEventos(parameters: AgendaTecnicoParameters): Observable<Calendar[]> {
        let params = new HttpParams();
        
        Object.keys(parameters).forEach(key => {
          if (parameters[key] !== undefined && parameters[key] !== null) params = params.append(key, String(parameters[key]));
        });

        return this._httpClient.get<Calendar[]>(`${c.api}/AgendaTecnico`, { params: params }).pipe(
            tap((data) => {
                this._calendars.next(data);
                this._events.next(data.map(d => d.eventos).reduce((a, b) => a.concat(b), []).map(o => o));
            })
        );
    }

    updateCalendar(id: string, calendar: Calendar): Observable<Calendar>
    {
        return this._httpClient.put<Calendar>(`${c.api}/AgendaTecnico`, calendar).pipe(
            tap(() => {
                let calendars = this._calendars.value;

                 // Find the index of the updated calendar
                const index = calendars.findIndex(item => item.id === id);

                // Update the calendar
                calendars[index] = calendar;

                // Update the calendars
                this._calendars.next(calendars);

                // Return the updated calendar
                return calendar;
            })
        );
    }
    
    addEvent(event): Observable<CalendarEvent>
    {
        return this._httpClient.post<CalendarEvent>(`${c.api}/AgendaTecnico/Evento`, event).pipe(
            tap((data) => {
                this._events.next([...this._events.value, ...[data]]);

                let calendars = this._calendars.value;
                const index = calendars.findIndex(c => c.id === data.calendarId);
                calendars[index].eventos.push(data);
                this._calendars.next(calendars);
            })
        );
    }
    
    updateEvent(id: string, event): Observable<CalendarEvent>
    {
        return this._httpClient.put<CalendarEvent>(`${c.api}/AgendaTecnico/Evento`, event).pipe(
            tap((updatedEvent) => {
                let events = this._events.value;

                const index = events.findIndex(item => item.id.toString() === id);

                // Update the event
                events[index] = updatedEvent;

                // Update the events
                this._events.next(events);

                // Return the updated event
                return updatedEvent;
            })
        );
    }
    
    deleteEvent(id: string): Observable<CalendarEvent>
    {
        return this._httpClient.delete<CalendarEvent>(`${c.api}/AgendaTecnico/Evento/${id}`).pipe(
            tap((data) => {
                let events = this._events.value;
                const iEvent = events.findIndex(e => e.id === data.id);
                events.slice(iEvent, 0);
                this._events.next(events);

                let calendars = this._calendars.value;
                const iCalendar = calendars.findIndex(c => c.id === data.calendarId);
                calendars[iCalendar].eventos.slice(iCalendar, 0);
                this._calendars.next(calendars);
            })
        );
    }
}

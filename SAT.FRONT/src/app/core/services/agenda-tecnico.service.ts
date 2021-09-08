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
    private _loadedEventsRange: { start: Moment | null; end: Moment | null } = {
        start: null,
        end  : null
    };
    private readonly _numberOfDaysToPrefetch = 60;
    private _settings: BehaviorSubject<CalendarSettings | null> = new BehaviorSubject(null);
    private _weekdays: BehaviorSubject<CalendarWeekday[] | null> = new BehaviorSubject(null);

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
    
    get settings$(): Observable<CalendarSettings>
    {
        return this._settings.asObservable();
    }
    
    get weekdays$(): Observable<CalendarWeekday[]>
    {
        return this._weekdays.asObservable();
    }

    obterPorParametros(parameters: AgendaTecnicoParameters): Observable<Calendar[]> {
        let params = new HttpParams();
        
        Object.keys(parameters).forEach(key => {
          if (parameters[key] !== undefined && parameters[key] !== null) params = params.append(key, String(parameters[key]));
        });

        return this._httpClient.get<Calendar[]>(`${c.api}/AgendaTecnico`, { params: params }).pipe(
            tap((data) => {
                var calendars = [];
                var events = [];

                data.forEach((ag: any) => {
                    calendars.push({
                        id: ag.id.toString(),
                        title: ag.title,
                        color: ag.color,
                        visible: true
                    });

                    ag.eventos.forEach(ev => {
                        events.push({
                            id: ev.id.toString(),
                            calendarId: ev.calendarId.toString(),
                            duration: ev.duration || 60,
                            title: ev.title,
                            description: ev.description,
                            start: ev.start,
                            end: ev.end
                        });
                    });
                });

                this._calendars.next(calendars);
                this._events.next(events);
            })
        );
    }

    addCalendar(calendar: Calendar): Observable<Calendar>
    {
        return this.calendars$.pipe(
            take(1),
            switchMap(calendars => this._httpClient.post<Calendar>('api/apps/calendar/calendars', {
                calendar
            }).pipe(
                map((addedCalendar) => {

                    // Add the calendar
                    calendars.push(addedCalendar);

                    // Update the calendars
                    this._calendars.next(calendars);

                    // Return the added calendar
                    return addedCalendar;
                })
            ))
        );
    }

    updateCalendar(id: string, calendar: Calendar): Observable<Calendar>
    {
        return this.calendars$.pipe(
            take(1),
            switchMap(calendars => this._httpClient.patch<Calendar>('api/apps/calendar/calendars', {
                id,
                calendar
            }).pipe(
                map((updatedCalendar) => {

                    // Find the index of the updated calendar
                    const index = calendars.findIndex(item => item.id === id);

                    // Update the calendar
                    calendars[index] = updatedCalendar;

                    // Update the calendars
                    this._calendars.next(calendars);

                    // Return the updated calendar
                    return updatedCalendar;
                })
            ))
        );
    }
    
    deleteCalendar(id: string): Observable<any>
    {
        return this.calendars$.pipe(
            take(1),
            switchMap(calendars => this._httpClient.delete<Calendar>('api/apps/calendar/calendars', {
                params: {id}
            }).pipe(
                map((isDeleted) => {

                    // Find the index of the deleted calendar
                    const index = calendars.findIndex(item => item.id === id);

                    // Delete the calendar
                    calendars.splice(index, 1);

                    // Update the calendars
                    this._calendars.next(calendars);

                    // Remove the events belong to deleted calendar
                    const events = this._events.value.filter(event => event.calendarId !== id);

                    // Update the events
                    this._events.next(events);

                    // Return the deleted status
                    return isDeleted;
                })
            ))
        );
    }
    
    reloadEvents(): Observable<CalendarEvent[]>
    {
        // Get the events
        return this._httpClient.get<CalendarEvent[]>('api/apps/calendar/events', {
            params: {
                start: this._loadedEventsRange.start.toISOString(),
                end  : this._loadedEventsRange.end.toISOString()
            }
        }).pipe(
            map((response) => {

                // Execute the observable with the response replacing the events object
                this._events.next(response);

                // Return the response
                return response;
            })
        );
    }
    
    prefetchFutureEvents(end: Moment): Observable<CalendarEvent[]>
    {
        // Calculate the remaining prefetched days
        const remainingDays = this._loadedEventsRange.end.diff(end, 'days');

        // Return if remaining days is bigger than the number
        // of days to prefetch. This means we were already been
        // there and fetched the events data so no need for doing
        // it again.
        if ( remainingDays >= this._numberOfDaysToPrefetch )
        {
            return of([]);
        }

        // Figure out the start and end dates
        const start = this._loadedEventsRange.end.clone().add(1, 'day');
        end = this._loadedEventsRange.end.clone().add(this._numberOfDaysToPrefetch - remainingDays, 'days');

        // Prefetch the events
        //return this.getEvents(start, end);
    }
    
    prefetchPastEvents(start: Moment): Observable<CalendarEvent[]>
    {
        // Calculate the remaining prefetched days
        const remainingDays = start.diff(this._loadedEventsRange.start, 'days');

        // Return if remaining days is bigger than the number
        // of days to prefetch. This means we were already been
        // there and fetched the events data so no need for doing
        // it again.
        if ( remainingDays >= this._numberOfDaysToPrefetch )
        {
            return of([]);
        }

        // Figure out the start and end dates
        start = this._loadedEventsRange.start.clone().subtract(this._numberOfDaysToPrefetch - remainingDays, 'days');
        const end = this._loadedEventsRange.start.clone().subtract(1, 'day');

        // Prefetch the events
        //return this.getEvents(start, end);
    }
    
    addEvent(event): Observable<CalendarEvent>
    {
        event.dataHoraCad = '2019-09-09';
        event.codUsuarioCad = 'rklima';
        event.duration = event.duration || 60;

        return this._httpClient.post<CalendarEvent>(`${c.api}/AgendaTecnico/Evento`, event).pipe(
            tap((data) => {
                console.log(data)

                data.id = data.id.toString();
                var lista = this._events.value;
                lista.push(data);
                this._events.next(lista);
                console.log(this._events.value)
            })
        );
    }
    
    updateEvent(id: string, event): Observable<CalendarEvent>
    {
        return this.events$.pipe(
            take(1),
            switchMap(events => this._httpClient.patch<CalendarEvent>(`${c.api}/AgendaTecnico/Evento`, 
                event
            ).pipe(
                map((updatedEvent) => {

                    // Find the index of the updated event
                    const index = events.findIndex(item => item.id === id);

                    // Update the event
                    events[index] = updatedEvent;

                    // Update the events
                    this._events.next(events);

                    // Return the updated event
                    return updatedEvent;
                })
            ))
        );
    }
    
    updateRecurringEvent(event, originalEvent, mode: CalendarEventEditMode): Observable<boolean>
    {
        return this._httpClient.patch<boolean>('api/apps/calendar/recurring-event', {
            event,
            originalEvent,
            mode
        });
    }
    
    deleteEvent(id: string): Observable<CalendarEvent>
    {
        return this.events$.pipe(
            take(1),
            switchMap(events => this._httpClient.delete<CalendarEvent>('api/apps/calendar/event', {params: {id}}).pipe(
                map((isDeleted) => {

                    // Find the index of the deleted event
                    const index = events.findIndex(item => item.id === id);

                    // Delete the event
                    events.splice(index, 1);

                    // Update the events
                    this._events.next(events);

                    // Return the deleted status
                    return isDeleted;
                })
            ))
        );
    }
    
    deleteRecurringEvent(event, mode: CalendarEventEditMode): Observable<boolean>
    {
        return this._httpClient.delete<boolean>('api/apps/calendar/recurring-event', {
            params: {
                event: JSON.stringify(event),
                mode
            }
        });
    }
    
    getSettings(): Observable<CalendarSettings>
    {
        return this._httpClient.get<CalendarSettings>('api/apps/calendar/settings').pipe(
            tap((response) => {
                this._settings.next(response);
            })
        );
    }
    
    updateSettings(settings: CalendarSettings): Observable<CalendarSettings>
    {
        return this.events$.pipe(
            take(1),
            switchMap(events => this._httpClient.patch<CalendarSettings>('api/apps/calendar/settings', {
                settings
            }).pipe(
                map((updatedSettings) => {

                    // Update the settings
                    this._settings.next(settings);

                    // Get weekdays again to get them in correct order
                    // in case the startWeekOn setting changes
                    this.getWeekdays().subscribe();

                    // Return the updated settings
                    return updatedSettings;
                })
            ))
        );
    }
    
    getWeekdays(): Observable<CalendarWeekday[]>
    {
        return this._httpClient.get<CalendarWeekday[]>('api/apps/calendar/weekdays').pipe(
            tap((response) => {
                this._weekdays.next(response);
            })
        );
    }
}

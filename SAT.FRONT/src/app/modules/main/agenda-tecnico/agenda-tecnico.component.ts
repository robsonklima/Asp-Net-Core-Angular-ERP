import { 
    AfterViewInit, ChangeDetectionStrategy, ChangeDetectorRef, Component, Inject, OnDestroy,
    OnInit, TemplateRef, ViewChild, ViewContainerRef, ViewEncapsulation
} from '@angular/core';
import { DOCUMENT } from '@angular/common';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Overlay, OverlayRef } from '@angular/cdk/overlay';
import { TemplatePortal } from '@angular/cdk/portal';
import { MatDrawer } from '@angular/material/sidenav';
import { FullCalendarComponent } from '@fullcalendar/angular';
import { Calendar as FullCalendar } from '@fullcalendar/core';
import dayGridPlugin from '@fullcalendar/daygrid';
import listPlugin from '@fullcalendar/list';
import interactionPlugin from '@fullcalendar/interaction';
import momentPlugin from '@fullcalendar/moment';
import rrulePlugin from '@fullcalendar/rrule';
import timeGridPlugin from '@fullcalendar/timegrid';
import { clone, cloneDeep, omit } from 'lodash-es';
import * as moment from 'moment';
import { RRule } from 'rrule';
import { interval, Subject } from 'rxjs';
import { debounceTime, delay, filter, map, startWith, takeUntil } from 'rxjs/operators';
import { FuseMediaWatcherService } from '@fuse/services/media-watcher';
import { AgendaTecnicoService } from 'app/core/services/agenda-tecnico.service';
import {
    Calendar, CalendarDrawerMode, CalendarEvent, CalendarEventEditMode, CalendarEventPanelMode, CalendarSettings
} from 'app/core/types/agenda-tecnico.types';
import ptLocale from '@fullcalendar/core/locales/pt';
import { UserService } from 'app/core/user/user.service';
import { UsuarioSessao } from 'app/core/types/usuario.types';
import { OrdemServicoService } from 'app/core/services/ordem-servico.service';
import { OrdemServico, OrdemServicoParameters } from 'app/core/types/ordem-servico.types';

@Component({
    selector       : 'app-agenda-tecnico',
    templateUrl    : './agenda-tecnico.component.html',
    styleUrls      : ['./agenda-tecnico.component.scss'],
    changeDetection: ChangeDetectionStrategy.OnPush,
    encapsulation  : ViewEncapsulation.None
})
export class AgendaTecnicoComponent implements OnInit, AfterViewInit, OnDestroy {
    @ViewChild('eventPanel') private _eventPanel: TemplateRef<any>;
    @ViewChild('fullCalendar') private _fullCalendar: FullCalendarComponent;
    @ViewChild('drawer') private _drawer: MatDrawer;

    locales = [ptLocale];
    calendars: Calendar[];
    ordensServico: OrdemServico[] = [];
    calendarPlugins: any[] = [ 
        dayGridPlugin, interactionPlugin, listPlugin, momentPlugin, rrulePlugin, timeGridPlugin
    ];
    drawerMode: CalendarDrawerMode = 'side';
    drawerOpened: boolean = true;
    event: CalendarEvent;
    eventEditMode: CalendarEventEditMode = 'single';
    eventForm: FormGroup;
    ordemServicoFiltro: FormControl = new FormControl();
    eventTimeFormat: any;
    events: CalendarEvent[] = [];
    panelMode: CalendarEventPanelMode = 'view';
    settings: CalendarSettings = {
        dateFormat : 'DD/MM/YYYY',
        timeFormat : '24',
        startWeekOn: 1,
    };
    view: 'timeGridWeek' | 'timeGridDay' | 'listYear' = 'timeGridDay';
    views: any;
    viewTitle: string;
    userSession: UsuarioSessao;
    private _eventPanelOverlayRef: OverlayRef;
    private _fullCalendarApi: FullCalendar;
    private _unsubscribeAll: Subject<any> = new Subject<any>();

    constructor(
        private _agendaTecnicoService: AgendaTecnicoService,
        private _changeDetectorRef: ChangeDetectorRef,
        @Inject(DOCUMENT) private _document: Document,
        private _formBuilder: FormBuilder,
        private _overlay: Overlay,
        private _fuseMediaWatcherService: FuseMediaWatcherService,
        private _viewContainerRef: ViewContainerRef,
        private _userService: UserService,
        private _ordemServicoService: OrdemServicoService
    ) {
        this.userSession = JSON.parse(this._userService.userSession);
    }
    
    ngOnInit(): void
    {
        interval(5 * 1000)
            .pipe(
                startWith(0),
                takeUntil(this._unsubscribeAll)
            )
            .subscribe(() => {
                //this._agendaTecnicoService.obterCalendariosEEventos({ pa: 1, codFilial: 4 }).subscribe();
            });

        this.obterOrdensServico();

        this.eventForm = this._formBuilder.group({
            id              : [''],
            calendarId      : ['', [Validators.required]],
            recurringEventId: [null],
            codUsuarioCad   : [null],
            codUsuarioManu  : [null],
            dataHoraCad     : [null],
            datahoraManut   : [null],
            title           : [''],
            description     : [''],
            start           : [null],
            end             : [null],
            duration        : [null],
            allDay          : [null],
            recurrence      : [null],
            codOS           : [null, [Validators.required]],
            range           : [{}]
        });

        this.ordemServicoFiltro.valueChanges.pipe(
            filter(query => !!query),
            debounceTime(700),
            delay(500),
            takeUntil(this._unsubscribeAll),
            map(async query => { this.obterOrdensServico(query) })
          ).subscribe(() => {});

        // Subscribe to 'range' field value changes
        this.eventForm.get('range').valueChanges.subscribe((value) => {

            if ( !value )
            {
                return;
            }

            // Set the 'start' field value from the range
            this.eventForm.get('start').setValue(value.start, {emitEvent: false});

            // If this is a recurring event...
            if ( this.eventForm.get('recurrence').value )
            {
                // Update the recurrence rules if needed
                this._updateRecurrenceRule();

                // Set the duration field
                const duration = moment(value.end).diff(moment(value.start), 'minutes');
                this.eventForm.get('duration').setValue(duration, {emitEvent: false});

                // Update the end value
                this._updateEndValue();
            }
            // Otherwise...
            else
            {
                // Set the end field
                this.eventForm.get('end').setValue(value.end, {emitEvent: false});
            }
        });

        // Get calendars
        this._agendaTecnicoService.calendars$
            .pipe(takeUntil(this._unsubscribeAll))
            .subscribe((calendars) => {

                // Store the calendars and events
                this.calendars = calendars;

                // Mark for check
                this._changeDetectorRef.markForCheck();
            });

        // Get events
        this._agendaTecnicoService.events$
            .pipe(takeUntil(this._unsubscribeAll))
            .subscribe((events) => {

                // Clone the events to change the object reference so
                // that the FullCalendar can trigger a re-render.
                this.events = cloneDeep(events);

                // Mark for check
                this._changeDetectorRef.markForCheck();
            });

        this.eventTimeFormat = {
            hour    : this.settings.timeFormat === '12' ? 'numeric' : '2-digit',
            hour12  : this.settings.timeFormat === '12',
            minute  : '2-digit',
            meridiem: this.settings.timeFormat === '12' ? 'short' : false,
        };

        // Subscribe to media changes
        this._fuseMediaWatcherService.onMediaChange$
            .pipe(takeUntil(this._unsubscribeAll))
            .subscribe(({matchingAliases}) => {
                // Set the drawerMode and drawerOpened if the given breakpoint is active
                if ( matchingAliases.includes('md') )
                {
                    this.drawerMode = 'side';
                    this.drawerOpened = true;
                }
                else
                {
                    this.drawerMode = 'over';
                    this.drawerOpened = false;
                }

                // Mark for check
                this._changeDetectorRef.markForCheck();
            });

        
        // Build the view specific FullCalendar options
        this.views = {
            dayGridMonth: {
                eventLimit     : 3,
                eventTimeFormat: this.eventTimeFormat,
                fixedWeekCount : false
            },
            timeGrid    : {
                allDayText        : '',
                columnHeaderFormat: {
                    weekday   : 'short',
                    day       : 'numeric',
                    omitCommas: true
                },
                columnHeaderHtml  : (date): string => `<span class="fc-weekday">${moment(date).format('ddd')}</span>
                                                       <span class="fc-date">${moment(date).format('D')}</span>`,
                slotDuration      : '01:00:00',
                slotLabelFormat   : this.eventTimeFormat,
            },
            timeGridWeek: {
                slotDuration: '01:00:00',
                minTime: '08:00:00',
                maxTime: '19:00:00',
            },
            timeGridDay : {
                slotDuration: '01:00:00',
                minTime: '08:00:00',
                maxTime: '19:00:00',
                nowIndicator: true
            },
            listYear    : {
                allDayText      : 'All day',
                eventTimeFormat : this.eventTimeFormat,
                listDayFormat   : false,
                listDayAltFormat: false
            }
        };
    }

    ngAfterViewInit(): void
    {
        // Get the full calendar API
        this._fullCalendarApi = this._fullCalendar.getApi();

        // Get the current view's title
        this.viewTitle = this._fullCalendarApi.view.title;

        // Get the view's current start and end dates, add/subtract
        // 60 days to create a ~150 days period to fetch the data for
        const viewStart = moment(this._fullCalendarApi.view.currentStart).subtract(30, 'days');
        const viewEnd = moment(this._fullCalendarApi.view.currentEnd).add(30, 'days');
    }

    ngOnDestroy(): void
    {
        // Unsubscribe from all subscriptions
        this._unsubscribeAll.next();
        this._unsubscribeAll.complete();

        // Dispose the overlay
        if ( this._eventPanelOverlayRef )
        {
            this._eventPanelOverlayRef.dispose();
        }
    }

    private async obterOrdensServico(filtro: string = '') {
        const params: OrdemServicoParameters = {
            codFiliais: "4",
            pa: 1,
            codStatusServicos: "1",
            filter: filtro,
            sortActive: 'codOS',
            sortDirection: 'desc'
        }

        const data = await this._ordemServicoService.obterPorParametros(params).toPromise();
        this.ordensServico = data.items;
        
    }

    toggleDrawer(): void
    {
        this._drawer.toggle();
    }
    
    changeEventPanelMode(panelMode: CalendarEventPanelMode, eventEditMode: CalendarEventEditMode = 'single'): void
    {
        // Set the panel mode
        this.panelMode = panelMode;

        // Set the event edit mode
        this.eventEditMode = eventEditMode;

        // Update the panel position
        setTimeout(() => {
            this._eventPanelOverlayRef.updatePosition();
        });
    }
    
    getCalendar(id): Calendar
    {
        if (!id)
        {
            return;
        }

        return this.calendars.find(calendar => calendar.id === id);
    }
    
    changeView(view: 'timeGridWeek' | 'timeGridDay' | 'listYear'): void
    {
        // Store the view
        this.view = view;

        // If the FullCalendar API is available...
        if ( this._fullCalendarApi )
        {
            // Set the view
            this._fullCalendarApi.changeView(view);

            // Update the view title
            this.viewTitle = this._fullCalendarApi.view.title;
        }
    }
    
    previous(): void
    {
        // Go to previous stop
        this._fullCalendarApi.prev();

        // Update the view title
        this.viewTitle = this._fullCalendarApi.view.title;

        // Get the view's current start date
        const start = moment(this._fullCalendarApi.view.currentStart);
    }
    
    today(): void
    {
        this._fullCalendarApi.today();
        this.viewTitle = this._fullCalendarApi.view.title;
    }
    
    next(): void
    {
        // Go to next stop
        this._fullCalendarApi.next();

        // Update the view title
        this.viewTitle = this._fullCalendarApi.view.title;

        // Get the view's current end date
        const end = moment(this._fullCalendarApi.view.currentEnd);

        // Prefetch future events
        //this._agendaTecnicoService.prefetchFutureEvents(end).subscribe();
    }
    
    onDateClick(calendarEvent): void
    {
        // Prepare the event
        const event = {
            id              : null,
            calendarId      : this.calendars[0].id,
            recurringEventId: null,
            isFirstInstance : false,
            title           : '',
            description     : '',
            color           : calendarEvent.color,
            start           : moment(calendarEvent.date).startOf('day').format('YYYY-MM-DD[T]HH:mm:ss.SSSZZ'),
            end             : moment(calendarEvent.date).endOf('day').format('YYYY-MM-DD[T]HH:mm:ss.SSSZZ'),
            duration        : null,
            allDay          : false,
            recurrence      : null,
            range           : {
                start: moment(calendarEvent.date).format('YYYY-MM-DD[T]HH:mm:ss.SSSZZ'),
                end  : moment(calendarEvent.date).add(1, 'hours').format('YYYY-MM-DD[T]HH:mm:ss.SSSZZ')
            }
        };

        // Set the event
        this.event = event;

        // Set the el on calendarEvent for consistency
        calendarEvent.el = calendarEvent.dayEl;

        // Reset the form and fill the event
        this.eventForm.reset();
        this.eventForm.patchValue(event);

        // Open the event panel
        this._openEventPanel(calendarEvent);

        // Change the event panel mode
        this.changeEventPanelMode('add');
    }
    
    onEventClick(calendarEvent): void
    {
        // Find the event with the clicked event's id
        const event: any = cloneDeep(this.events.find(item => item.id.toString() === calendarEvent.event.id));

        // Set the event
        this.event = event;

        event.range = {
            start: event?.start,
            end: event?.end
        };

        // Reset the form and fill the event
        this.eventForm.reset();
        this.eventForm.patchValue(event);

        // Open the event panel
        this._openEventPanel(calendarEvent);
    }
    
    onEventRender(calendarEvent): void
    {
        // Get event's calendar
        const calendar = this.calendars.find(item => item.id === calendarEvent.event.extendedProps.calendarId);

        // Return if the calendar doesn't exist...
        if ( !calendar )
        {
            return;
        }

        // Set the color class of the event
        calendarEvent.el.classList.add(calendar.color);

        // Set the event's title to '(No title)' if event title is not available
        if ( !calendarEvent.event.title )
        {
            //calendarEvent.el.querySelector('.fc-title').innerText = '(No title)';
        }

        // Set the event's visibility
        calendarEvent.el.style.display = calendar.visible ? 'flex' : 'none';
    }

    onEventDrop(calendarEvent): void {
        const event = this.events.find(item => item.id.toString() === calendarEvent.event.id);

        event.start = moment(calendarEvent.event.start).format('YYYY-MM-DD[T]HH:mm:ss.SSSZZ');
        event.end = moment(calendarEvent.event.end).format('YYYY-MM-DD[T]HH:mm:ss.SSSZZ');
        
        this._agendaTecnicoService.updateEvent(event.id.toString(), event).subscribe();
    }

    onEventResize(calendarEvent): void {
        const event = this.events.find(item => item.id.toString() === calendarEvent.event.id);

        event.start = moment(calendarEvent.event.start).format('YYYY-MM-DD[T]HH:mm:ss.SSSZZ');
        event.end = moment(calendarEvent.event.end).format('YYYY-MM-DD[T]HH:mm:ss.SSSZZ');
        
        this._agendaTecnicoService.updateEvent(event.id.toString(), event).subscribe();
    }
    
    onCalendarUpdated(calendar): void
    {
        this._fullCalendarApi.rerenderEvents();
    }
    
    addEvent(): void
    {
        // Get the clone of the event form value
        let newEvent = clone(this.eventForm.value);
        newEvent.dataHoraCad = moment().format('YYYY-MM-DD HH:mm:ss');
        newEvent.codUsuarioCad = this.userSession.usuario.codUsuario;
        newEvent.duration = newEvent.duration || 60;
        newEvent.title = newEvent.codOS;

        // Modify the event before sending it to the server
        newEvent = omit(newEvent, ['range', 'recurringEventId']);

        // Add the event
        this._agendaTecnicoService.addEvent(newEvent).subscribe(() => {
            this.obterOrdensServico();

            // Close the event panel
            this._closeEventPanel();
        });
    }

    updateEvent(): void
    {
        // Get the clone of the event form value
        let event = clone(this.eventForm.value);
        event.title = event.codOS;        
  
        // Update the event on the server
        this._agendaTecnicoService.updateEvent(event.id.toString(), event).subscribe(() => {
            this.obterOrdensServico();

            // Close the event panel
            this._closeEventPanel();
        });
    }
    
    deleteEvent(event, mode: CalendarEventEditMode = 'single'): void
    {
        // If the event is a recurring event...
        this._agendaTecnicoService.deleteEvent(event.id).subscribe(() => {

            // Close the event panel
            this._closeEventPanel();
        });
    }
    
    private _createEventPanelOverlay(positionStrategy): void
    {
        // Create the overlay
        this._eventPanelOverlayRef = this._overlay.create({
            panelClass    : ['calendar-event-panel'],
            backdropClass : '',
            hasBackdrop   : true,
            scrollStrategy: this._overlay.scrollStrategies.reposition(),
            positionStrategy
        });

        // Detach the overlay from the portal on backdrop click
        this._eventPanelOverlayRef.backdropClick().subscribe(() => {
            this._closeEventPanel();
        });
    }
    
    private _openEventPanel(calendarEvent): void
    {
        const positionStrategy = this._overlay.position().flexibleConnectedTo(calendarEvent.el).withFlexibleDimensions(false).withPositions([
            {
                originX : 'end',
                originY : 'top',
                overlayX: 'start',
                overlayY: 'top',
                offsetX : 8
            },
            {
                originX : 'start',
                originY : 'top',
                overlayX: 'end',
                overlayY: 'top',
                offsetX : -8
            },
            {
                originX : 'start',
                originY : 'bottom',
                overlayX: 'end',
                overlayY: 'bottom',
                offsetX : -8
            },
            {
                originX : 'end',
                originY : 'bottom',
                overlayX: 'start',
                overlayY: 'bottom',
                offsetX : 8
            }
        ]);

        // Create the overlay if it doesn't exist
        if ( !this._eventPanelOverlayRef )
        {
            this._createEventPanelOverlay(positionStrategy);
        }
        // Otherwise, just update the position
        else
        {
            this._eventPanelOverlayRef.updatePositionStrategy(positionStrategy);
        }

        // Attach the portal to the overlay
        this._eventPanelOverlayRef.attach(new TemplatePortal(this._eventPanel, this._viewContainerRef));

        // Mark for check
        this._changeDetectorRef.markForCheck();
    }
    
    private _closeEventPanel(): void
    {
        // Detach the overlay from the portal
        this._eventPanelOverlayRef.detach();

        // Reset the panel and event edit modes
        this.panelMode = 'view';
        this.eventEditMode = 'single';

        // Mark for check
        this._changeDetectorRef.markForCheck();
    }
    
    private _updateRecurrenceRule(): void
    {
        // Get the event
        const event = this.eventForm.value;

        // Return if this is a non-recurring event
        if ( !event.recurrence )
        {
            return;
        }

        // Parse the recurrence rule
        const parsedRules = {};
        event.recurrence.split(';').forEach((rule) => {

            // Split the rule
            const parsedRule = rule.split('=');

            // Add the rule to the parsed rules
            parsedRules[parsedRule[0]] = parsedRule[1];
        });

        // If there is a BYDAY rule, split that as well
        if ( parsedRules['BYDAY'] )
        {
            parsedRules['BYDAY'] = parsedRules['BYDAY'].split(',');
        }

        // Do not update the recurrence rule if ...
        // ... the frequency is DAILY,
        // ... the frequency is WEEKLY and BYDAY has multiple values,
        // ... the frequency is MONTHLY and there isn't a BYDAY rule,
        // ... the frequency is YEARLY,
        if ( parsedRules['FREQ'] === 'DAILY' ||
            (parsedRules['FREQ'] === 'WEEKLY' && parsedRules['BYDAY'].length > 1) ||
            (parsedRules['FREQ'] === 'MONTHLY' && !parsedRules['BYDAY']) ||
            parsedRules['FREQ'] === 'YEARLY' )
        {
            return;
        }

        // If the frequency is WEEKLY, update the BYDAY value with the new one
        if ( parsedRules['FREQ'] === 'WEEKLY' )
        {
            parsedRules['BYDAY'] = [moment(event.start).format('dd').toUpperCase()];
        }

        // If the frequency is MONTHLY, update the BYDAY value with the new one
        if ( parsedRules['FREQ'] === 'MONTHLY' )
        {
            // Calculate the weekday
            const weekday = moment(event.start).format('dd').toUpperCase();

            // Calculate the nthWeekday
            let nthWeekdayNo = 1;
            while ( moment(event.start).isSame(moment(event.start).subtract(nthWeekdayNo, 'week'), 'month') )
            {
                nthWeekdayNo++;
            }

            // Set the BYDAY
            parsedRules['BYDAY'] = [nthWeekdayNo + weekday];
        }

        // Generate the rule string from the parsed rules
        const rules = [];
        Object.keys(parsedRules).forEach((key) => {
            rules.push(key + '=' + (Array.isArray(parsedRules[key]) ? parsedRules[key].join(',') : parsedRules[key]));
        });
        const rrule = rules.join(';');

        // Update the recurrence rule
        this.eventForm.get('recurrence').setValue(rrule);
    }
    
    private _updateEndValue(): void
    {
        // Get the event recurrence
        const recurrence = this.eventForm.get('recurrence').value;

        // Return if this is a non-recurring event
        if ( !recurrence )
        {
            return;
        }

        // Parse the recurrence rule
        const parsedRules = {};
        recurrence.split(';').forEach((rule) => {

            // Split the rule
            const parsedRule = rule.split('=');

            // Add the rule to the parsed rules
            parsedRules[parsedRule[0]] = parsedRule[1];
        });

        // If there is an UNTIL rule...
        if ( parsedRules['UNTIL'] )
        {
            // Use that to set the end date
            this.eventForm.get('end').setValue(parsedRules['UNTIL']);

            // Return
            return;
        }

        // If there is a COUNT rule...
        if ( parsedRules['COUNT'] )
        {
            // Generate the RRule string
            const rrule = 'DTSTART=' + moment(this.eventForm.get('start').value).utc().format('YYYYMMDD[T]HHmmss[Z]') + '\nRRULE:' + recurrence;

            // Use RRule string to generate dates
            const dates = RRule.fromString(rrule).all();

            // Get the last date from dates array and set that as the end date
            this.eventForm.get('end').setValue(moment(dates[dates.length - 1]).toISOString());

            // Return
            return;
        }

        // If there are no UNTIL or COUNT, set the end date to a fixed value
        this.eventForm.get('end').setValue(moment().year(9999).endOf('year').toISOString());
    }
}
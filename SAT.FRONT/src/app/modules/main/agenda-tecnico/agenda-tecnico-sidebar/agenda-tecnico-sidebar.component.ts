import { Component, EventEmitter, OnDestroy, OnInit, Output, TemplateRef, ViewChild, ViewEncapsulation } from '@angular/core';
import { Overlay, OverlayRef } from '@angular/cdk/overlay';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { Calendar } from 'app/core/types/agenda-tecnico.types';
import { AgendaTecnicoService } from 'app/core/services/agenda-tecnico.service';

@Component({
    selector     : 'app-agenda-tecnico-sidebar',
    templateUrl  : './agenda-tecnico-sidebar.component.html',
    encapsulation: ViewEncapsulation.None
})
export class AgendaTecnicoSidebarComponent implements OnInit, OnDestroy
{
    @Output() readonly calendarUpdated: EventEmitter<any> = new EventEmitter<any>();
    @ViewChild('editPanel') private _editPanel: TemplateRef<any>;

    calendar: Calendar | null;
    calendars: Calendar[];
    private _editPanelOverlayRef: OverlayRef;
    private _unsubscribeAll: Subject<any> = new Subject<any>();

    constructor(
        private _agendaTecnicoService: AgendaTecnicoService,
        private _overlay: Overlay
    ) { }
    
    ngOnInit(): void
    {
        // Get calendars
        this._agendaTecnicoService.calendars$
            .pipe(takeUntil(this._unsubscribeAll))
            .subscribe((calendars) => {

                // Store the calendars
                this.calendars = calendars;
            });
    }
    
    ngOnDestroy(): void
    {
        // Unsubscribe from all subscriptions
        this._unsubscribeAll.next();
        this._unsubscribeAll.complete();

        // Dispose the overlay
        if ( this._editPanelOverlayRef )
        {
            this._editPanelOverlayRef.dispose();
        }
    }
    
    closeEditPanel(): void
    {
        // Detach the overlay from the portal
        if ( this._editPanelOverlayRef )
        {
            this._editPanelOverlayRef.detach();
        }
    }
    
    toggleCalendarVisibility(calendar: Calendar): void
    {
        // Toggle the visibility
        calendar.visible = !calendar.visible;

        // Update the calendar
        this.saveCalendar(calendar);
    }
    
    saveCalendar(calendar: Calendar): void
    {
        // If there is no id on the calendar...
        if ( !calendar.id )
        {
            // Add calendar to the server
            this._agendaTecnicoService.addCalendar(calendar).subscribe(() => {

                // Close the edit panel
                this.closeEditPanel();

                // Emit the calendarUpdated event
                this.calendarUpdated.emit();
            });
        }
        // Otherwise...
        else
        {
            // Update the calendar on the server
            this._agendaTecnicoService.updateCalendar(calendar.id, calendar).subscribe(() => {

                // Close the edit panel
                this.closeEditPanel();

                // Emit the calendarUpdated event
                this.calendarUpdated.emit();
            });
        }
    }
    
    deleteCalendar(calendar: Calendar): void
    {
        // Delete the calendar on the server
        this._agendaTecnicoService.deleteCalendar(calendar.id).subscribe(() => {

            // Close the edit panel
            this.closeEditPanel();

            // Emit the calendarUpdated event
            this.calendarUpdated.emit();
        });
    }
    
    private _createEditPanelOverlay(): void
    {
        // Create the overlay
        this._editPanelOverlayRef = this._overlay.create({
            hasBackdrop     : true,
            scrollStrategy  : this._overlay.scrollStrategies.reposition(),
            positionStrategy: this._overlay.position()
                                  .global()
                                  .centerHorizontally()
                                  .centerVertically()
        });

        // Detach the overlay from the portal on backdrop click
        this._editPanelOverlayRef.backdropClick().subscribe(() => {
            this.closeEditPanel();
            this.calendar = null;
        });
    }
}

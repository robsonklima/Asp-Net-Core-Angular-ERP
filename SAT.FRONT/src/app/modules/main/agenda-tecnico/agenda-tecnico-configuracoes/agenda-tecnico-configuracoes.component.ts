import { ChangeDetectionStrategy, ChangeDetectorRef, Component, OnDestroy, OnInit, ViewEncapsulation } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Subject } from 'rxjs';
import { AgendaTecnicoService } from 'app/core/services/agenda-tecnico.service';

@Component({
    selector       : 'app-agenda-tecnico-configuracoes',
    templateUrl    : './agenda-tecnico-configuracoes.component.html',
    changeDetection: ChangeDetectionStrategy.OnPush,
    encapsulation  : ViewEncapsulation.None
})
export class AgendaTecnicoConfiguracoesComponent implements OnInit, OnDestroy
{
    settingsForm: FormGroup;
    private _unsubscribeAll: Subject<any> = new Subject<any>();

    /**
     * Constructor
     */
    constructor(
        private _agendaTecnicoService: AgendaTecnicoService,
        private _changeDetectorRef: ChangeDetectorRef,
        private _formBuilder: FormBuilder
    )
    {
    }

    // -----------------------------------------------------------------------------------------------------
    // @ Accessors
    // -----------------------------------------------------------------------------------------------------

    /**
     * Getter for current year
     */
    get year(): string
    {
        return new Date().getFullYear().toString();
    }

    // -----------------------------------------------------------------------------------------------------
    // @ Lifecycle hooks
    // -----------------------------------------------------------------------------------------------------

    /**
     * On init
     */
    ngOnInit(): void
    {
        // Create the event form
        this.settingsForm = this._formBuilder.group({
            dateFormat : [''],
            timeFormat : [''],
            startWeekOn: ['']
        });

        this.settingsForm.patchValue({
            dateFormat : 'DD/MM/YYYY',
            timeFormat : '24',
            startWeekOn: 1,
        });
    }

    /**
     * On destroy
     */
    ngOnDestroy(): void
    {
        // Unsubscribe from all subscriptions
        this._unsubscribeAll.next();
        this._unsubscribeAll.complete();
    }
}

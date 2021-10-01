import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { MAT_DATE_FORMATS } from '@angular/material/core';
import { AgendaTecnicoComponent } from 'app/modules/main/agenda-tecnico/agenda-tecnico.component';
import { agendaTecnicoRoutes } from 'app/modules/main/agenda-tecnico/agenda-tecnico.routing';
import { TranslocoModule } from '@ngneat/transloco';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule, HttpClientJsonpModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MbscModule } from '@mobiscroll/angular';
import { SharedModule } from 'app/shared/shared.module';

export const FORMATO_DATA = {
    parse: {
        dateInput: 'LL',
    },
    display: {
        dateInput: 'DD/MM/yyyy',
        monthYearLabel: 'YYYY',
        dateA11yLabel: 'LL',
        monthYearA11yLabel: 'YYYY',
    },
};

@NgModule({
    declarations: [
        AgendaTecnicoComponent,
    ],
    imports: [
        RouterModule.forChild(agendaTecnicoRoutes),
        TranslocoModule,
        SharedModule,
        MbscModule,
        FormsModule,
        ReactiveFormsModule,
        HttpClientModule,
        HttpClientJsonpModule
    ],
    providers: [
        {
            provide : MAT_DATE_FORMATS,
            useValue: FORMATO_DATA
        }
    ]
})
export class AgendaTecnicoModule
{
}

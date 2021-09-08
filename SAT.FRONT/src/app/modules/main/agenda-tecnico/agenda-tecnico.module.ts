import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { ScrollingModule } from '@angular/cdk/scrolling';
import { MAT_DATE_FORMATS } from '@angular/material/core';
import { MatButtonModule } from '@angular/material/button';
import { MatButtonToggleModule } from '@angular/material/button-toggle';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatDialogModule } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatMenuModule } from '@angular/material/menu';
import { MatMomentDateModule } from '@angular/material-moment-adapter';
import { MatRadioModule } from '@angular/material/radio';
import { MatSelectModule } from '@angular/material/select';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatTooltipModule } from '@angular/material/tooltip';
import { FullCalendarModule } from '@fullcalendar/angular';
import { FuseDateRangeModule } from '@fuse/components/date-range';
import { SharedModule } from 'app/shared/shared.module';
import { AgendaTecnicoComponent } from 'app/modules/main/agenda-tecnico/agenda-tecnico.component';
import { AgendaTecnicoConfiguracoesComponent } from 'app/modules/main/agenda-tecnico/agenda-tecnico-configuracoes/agenda-tecnico-configuracoes.component';
import { AgendaTecnicoSidebarComponent } from 'app/modules/main/agenda-tecnico/agenda-tecnico-sidebar/agenda-tecnico-sidebar.component';
import { agendaTecnicoRoutes } from 'app/modules/main/agenda-tecnico/agenda-tecnico.routing';
import { TranslocoModule } from '@ngneat/transloco';

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
        AgendaTecnicoConfiguracoesComponent,
        AgendaTecnicoSidebarComponent
    ],
    imports     : [
        RouterModule.forChild(agendaTecnicoRoutes),
        ScrollingModule,
        MatButtonModule,
        MatButtonToggleModule,
        MatCheckboxModule,
        MatDatepickerModule,
        MatDialogModule,
        MatFormFieldModule,
        MatIconModule,
        MatInputModule,
        MatMenuModule,
        MatMomentDateModule,
        MatRadioModule,
        MatSelectModule,
        MatSidenavModule,
        MatTooltipModule,
        FullCalendarModule,
        FuseDateRangeModule,
        SharedModule,
        TranslocoModule
    ],
    providers   : [
        {
            provide : MAT_DATE_FORMATS,
            useValue: FORMATO_DATA
        }
    ]
})
export class AgendaTecnicoModule
{
}

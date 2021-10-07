import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { AgendaTecnicoComponent } from 'app/modules/main/agenda-tecnico/agenda-tecnico.component';
import { agendaTecnicoRoutes } from 'app/modules/main/agenda-tecnico/agenda-tecnico.routing';
import { TranslocoModule } from '@ngneat/transloco';
import { HttpClientModule, HttpClientJsonpModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MbscModule } from '@mobiscroll/angular';
import { SharedModule } from 'app/shared/shared.module';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatIconModule } from '@angular/material/icon';

@NgModule({
    declarations: [
        AgendaTecnicoComponent,
    ],
    imports: [
        RouterModule.forChild(agendaTecnicoRoutes),
        SharedModule,
        MbscModule,
        FormsModule,
        ReactiveFormsModule,
        HttpClientModule,
        HttpClientJsonpModule,
        TranslocoModule,
        MatSidenavModule,
        MatIconModule
    ],
    providers: [],
    bootstrap: [AgendaTecnicoComponent]

})
export class AgendaTecnicoModule
{
}

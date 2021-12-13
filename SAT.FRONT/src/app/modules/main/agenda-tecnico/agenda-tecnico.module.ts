import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { AgendaTecnicoComponent } from 'app/modules/main/agenda-tecnico/agenda-tecnico.component';
import { agendaTecnicoRoutes } from 'app/modules/main/agenda-tecnico/agenda-tecnico.routing';
import { TranslocoModule } from '@ngneat/transloco';
import { HttpClientModule, HttpClientJsonpModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MbscModule, MbscPopup, MbscPopupModule } from '@mobiscroll/angular';
import { SharedModule } from 'app/shared/shared.module';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatIconModule } from '@angular/material/icon';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MatButtonModule } from '@angular/material/button';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { RoteiroMapaComponent } from './roteiro-mapa/roteiro-mapa.component';
import { MatDialogModule } from '@angular/material/dialog';
import { MatOptionModule } from '@angular/material/core';
import { AgendaTecnicoFiltroComponent } from './agenda-tecnico-filtro/agenda-tecnico-filtro.component';
import { MatSelectModule } from '@angular/material/select';
import { AgendaTecnicoRealocacaoDialogComponent } from './agenda-tecnico-realocacao-dialog/agenda-tecnico-realocacao-dialog.component';
import { AgendaTecnicoChamadosFiltroComponent } from './agenda-tecnico-chamados-filtro/agenda-tecnico-chamados-filtro.component';
import { AgendaTecnicoChamadosComponent } from './agenda-tecnico-chamados/agenda-tecnico-chamados.component';

@NgModule({
    declarations: [
        AgendaTecnicoComponent,
        RoteiroMapaComponent,
        AgendaTecnicoFiltroComponent,
        AgendaTecnicoRealocacaoDialogComponent,
        AgendaTecnicoChamadosFiltroComponent,
        AgendaTecnicoChamadosComponent
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
        MatSelectModule,
        MatIconModule,
        MatTooltipModule,
        MatButtonModule,
        MatProgressSpinnerModule,
        MatFormFieldModule,
        MatInputModule,
        MatDialogModule,
        MatOptionModule,
        MatIconModule
    ],
    providers: [],
    bootstrap: [AgendaTecnicoComponent]

})
export class AgendaTecnicoModule
{
}

import { NgModule } from '@angular/core';
import { PontoPeriodoListaComponent } from './ponto-periodo-lista/ponto-periodo-lista.component';
import { RouterModule } from '@angular/router';
import { pontoRoutes } from './ponto.routing';
import { SharedModule } from 'app/shared/shared.module';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatIconModule } from '@angular/material/icon';
import { MatSortModule } from '@angular/material/sort';
import { MatMenuModule } from '@angular/material/menu';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { CommonModule } from '@angular/common';
import { TranslocoModule } from '@ngneat/transloco';
import { PontoColaboradorListaComponent } from './ponto-colaborador-lista/ponto-colaborador-lista.component';
import { MatButtonModule } from '@angular/material/button';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MatInputModule } from '@angular/material/input';
import { PontoPeriodoFormComponent } from './ponto-periodo-form/ponto-periodo-form.component';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { PontoTurnoListaComponent } from './ponto-turno-lista/ponto-turno-lista.component';
import { PontoTurnoFormComponent } from './ponto-turno-form/ponto-turno-form.component';
import { MatDatepickerModule } from '@angular/material/datepicker';

@NgModule({
  declarations: [
    PontoPeriodoListaComponent,
    PontoColaboradorListaComponent,
    PontoPeriodoFormComponent,
    PontoTurnoListaComponent,
    PontoTurnoFormComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(pontoRoutes),
    MatPaginatorModule,
    SharedModule,
    MatIconModule,
    MatSortModule,
    MatMenuModule,
    MatFormFieldModule,
    MatInputModule,
    MatProgressBarModule,
    TranslocoModule,
    MatButtonModule,
    MatTooltipModule,
    MatProgressSpinnerModule,
    MatDatepickerModule
  ]
})
export class PontoModule { }

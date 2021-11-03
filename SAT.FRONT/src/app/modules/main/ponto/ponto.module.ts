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

@NgModule({
  declarations: [
    PontoPeriodoListaComponent,
    PontoColaboradorListaComponent
  ],
  imports: [
    CommonModule,
    SharedModule,
    RouterModule.forChild(pontoRoutes),
    MatPaginatorModule,
    MatIconModule,
    MatSortModule,
    MatMenuModule,
    MatFormFieldModule,
    MatProgressBarModule,
    TranslocoModule,
    MatButtonModule,
    MatTooltipModule
  ]
})
export class PontoModule { }

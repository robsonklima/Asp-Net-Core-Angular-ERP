import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PontoPeriodoListaComponent } from './ponto-periodo-lista/ponto-periodo-lista.component';
import { RouterModule } from '@angular/router';
import { pontoRoutes } from './ponto.routing';

@NgModule({
  declarations: [
    PontoPeriodoListaComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(pontoRoutes),
  ]
})
export class PontoModule { }

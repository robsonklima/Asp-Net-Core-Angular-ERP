import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { despesaRoutes } from './despesa.routing';
import { DespesaTecnicoListaComponent } from './despesa-tecnico-lista/despesa-tecnico-lista.component';

@NgModule({
  declarations:
    [
      DespesaTecnicoListaComponent
    ],
  imports: [
    RouterModule.forChild(despesaRoutes),
    CommonModule
  ]
})
export class DespesaModule { }
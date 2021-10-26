import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { despesasRoutes } from './despesas.routing';
import { DespesasTecnicosListaComponent } from './despesas-tecnicos/despesas-tecnicos-lista/despesas-tecnicos-lista.component';

@NgModule({
  declarations:
    [
      DespesasTecnicosListaComponent
    ],
  imports: [
    RouterModule.forChild(despesasRoutes),
    CommonModule
  ]
})
export class DespesasModule { }
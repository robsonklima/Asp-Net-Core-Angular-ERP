import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { despesasTecnicosRoutes } from './despesas-tecnicos.routing';

@NgModule({
  declarations:
    [
    ],
  imports: [
    RouterModule.forChild(despesasTecnicosRoutes),
    CommonModule
  ]
})
export class DespesasTecnicosModule { }
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { despesaRoutes } from './despesa.routing';
import { DespesaTecnicoListaComponent } from './despesa-tecnico-lista/despesa-tecnico-lista.component';
import { DespesaAtendimentoListaComponent } from './despesa-atendimento-lista/despesa-atendimento-lista.component';
import { TranslocoModule } from '@ngneat/transloco';

@NgModule({
  declarations:
    [
      DespesaTecnicoListaComponent,
      DespesaAtendimentoListaComponent
    ],
  imports: [
    RouterModule.forChild(despesaRoutes),
    CommonModule,
    TranslocoModule
  ]
})
export class DespesaModule { }
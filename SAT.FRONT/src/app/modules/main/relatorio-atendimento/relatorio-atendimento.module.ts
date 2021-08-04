import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RelatorioAtendimentoFormComponent } from './relatorio-atendimento-form/relatorio-atendimento-form.component';
import { RouterModule } from '@angular/router';
import { relatorioAtendimentoRoutes } from './relatorio-atendimento.routing';

@NgModule({
  declarations: [
    RelatorioAtendimentoFormComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(relatorioAtendimentoRoutes)
  ]
})
export class RelatorioAtendimentoModule { }

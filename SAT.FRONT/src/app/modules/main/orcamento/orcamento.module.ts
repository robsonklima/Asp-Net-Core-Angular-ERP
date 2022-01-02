import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { OrcamentoListaComponent } from './orcamento-lista/orcamento-lista.component';
import { OrcamentoDetalheComponent } from './orcamento-detalhe/orcamento-detalhe.component';
import { orcamentoRoutes } from './orcamento.routing';
import { RouterModule } from '@angular/router';
import { SharedModule } from 'app/shared/shared.module';

@NgModule({
  declarations: [
    OrcamentoListaComponent,
    OrcamentoDetalheComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(orcamentoRoutes),
    SharedModule
  ]
})
export class OrcamentoModule { }

import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { InstalacaoContratoComponent } from './instalacao-contrato/instalacao-contrato.component';
import { RouterModule } from '@angular/router';
import { instalacaoRoutes } from './instalacao.routing';

@NgModule({
  declarations: [
    InstalacaoContratoComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(instalacaoRoutes)
  ]
})
export class InstalacaoModule { }

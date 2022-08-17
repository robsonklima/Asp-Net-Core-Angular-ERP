import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AuditoriaListaComponent } from './auditoria/auditoria-lista/auditoria-lista.component';
import { AuditoriaFormComponent } from './auditoria/auditoria-form/auditoria-form.component';
import { AuditoriaFiltroComponent } from './auditoria/auditoria-filtro/auditoria-filtro.component';
import { ValoresCombustivelListaComponent } from './valores-combustivel/valores-combustivel-lista/valores-combustivel-lista.component';
import { ValoresCombustivelFormComponent } from './valores-combustivel/valores-combustivel-form/valores-combustivel-form.component';
import { ValoresCombustivelFiltroComponent } from './valores-combustivel/valores-combustivel-filtro/valores-combustivel-filtro.component';



@NgModule({
  declarations: [
    AuditoriaListaComponent,
    AuditoriaFormComponent,
    AuditoriaFiltroComponent,
    ValoresCombustivelListaComponent,
    ValoresCombustivelFormComponent,
    ValoresCombustivelFiltroComponent,
  ],
  imports: [
    CommonModule
  ]
})
export class FrotaTecnicosModule { }

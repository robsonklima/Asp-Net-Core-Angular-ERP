import { CidadeFormComponent } from './cidade-form/cidade-form.component';
import { CidadeListaComponent } from './cidade-lista/cidade-lista.component';
import { cidadeRoutes } from './cidade.routing';

import { ListFormModule } from 'app/shared/listForm.module';
import { NgModule } from '@angular/core';

@NgModule({
  declarations: [
    CidadeFormComponent,
    CidadeListaComponent
  ],
  imports: [
    ListFormModule.configureRoutes(cidadeRoutes)
  ]
})

export class CidadeModule { }

import { NgModule } from '@angular/core';
import { Erro500Component } from './erro-500.component';
import { RouterModule } from '@angular/router';
import { erro500Routes } from './erro-500.routing';

@NgModule({
  declarations: [
    Erro500Component
  ],
  imports     : [
      RouterModule.forChild(erro500Routes)
  ]
})
export class Erro500Module { }

import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { Erro503Component } from './erro-503.component';
import { erro503Routes } from './erro-503.routing';

@NgModule({
  declarations: [
    Erro503Component
  ],
  imports     : [
      RouterModule.forChild(erro503Routes)
  ]
})
export class Erro503Module { }

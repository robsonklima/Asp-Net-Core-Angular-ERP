import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { regiaoRoutes } from './regiao.routing'

@NgModule({
  declarations: [

  ],
  imports: [
    CommonModule,
    RouterModule.forChild(regiaoRoutes)
  ]
})
export class RegiaoModule { }

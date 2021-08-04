import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { autorizadaRoutes } from './autorizada.routing'

@NgModule({
  declarations: [
    
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(autorizadaRoutes),
  ]
})
export class AutorizadaModule { }

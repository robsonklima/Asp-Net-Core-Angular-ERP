import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { JitsiComponent } from './jitsi/jitsi.component';
import { conferenciaRoutes } from './conferencia.routing'
import { RouterModule } from '@angular/router';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';

@NgModule({
  declarations: [
    JitsiComponent
  ],
  imports: [
    RouterModule.forChild(conferenciaRoutes),
    CommonModule,
    MatButtonModule,
    MatIconModule
  ]
})
export class ConferenciaModule { }

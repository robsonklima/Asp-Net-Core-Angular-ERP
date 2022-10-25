import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatOptionModule } from '@angular/material/core';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatTableModule } from '@angular/material/table';
import { MatTooltipModule } from '@angular/material/tooltip';
import { RouterModule } from '@angular/router';
import { FuseAlertModule } from '@fuse/components/alert';
import { TranslocoModule } from '@ngneat/transloco';
import { SharedModule } from 'app/shared/shared.module';
import { ContatoListaComponent } from './contato-lista/contato-lista.component';
import { contatoRoutes } from './contato.routing';

@NgModule({
  declarations: [
    ContatoListaComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(contatoRoutes),
    MatPaginatorModule,
    SharedModule,
    MatIconModule,
    MatFormFieldModule,
    MatInputModule,
    TranslocoModule,
    MatButtonModule,
    MatTooltipModule,
    MatProgressSpinnerModule,
    MatOptionModule,
    MatTableModule,
    MatCheckboxModule,
    FuseAlertModule,
  ]
})
export class ContatoModule { }

import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatInputModule } from '@angular/material/input';
import { SharedModule } from 'app/shared/shared.module';
import { TranslocoModule } from '@ngneat/transloco';
import { FuseHighlightModule } from '@fuse/components/highlight';
import { NgxMatSelectSearchModule } from 'ngx-mat-select-search';
import { MatSortModule } from '@angular/material/sort';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { MatSelectModule } from '@angular/material/select';
import { grupoEquipamentoRoutes } from './grupo-equipamento.routing'
import { GrupoEquipamentoListaComponent } from './grupo-equipamento-lista/grupo-equipamento-lista.component';
import { GrupoEquipamentoFormComponent } from './grupo-equipamento-form/grupo-equipamento-form.component';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatMenuModule } from '@angular/material/menu';

@NgModule({
  declarations: [
    GrupoEquipamentoListaComponent,
    GrupoEquipamentoFormComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(grupoEquipamentoRoutes),
    MatPaginatorModule,
    MatIconModule,
    MatFormFieldModule,
    MatButtonModule,
    TranslocoModule,
    SharedModule,
    FuseHighlightModule,
    NgxMatSelectSearchModule,
    MatInputModule,
    MatSortModule,
    MatProgressBarModule,
    MatSnackBarModule,
    MatSelectModule,
    MatProgressSpinnerModule,
    MatMenuModule
  ]
})
export class GrupoEquipamentoModule { }

import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatIconModule } from '@angular/material/icon';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatButtonModule } from '@angular/material/button';
import { TranslocoModule } from '@ngneat/transloco';
import { SharedModule } from 'app/shared/shared.module';
import { MatInputModule } from '@angular/material/input';
import { FuseHighlightModule } from '@fuse/components/highlight';
import { NgxMatSelectSearchModule } from 'ngx-mat-select-search';
import { MatSortModule } from '@angular/material/sort';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatSelectModule } from '@angular/material/select';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatMenuModule } from '@angular/material/menu';
import { MatTooltipModule } from '@angular/material/tooltip';
import { grupoEquipamentoRoutes } from './grupo-equipamento.routing';
import { GrupoEquipamentoFormComponent } from './grupo-equipamento-form/grupo-equipamento-form.component';
import { GrupoEquipamentoListaComponent } from './grupo-equipamento-lista/grupo-equipamento-lista.component';

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
    SharedModule,
    TranslocoModule,
    MatSortModule,
    MatInputModule,
    FuseHighlightModule,
    NgxMatSelectSearchModule,
    MatProgressBarModule,
    MatProgressSpinnerModule,
    MatCheckboxModule,
    MatSelectModule,
    MatDatepickerModule,
    MatMenuModule,
    MatTooltipModule
  ]
})
export class GrupoEquipamentoModule { }

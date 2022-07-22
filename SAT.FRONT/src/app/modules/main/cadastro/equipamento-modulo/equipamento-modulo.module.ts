import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { SharedModule } from '../../../../shared/shared.module';
import { MatPaginatorModule } from '@angular/material/paginator';
import { TranslocoModule } from '@ngneat/transloco';
import { MatButtonModule } from '@angular/material/button';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatMenuModule } from '@angular/material/menu';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatSelectModule } from '@angular/material/select';
import { MatSortModule } from '@angular/material/sort';
import { FuseHighlightModule } from '@fuse/components/highlight';
import { NgxMatSelectSearchModule } from 'ngx-mat-select-search';
import { MatTooltipModule } from '@angular/material/tooltip';
import { EquipamentoModuloFormComponent } from './equipamento-modulo-form/equipamento-modulo-form.component';
import { EquipamentoModuloListaComponent } from './equipamento-modulo-lista/equipamento-modulo-lista.component';
import { equipamentoModuloRoutes } from './equipamento-modulo.routing';
import { EquipamentoModuloFiltroComponent } from './equipamento-modulo-filtro/equipamento-modulo-filtro.component';
import { MatSidenav, MatSidenavModule } from '@angular/material/sidenav';
import { FiltroModule } from '../../filtros/filtro.module';

@NgModule({
  declarations: [
    EquipamentoModuloListaComponent,
    EquipamentoModuloFormComponent,
    EquipamentoModuloFiltroComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(equipamentoModuloRoutes),
    MatPaginatorModule,
    MatIconModule,
    MatFormFieldModule,
    MatSidenavModule,
    MatButtonModule,
    SharedModule,
    TranslocoModule,
    MatSortModule,
    MatInputModule,
    FuseHighlightModule,
    NgxMatSelectSearchModule,
    MatProgressBarModule,
    FiltroModule,
    MatProgressSpinnerModule,
    MatCheckboxModule,
    MatSelectModule,
    MatDatepickerModule,
    MatTooltipModule,
    MatMenuModule
  ]
})
export class EquipamentoModuloModule { }

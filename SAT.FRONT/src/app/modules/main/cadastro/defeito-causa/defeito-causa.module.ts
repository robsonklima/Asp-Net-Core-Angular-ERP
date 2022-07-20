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
import { DefeitoCausaFormComponent } from './defeito-causa-form/defeito-causa-form.component';
import { DefeitoCausaListaComponent } from './defeito-causa-lista/defeito-causa-lista.component';
import { defeitoCausaRoutes } from './defeito-causa.routing';
import { MatSidenavModule } from '@angular/material/sidenav';
import { DefeitoCausaFiltroComponent } from './defeito-causa-filtro/defeito-causa-filtro.component';
import { FiltroModule } from '../../filtros/filtro.module';

@NgModule({
  declarations: [
    DefeitoCausaListaComponent,
    DefeitoCausaFormComponent,
    DefeitoCausaFiltroComponent 
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(defeitoCausaRoutes),
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
    MatTooltipModule,
    MatSidenavModule,
    MatMenuModule,
    FiltroModule
  ]
})
export class DefeitoCausaModule { }

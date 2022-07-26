import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatIconModule } from '@angular/material/icon';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatButtonModule } from '@angular/material/button';
import { SharedModule } from 'app/shared/shared.module';
import { TranslocoModule } from '@ngneat/transloco';
import { MatSortModule } from '@angular/material/sort';
import { MatInputModule } from '@angular/material/input';
import { FuseHighlightModule } from '@fuse/components/highlight';
import { NgxMatSelectSearchModule } from 'ngx-mat-select-search';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatSelectModule } from '@angular/material/select';
import { IConfig, NgxMaskModule } from 'ngx-mask';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { filialRoutes } from './filial.routing';
import { MatMenuModule } from '@angular/material/menu';
import { MatTooltipModule } from '@angular/material/tooltip';
import { FilialFormComponent } from './filial-form/filial-form.component';
import { FilialListaComponent } from './filial-lista/filial-lista.component';
import { FilialFiltroComponent } from './filial-filtro/filial-filtro.component';
import { MatSidenavModule } from '@angular/material/sidenav';
import { FiltroModule } from '../../filtros/filtro.module';

const maskConfigFunction: () => Partial<IConfig> = () => {
  return {
    validation: false,
  };
};

@NgModule({
  declarations: [
    FilialListaComponent,
    FilialFormComponent,
    FilialFiltroComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(filialRoutes),
    NgxMaskModule.forRoot(maskConfigFunction),
    MatPaginatorModule,
    MatIconModule,
    MatFormFieldModule,
    MatSidenavModule,
    FiltroModule,
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
export class FilialModule { }

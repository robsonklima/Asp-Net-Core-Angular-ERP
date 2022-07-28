import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PecaListaComponent } from './peca-lista/peca-lista.component';
import { PecaFormComponent } from './peca-form/peca-form.component';
import { pecaRoutes } from './peca.routing';
import { RouterModule } from '@angular/router';
import { MatButtonModule } from '@angular/material/button';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatMenuModule } from '@angular/material/menu';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatSelectModule } from '@angular/material/select';
import { MatSortModule } from '@angular/material/sort';
import { FuseHighlightModule } from '@fuse/components/highlight';
import { TranslocoModule } from '@ngneat/transloco';
import { SharedModule } from 'app/shared/shared.module';
import { IConfig, NgxMaskModule } from 'ngx-mask';
import { NgxMatSelectSearchModule } from 'ngx-mat-select-search';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MatSidenavModule } from '@angular/material/sidenav';
import { FiltroModule } from '../../filtros/filtro.module';
import { PecaFiltroComponent } from './peca-filtro/peca-filtro.component';

const maskConfigFunction: () => Partial<IConfig> = () => {
  return {
      validation: false,
  };
};

@NgModule({
  declarations: [
    PecaListaComponent,
    PecaFormComponent,
    PecaFiltroComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(pecaRoutes),
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
    MatTooltipModule,
    NgxMaskModule
  ]
})
export class PecaModule { }

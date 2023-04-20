import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
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
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatSortModule } from '@angular/material/sort';
import { MatTooltipModule } from '@angular/material/tooltip';
import { RouterModule } from '@angular/router';
import { FuseHighlightModule } from '@fuse/components/highlight';
import { TranslocoModule } from '@ngneat/transloco';
import { SharedModule } from 'app/shared/shared.module';
import { IConfig, NgxMaskModule } from 'ngx-mask';
import { NgxMatSelectSearchModule } from 'ngx-mat-select-search';
import { FiltroModule } from '../../filtros/filtro.module';
import { TecnicoFiltroComponent } from './tecnico-filtro/tecnico-filtro.component';
import { TecnicoFormComponent } from './tecnico-form/tecnico-form.component';
import { TecnicoListaComponent } from './tecnico-lista/tecnico-lista.component';
import { tecnicoRoutes } from './tecnico.routing';
import { TecnicoContaListaComponent } from './tecnico-conta-lista/tecnico-conta-lista.component';
import { MatDialogModule } from '@angular/material/dialog';
import { TecnicoContaFormDialogComponent } from './tecnico-conta-form-dialog/tecnico-conta-form-dialog.component';

const maskConfigFunction: () => Partial<IConfig> = () => {
  return {
      validation: false,
  };
};

@NgModule({
  declarations: [
    TecnicoListaComponent,
    TecnicoFormComponent,
    TecnicoFiltroComponent,
    TecnicoContaListaComponent,
    TecnicoContaFormDialogComponent,
    TecnicoFormComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(tecnicoRoutes),
    NgxMaskModule.forRoot(maskConfigFunction),
    MatPaginatorModule,
    MatFormFieldModule,
    MatButtonModule,
    MatSidenavModule,
    FiltroModule,
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
    MatMenuModule,
    MatDialogModule,
    MatIconModule,
  ],
})
export class TecnicoModule { }

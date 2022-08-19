import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { conferenciaRoutes } from './conferencia.routing'
import { RouterModule } from '@angular/router';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { ConferenciaListaComponent } from './conferencia-lista/conferencia-lista.component';
import { ConferenciaFormComponent } from './conferencia-form/conferencia-form.component';
import { ConferenciaSalaComponent } from './conferencia-sala/conferencia-sala.component';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatSidenavModule } from '@angular/material/sidenav';
import { FiltroModule } from '../filtros/filtro.module';
import { SharedModule } from 'app/shared/shared.module';
import { TranslocoModule } from '@ngneat/transloco';
import { MatSortModule } from '@angular/material/sort';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatInputModule } from '@angular/material/input';
import { MatMenuModule } from '@angular/material/menu';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatSelectModule } from '@angular/material/select';
import { MatTooltipModule } from '@angular/material/tooltip';
import { FuseHighlightModule } from '@fuse/components/highlight';
import { NgxMatSelectSearchModule } from 'ngx-mat-select-search';
import { ConferenciaFiltroComponent } from './conferencia-filtro/conferencia-filtro.component';

@NgModule({
  declarations: [
    ConferenciaListaComponent,
    ConferenciaFormComponent,
    ConferenciaSalaComponent,
    ConferenciaFiltroComponent
  ],
  imports: [
    RouterModule.forChild(conferenciaRoutes),
    CommonModule,
    MatPaginatorModule,
    MatIconModule,
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
    MatMenuModule
  ]
})
export class ConferenciaModule { }

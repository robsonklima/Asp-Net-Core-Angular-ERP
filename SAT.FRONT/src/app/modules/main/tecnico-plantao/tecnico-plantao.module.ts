import { LOCALE_ID, NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { SharedModule } from 'app/shared/shared.module';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatIconModule } from '@angular/material/icon';
import { MatSortModule } from '@angular/material/sort';
import { MatMenuModule } from '@angular/material/menu';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { CommonModule } from '@angular/common';
import { TranslocoModule } from '@ngneat/transloco';
import { MatButtonModule } from '@angular/material/button';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MatInputModule } from '@angular/material/input';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatOptionModule } from '@angular/material/core';
import { MatSelectModule } from '@angular/material/select';
import { MatTableModule } from '@angular/material/table';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { FuseAlertModule } from '@fuse/components/alert';
import { ReactiveFormsModule } from '@angular/forms';
import { TecnicoPlantaoListaComponent } from './tecnico-plantao-lista/tecnico-plantao-lista.component';
import { tecnicoPlantaoRoutes } from './tecnico-plantao.routing';
import { TecnicoPlantaoInformacoesComponent } from './tecnico-plantao-informacoes/tecnico-plantao-informacoes.component';
import { TecnicoPlantaoRegioesComponent } from './tecnico-plantao-regioes/tecnico-plantao-regioes.component';
import { TecnicoPlantaoClientesComponent } from './tecnico-plantao-clientes/tecnico-plantao-clientes.component';
import { TecnicoPlantaoFormComponent } from './tecnico-plantao-form/tecnico-plantao-form.component';
import { NgxMatSelectSearchModule } from 'ngx-mat-select-search';
import { TecnicoPlantaoFiltroComponent } from './tecnico-plantao-filtro/tecnico-plantao-filtro.component';
import { FiltroModule } from '../filtros/filtro.module';
import { MatSidenavModule } from '@angular/material/sidenav';

@NgModule({
  declarations: [
    TecnicoPlantaoListaComponent,
    TecnicoPlantaoInformacoesComponent,
    TecnicoPlantaoRegioesComponent,
    TecnicoPlantaoClientesComponent,
    TecnicoPlantaoFormComponent,
    TecnicoPlantaoFiltroComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(tecnicoPlantaoRoutes),
    MatPaginatorModule,
    SharedModule,
    MatIconModule,
    MatSortModule,
    MatMenuModule,
    MatFormFieldModule,
    MatInputModule,
    MatProgressBarModule,
    FiltroModule,
    TranslocoModule,
    MatButtonModule,
    MatTooltipModule,
    MatProgressSpinnerModule,
    MatDatepickerModule,
    MatOptionModule,
    MatSelectModule,
    NgxMatSelectSearchModule,
    MatTableModule,
    MatCheckboxModule,
    MatSidenavModule,
    FuseAlertModule,
    ReactiveFormsModule
  ],
  providers: [{ provide: LOCALE_ID, useValue: 'pt' }]
})
export class TecnicoPlantaoModule { }

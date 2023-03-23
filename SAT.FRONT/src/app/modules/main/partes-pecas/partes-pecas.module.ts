import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatChipsModule } from '@angular/material/chips';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatDialogModule } from '@angular/material/dialog';
import { MatExpansionModule } from '@angular/material/expansion';
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
import { FiltroModule } from '../filtros/filtro.module';
import { PartesPecasControleDetalhesHistoricoComponent } from './controle/partes-pecas-controle-detalhes/partes-pecas-controle-detalhes-historico/partes-pecas-controle-detalhes-historico.component';
import { PartesPecasControleDetalhesComponent } from './controle/partes-pecas-controle-detalhes/partes-pecas-controle-detalhes.component';
import { PartesPecasControleFiltroComponent } from './controle/partes-pecas-controle-filtro/partes-pecas-controle-filtro.component';
import { PartesPecasControleListaComponent } from './controle/partes-peças-controle-lista/partes-pecas-controle-lista.component';
import { partesPecasRoutes } from './partes-pecas.routing';



const maskConfigFunction: () => Partial<IConfig> = () =>
{
    return {
        validation: false,
    };
};

@NgModule({
  declarations: [
    PartesPecasControleListaComponent,
    PartesPecasControleFiltroComponent,
    PartesPecasControleDetalhesComponent,
    PartesPecasControleDetalhesHistoricoComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(partesPecasRoutes),
    NgxMaskModule.forRoot(maskConfigFunction),
    MatPaginatorModule,
    MatIconModule,
    MatFormFieldModule,
    MatButtonModule,
    MatSidenavModule,
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
    FormsModule,
    FiltroModule,
    MatMenuModule,
    MatExpansionModule,
    MatChipsModule,
    MatDialogModule
  ]
})
export class PartesPecasModule { }

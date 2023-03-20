import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
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
import { FiltroModule } from '../filtros/filtro.module';
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
    PartesPecasControleListaComponent
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
  ]
})
export class PartesPecasModule { }

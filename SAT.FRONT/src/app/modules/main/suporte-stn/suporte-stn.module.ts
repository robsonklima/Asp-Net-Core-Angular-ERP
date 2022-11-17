import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { suporteSTNRoutes } from './suporte-stn.routing';
import { IConfig, NgxMaskModule } from 'ngx-mask';
import { MatPaginatorModule } from '@angular/material/paginator';
import { SharedModule } from 'app/shared/shared.module';
import { MatIconModule } from '@angular/material/icon';
import { MatSortModule } from '@angular/material/sort';
import { MatMenuModule } from '@angular/material/menu';
import { MatFormFieldControl, MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { TranslocoModule } from '@ngneat/transloco';
import { MatButtonModule } from '@angular/material/button';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatRippleModule } from '@angular/material/core';
import { MatSelectModule } from '@angular/material/select';
import { MatTableModule } from '@angular/material/table';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { FuseAlertModule } from '@fuse/components/alert';
import { MatSidenavModule } from '@angular/material/sidenav';
import { OrdemServicoSTNListaComponent } from './ordem-servico-stn-lista/ordem-servico-stn-lista.component';
import { OrdemServicoSTNFiltroComponent } from './ordem-servico-stn-filtro/ordem-servico-stn-filtro.component';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { MatDialogModule } from '@angular/material/dialog';
import { FuseHighlightModule } from '@fuse/components/highlight';
import { MatMomentDateModule } from '@angular/material-moment-adapter';
import { DragDropModule } from '@angular/cdk/drag-drop';
import { MatChipsModule } from '@angular/material/chips';
import { MatButtonToggleModule } from '@angular/material/button-toggle';
import { MatTabsModule } from '@angular/material/tabs';
import { NgxMatSelectSearchModule } from 'ngx-mat-select-search';
import { FuseCardModule } from '@fuse/components/card';
import { MatBadgeModule } from '@angular/material/badge';
import { MatListModule } from '@angular/material/list';
import { MatStepperModule } from '@angular/material/stepper';
import { FiltroModule } from '../filtros/filtro.module';
import { MatDividerModule } from '@angular/material/divider';
import { MatExpansionModule } from '@angular/material/expansion';
import { OrdemServicoStnFormComponent } from './ordem-servico-stn-form/ordem-servico-stn-form.component';
import { OrdemServicoStnHistoricoComponent } from './ordem-servico-stn-historico/ordem-servico-stn-historico.component';
import { SuporteStnLaudoFormComponent } from './suporte-stn-laudo/suporte-stn-laudo-form/suporte-stn-laudo-form.component';
import { SuporteStnLaudoListaComponent } from './suporte-stn-laudo/suporte-stn-laudo-lista/suporte-stn-laudo-lista.component';
import { SuporteStnLaudoFiltroComponent } from './suporte-stn-laudo/suporte-stn-laudo-filtro/suporte-stn-laudo-filtro.component';

const maskConfigFunction: () => Partial<IConfig> = () => {
  return {
    validation: false,
  };
};

@NgModule({
  declarations: [
    OrdemServicoSTNListaComponent,
    OrdemServicoSTNFiltroComponent,
    OrdemServicoStnFormComponent,
    OrdemServicoStnHistoricoComponent,
    SuporteStnLaudoFormComponent,
    SuporteStnLaudoListaComponent,
    SuporteStnLaudoFiltroComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(suporteSTNRoutes),
    NgxMaskModule.forRoot(maskConfigFunction),
    MatButtonToggleModule,
    MatChipsModule,
    MatDatepickerModule,
    MatDividerModule,
    DragDropModule,
    MatMomentDateModule,
    FuseHighlightModule,
    MatButtonModule,
    MatCheckboxModule,
    MatFormFieldModule,
    MatIconModule,
    MatInputModule,
    MatMenuModule,
    MatPaginatorModule,
    MatProgressBarModule,
    MatDialogModule,
    MatRippleModule,
    MatSortModule,
    MatSelectModule,
    MatExpansionModule,
    MatSlideToggleModule,
    SharedModule,
    MatTableModule,
    MatTabsModule,
    TranslocoModule,
    NgxMatSelectSearchModule,
    FuseCardModule,
    MatBadgeModule,
    MatSidenavModule,
    MatListModule,
    MatStepperModule,
    FuseAlertModule,
    MatProgressSpinnerModule,
    MatTooltipModule,
    FiltroModule,
    NgxMatSelectSearchModule
  ]
})
export class SuporteSTNModule { }

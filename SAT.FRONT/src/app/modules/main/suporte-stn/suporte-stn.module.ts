import { DragDropModule } from '@angular/cdk/drag-drop';
import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { MatMomentDateModule } from '@angular/material-moment-adapter';
import { MatBadgeModule } from '@angular/material/badge';
import { MatButtonModule } from '@angular/material/button';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatChipsModule } from '@angular/material/chips';
import { MatRippleModule } from '@angular/material/core';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatDialogModule } from '@angular/material/dialog';
import { MatDividerModule } from '@angular/material/divider';
import { MatExpansionModule } from '@angular/material/expansion';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatListModule } from '@angular/material/list';
import { MatMenuModule } from '@angular/material/menu';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatRadioModule } from '@angular/material/radio';
import { MatSelectModule } from '@angular/material/select';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { MatSortModule } from '@angular/material/sort';
import { MatStepperModule } from '@angular/material/stepper';
import { MatTableModule } from '@angular/material/table';
import { MatTabsModule } from '@angular/material/tabs';
import { MatTooltipModule } from '@angular/material/tooltip';
import { RouterModule } from '@angular/router';
import { FuseAlertModule } from '@fuse/components/alert';
import { FuseCardModule } from '@fuse/components/card';
import { FuseHighlightModule } from '@fuse/components/highlight';
import { TranslocoModule } from '@ngneat/transloco';
import { SharedModule } from 'app/shared/shared.module';
import { IConfig, NgxMaskModule } from 'ngx-mask';
import { NgxMatSelectSearchModule } from 'ngx-mat-select-search';
import { FiltroModule } from '../filtros/filtro.module';
import { OrdemServicoSTNFiltroComponent } from './ordem-servico-stn-filtro/ordem-servico-stn-filtro.component';
import { OrdemServicoStnFormAtendimentoComponent } from './ordem-servico-stn-form/ordem-servico-stn-form-atendimento/ordem-servico-stn-form-atendimento.component';
import { OrdemServicoStnFormHistoricoOsComponent } from './ordem-servico-stn-form/ordem-servico-stn-form-historico-os/ordem-servico-stn-form-historico-os.component';
import { OrdemServicoStnFormHistoricoComponent } from './ordem-servico-stn-form/ordem-servico-stn-form-historico/ordem-servico-stn-form-historico.component';
import { OrdemServicoStnFormInformacaoComponent } from './ordem-servico-stn-form/ordem-servico-stn-form-informacao/ordem-servico-stn-form-informacao.component';
import { OrdemServicoStnFormComponent } from './ordem-servico-stn-form/ordem-servico-stn-form.component';
import { OrdemServicoSTNListaComponent } from './ordem-servico-stn-lista/ordem-servico-stn-lista.component';
import { SuporteStnBloquearOSComponent } from './suporte-stn-bloquear-os/suporte-stn-bloquear-os.component';
import { SuporteStnLaudoFiltroComponent } from './suporte-stn-laudo/suporte-stn-laudo-filtro/suporte-stn-laudo-filtro.component';
import { SuporteStnLaudoFormAtendimentoComponent } from './suporte-stn-laudo/suporte-stn-laudo-form/suporte-stn-laudo-form-atendimento/suporte-stn-laudo-form-atendimento.component';
import { SuporteStnLaudoFormFotoDialogComponent } from './suporte-stn-laudo/suporte-stn-laudo-form/suporte-stn-laudo-form-foto/suporte-stn-laudo-form-foto-dialog/suporte-stn-laudo-form-foto-dialog.component';
import { SuporteStnLaudoFormFotoComponent } from './suporte-stn-laudo/suporte-stn-laudo-form/suporte-stn-laudo-form-foto/suporte-stn-laudo-form-foto.component';
import { SuporteStnLaudoFormInformacoesComponent } from './suporte-stn-laudo/suporte-stn-laudo-form/suporte-stn-laudo-form-informacoes/suporte-stn-laudo-form-informacoes.component';
import { SuporteStnLaudoFormComponent } from './suporte-stn-laudo/suporte-stn-laudo-form/suporte-stn-laudo-form.component';
import { SuporteStnLaudoListaComponent } from './suporte-stn-laudo/suporte-stn-laudo-lista/suporte-stn-laudo-lista.component';
import { suporteSTNRoutes } from './suporte-stn.routing';

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
    SuporteStnLaudoFormComponent,
    SuporteStnLaudoListaComponent,
    SuporteStnLaudoFiltroComponent,
    SuporteStnLaudoFormFotoComponent,
    SuporteStnLaudoFormFotoDialogComponent,
    SuporteStnLaudoFormInformacoesComponent,
    SuporteStnLaudoFormAtendimentoComponent,
    OrdemServicoStnFormInformacaoComponent,
    OrdemServicoStnFormHistoricoComponent,
    OrdemServicoStnFormAtendimentoComponent,
    OrdemServicoStnFormHistoricoOsComponent,
    SuporteStnBloquearOSComponent
  ],
  imports: [
    CommonModule,
    SharedModule,
    RouterModule.forChild(suporteSTNRoutes),
    NgxMaskModule.forRoot(maskConfigFunction),
    MatSlideToggleModule,
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
    NgxMatSelectSearchModule,
    MatRadioModule
  ]
})
export class SuporteSTNModule { }

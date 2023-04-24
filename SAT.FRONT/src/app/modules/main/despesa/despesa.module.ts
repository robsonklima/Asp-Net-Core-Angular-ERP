import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { MatMomentDateModule } from '@angular/material-moment-adapter';
import { MatBadgeModule } from '@angular/material/badge';
import { MatButtonModule } from '@angular/material/button';
import { MatButtonToggleModule } from '@angular/material/button-toggle';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatChipsModule } from '@angular/material/chips';
import { MatRippleModule } from '@angular/material/core';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatDialogModule } from '@angular/material/dialog';
import { MatDividerModule } from '@angular/material/divider';
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
import { DespesaAdiantamentoConsultaComponent } from './despesa-adiantamento-consulta/despesa-adiantamento-consulta.component';
import { DespesaAdiantamentoFiltroComponent } from './despesa-adiantamento-filtro/despesa-adiantamento-filtro.component';
import { DespesaAdiantamentoFormComponent } from './despesa-adiantamento-form/despesa-adiantamento-form.component';
import { DespesaAdiantamentoListaComponent } from './despesa-adiantamento-lista/despesa-adiantamento-lista.component';
import { DespesaAdiantamentoSolicitacaoComponent } from './despesa-adiantamento-solicitacao/despesa-adiantamento-solicitacao.component';
import { DespesaAtendimentoFiltroComponent } from './despesa-atendimento-filtro/despesa-atendimento-filtro.component';
import { DespesaAtendimentoAdiantamentoDialogComponent } from './despesa-atendimento-lista/despesa-atendimento-adiantamento-dialog/despesa-atendimento-adiantamento-dialog.component';
import { DespesaAtendimentoListaComponent } from './despesa-atendimento-lista/despesa-atendimento-lista.component';
import { DespesaAtendimentoObservacaoImpressaoComponent } from './despesa-atendimento-lista/despesa-atendimento-observacao-impressao/despesa-atendimento-observacao-impressao.component';
import { DespesaAtendimentoRelatorioImpressaoComponent } from './despesa-atendimento-lista/despesa-atendimento-relatorio-impressao/despesa-atendimento-relatorio-impressao.component';
import { DespesaAtendimentoReprovacaoListaComponent } from './despesa-atendimento-lista/despesa-atendimento-reprovacao-lista/despesa-atendimento-reprovacao-lista.component';
import { DespesaAtendimentoRelatorioListaComponent } from './despesa-atendimento-relatorio-lista/despesa-atendimento-relatorio-lista.component';
import { DespesaConfiguracaoFormComponent } from './despesa-configuracao-form/despesa-configuracao-form.component';
import { DespesaConfiguracaoListaComponent } from './despesa-configuracao-lista/despesa-configuracao-lista.component';
import { DespesaItemDialogComponent } from './despesa-manutencao/despesa-item-dialog/despesa-item-dialog.component';
import { DespesaManutencaoComponent } from './despesa-manutencao/despesa-manutencao.component';
import { DespesaPeriodoFiltroComponent } from './despesa-periodo-filtro/despesa-periodo-filtro.component';
import { DespesaPeriodoFormComponent } from './despesa-periodo-form/despesa-periodo-form.component';
import { DespesaPeriodoListaComponent } from './despesa-periodo-lista/despesa-periodo-lista.component';
import { DespesaProtocoloDetalheImpressaoComponent } from './despesa-protocolo-detalhe/despesa-protocolo-detalhe-impressao/despesa-protocolo-detalhe-impressao.component';
import { DespesaProtocoloDetalhePeriodosDialogComponent } from './despesa-protocolo-detalhe/despesa-protocolo-detalhe-periodos-dialog/despesa-protocolo-detalhe-periodos-dialog.component';
import { DespesaProtocoloDetalheComponent } from './despesa-protocolo-detalhe/despesa-protocolo-detalhe.component';
import { DespesaProtocoloFiltroComponent } from './despesa-protocolo-filtro/despesa-protocolo-filtro.component';
import { DespesaProtocoloFormComponent } from './despesa-protocolo-form/despesa-protocolo-form.component';
import { DespesaProtocoloListaComponent } from './despesa-protocolo-lista/despesa-protocolo-lista.component';
import { DespesaTecnicoFiltroComponent } from './despesa-tecnico-filtro/despesa-tecnico-filtro.component';
import { DespesaTecnicoListaComponent } from './despesa-tecnico-lista/despesa-tecnico-lista.component';
import { despesaRoutes } from './despesa.routing';

const maskConfigFunction: () => Partial<IConfig> = () =>
{
  return {
    validation: false,
  };
};

@NgModule({
  declarations:
    [
      DespesaTecnicoListaComponent,
      DespesaAtendimentoListaComponent,
      DespesaAtendimentoFiltroComponent,
      DespesaTecnicoFiltroComponent,
      DespesaAtendimentoRelatorioListaComponent,
      DespesaManutencaoComponent,
      DespesaItemDialogComponent,
      DespesaAdiantamentoListaComponent,
      DespesaAdiantamentoFiltroComponent,
      DespesaProtocoloListaComponent,
      DespesaProtocoloFiltroComponent,
      DespesaProtocoloDetalheComponent,
      DespesaProtocoloDetalhePeriodosDialogComponent,
      DespesaProtocoloDetalheImpressaoComponent,
      DespesaPeriodoListaComponent,
      DespesaPeriodoFiltroComponent,
      DespesaAtendimentoReprovacaoListaComponent,
      DespesaAtendimentoAdiantamentoDialogComponent,
      DespesaAtendimentoRelatorioImpressaoComponent,
      DespesaAtendimentoObservacaoImpressaoComponent,
      DespesaAdiantamentoConsultaComponent,
      DespesaPeriodoFormComponent,
      DespesaAdiantamentoFormComponent,
      DespesaConfiguracaoListaComponent,
      DespesaConfiguracaoFormComponent,
      DespesaAdiantamentoSolicitacaoComponent,
      DespesaProtocoloFormComponent
    ],
  imports: [
    RouterModule.forChild(despesaRoutes),
    NgxMaskModule.forRoot(maskConfigFunction),
    CommonModule,
    MatButtonToggleModule,
    MatChipsModule,
    MatDatepickerModule,
    MatDividerModule,
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
    MatSlideToggleModule,
    SharedModule,
    MatTableModule,
    MatTabsModule,
    NgxMatSelectSearchModule,
    FuseCardModule,
    MatBadgeModule,
    MatTooltipModule,
    MatSidenavModule,
    MatListModule,
    MatStepperModule,
    FuseAlertModule,
    MatProgressSpinnerModule,
    TranslocoModule,
    MatRadioModule,
    FiltroModule,
  ]
})
export class DespesaModule { }
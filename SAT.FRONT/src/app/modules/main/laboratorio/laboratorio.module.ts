import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatMomentDateModule } from "@angular/material-moment-adapter";
import { MatBadgeModule } from "@angular/material/badge";
import { MatButtonModule } from "@angular/material/button";
import { MatButtonToggleModule } from "@angular/material/button-toggle";
import { MatCheckboxModule } from "@angular/material/checkbox";
import { MatChipsModule } from "@angular/material/chips";
import { MatRippleModule } from "@angular/material/core";
import { MatDatepickerModule } from "@angular/material/datepicker";
import { MatDialogModule } from "@angular/material/dialog";
import { MatDividerModule } from "@angular/material/divider";
import { MatFormFieldModule } from "@angular/material/form-field";
import { MatIconModule } from "@angular/material/icon";
import { MatInputModule } from "@angular/material/input";
import { MatListModule } from "@angular/material/list";
import { MatMenuModule } from "@angular/material/menu";
import { MatPaginatorModule } from "@angular/material/paginator";
import { MatProgressBarModule } from "@angular/material/progress-bar";
import { MatProgressSpinnerModule } from "@angular/material/progress-spinner";
import { MatSelectModule } from "@angular/material/select";
import { MatSidenavModule } from "@angular/material/sidenav";
import { MatSlideToggleModule } from "@angular/material/slide-toggle";
import { MatSortModule } from "@angular/material/sort";
import { MatStepperModule } from "@angular/material/stepper";
import { MatTableModule } from "@angular/material/table";
import { MatTabsModule } from "@angular/material/tabs";
import { MatTooltipModule } from "@angular/material/tooltip";
import { RouterModule } from "@angular/router";
import { FuseAlertModule } from "@fuse/components/alert";
import { FuseCardModule } from "@fuse/components/card";
import { FuseHighlightModule } from "@fuse/components/highlight";
import { TranslocoModule } from "@ngneat/transloco";
import { SharedModule } from "app/shared/shared.module";
import { NgxMatSelectSearchModule } from "ngx-mat-select-search";
import { FiltroModule } from "../filtros/filtro.module";
import { MatRadioModule } from '@angular/material/radio';
import { DragDropModule } from '@angular/cdk/drag-drop';
import { IConfig, NgxMaskModule } from 'ngx-mask';
import { laboratorioRoutes } from './laboratorio.routing';
import { NgApexchartsModule } from 'ng-apexcharts';
import { LaboratorioPainelControleComponent } from './laboratorio-painel-controle/laboratorio-painel-controle.component';
import { PainelControlePecasComponent } from './laboratorio-painel-controle/painel-controle-pecas/painel-controle-pecas.component';
import { PainelControleTecnicosComponent } from './laboratorio-painel-controle/painel-controle-tecnicos/painel-controle-tecnicos.component';
import { PainelControleTecnicosItensComponent } from './laboratorio-painel-controle/painel-controle-tecnicos/painel-controle-tecnicos-itens/painel-controle-tecnicos-itens.component';
import { LaboratorioProcessoReparoListaComponent } from './laboratorio-processo-reparo/laboratorio-processo-reparo-lista/laboratorio-processo-reparo-lista.component';
import { LaboratorioProcessoReparoFiltroComponent } from './laboratorio-processo-reparo/laboratorio-processo-reparo-filtro/laboratorio-processo-reparo-filtro.component';
import { LaboratorioProcessoReparoDetalheComponent } from './laboratorio-processo-reparo/laboratorio-processo-reparo-detalhe/laboratorio-processo-reparo-detalhe.component';
import { LaboratorioBancadaComponent } from './laboratorio-bancada/laboratorio-bancada.component';
import { LaboratorioBancadaDialogComponent } from './laboratorio-bancada/laboratorio-bancada-dialog/laboratorio-bancada-dialog.component';
import { LaboratorioOrdemReparoListaComponent } from './laboratorio-ordem-reparo/laboratorio-ordem-reparo-lista/laboratorio-ordem-reparo-lista.component';
import { LaboratorioOrdemReparoFormComponent } from './laboratorio-ordem-reparo/laboratorio-ordem-reparo-form/laboratorio-ordem-reparo-form.component';
import { LaboratorioOrdemReparoFiltroComponent } from './laboratorio-ordem-reparo/laboratorio-ordem-reparo-filtro/laboratorio-ordem-reparo-filtro.component';
import { LaboratorioCheckListListaComponent } from './laboratorio-checklist/laboratorio-checklist-lista/laboratorio-checklist-lista.component';
import { LaboratorioChecklistFormComponent } from './laboratorio-checklist/laboratorio-checklist-form/laboratorio-checklist-form.component';
import { LaboratorioChecklistFiltroComponent } from './laboratorio-checklist/laboratorio-checklist-filtro/laboratorio-checklist-filtro.component';
import { LaboratorioChecklistItemFormDialogComponent } from './laboratorio-checklist/laboratorio-checklist-item-form-dialog/laboratorio-checklist-item-form-dialog.component';
import { LaboratorioProcessoReparoHistoricoComponent } from './laboratorio-processo-reparo/laboratorio-processo-reparo-historico/laboratorio-processo-reparo-historico.component';
import { LaboratorioProcessoReparoFormChecklistComponent } from './laboratorio-processo-reparo/laboratorio-processo-reparo-detalhe/laboratorio-processo-reparo-form-checklist/laboratorio-processo-reparo-form-checklist.component';
import { LaboratorioProcessoReparoFormDefeitoComponent } from './laboratorio-processo-reparo/laboratorio-processo-reparo-detalhe/laboratorio-processo-reparo-form-defeito/laboratorio-processo-reparo-form-defeito.component';
import { LaboratorioProcessoReparoFormSolucaoComponent } from './laboratorio-processo-reparo/laboratorio-processo-reparo-detalhe/laboratorio-processo-reparo-form-solucao/laboratorio-processo-reparo-form-solucao.component';
import { LaboratorioProcessoReparoFormComponent } from './laboratorio-processo-reparo/laboratorio-processo-reparo-detalhe/laboratorio-processo-reparo-form/laboratorio-processo-reparo-form.component';
import { LaboratorioProcessoReparoInsumoComponent } from './laboratorio-processo-reparo/laboratorio-processo-reparo-detalhe/laboratorio-processo-reparo-insumo/laboratorio-processo-reparo-insumo.component';
import { ProcessoReparoListaMaisOpcoesComponent } from './laboratorio-processo-reparo/laboratorio-processo-reparo-lista/processo-reparo-lista-mais-opcoes/processo-reparo-lista-mais-opcoes.component';
import { LaboratorioDashboardComponent } from './laboratorio-dashboard/laboratorio-dashboard.component';
import { LaboratorioDashboardTempoMedioReparoComponent } from './laboratorio-dashboard/laboratorio-dashboard-tempo-medio-reparo/laboratorio-dashboard-tempo-medio-reparo.component';
import { LaboratorioDashboardProdutividadeTecnicaComponent } from './laboratorio-dashboard/laboratorio-dashboard-produtividade-tecnica/laboratorio-dashboard-produtividade-tecnica.component';
import { LaboratorioDashboardItensAntigosComponent } from './laboratorio-dashboard/laboratorio-dashboard-itens-antigos/laboratorio-dashboard-itens-antigos.component';
import { LaboratorioDashboardReincidenciaComponent } from './laboratorio-dashboard/laboratorio-dashboard-reincidencia/laboratorio-dashboard-reincidencia.component';
import { LaboratorioDashboardItensRecebidosReparadosComponent } from './laboratorio-dashboard/laboratorio-dashboard-itens-recebidos-reparados/laboratorio-dashboard-itens-recebidos-reparados.component';
import { LaboratorioDashboardTopFaltantesComponent } from './laboratorio-dashboard/laboratorio-dashboard-top-faltantes/laboratorio-dashboard-top-faltantes.component';
import { LaboratorioOrcamentoListaComponent } from './laboratorio-orcamento/laboratorio-orcamento-lista/laboratorio-orcamento-lista.component';
import { LaboratorioOSBancadaFormComponent } from './laboratorio-os-bancada/laboratorio-os-bancada-form/laboratorio-os-bancada-form.component';
import { LaboratorioOSBancadaPecasDialogComponent } from './laboratorio-os-bancada/laboratorio-os-bancada-pecas-dialog/laboratorio-os-bancada-pecas-dialog.component';
import { LaboratorioOSBancadaListaComponent } from './laboratorio-os-bancada/laboratorio-os-bancada-lista/laboratorio-os-bancada-lista.component';
import { LaboratorioOSBancadaPecaRE5114ListaComponent } from './laboratorio-os-bancada/laboratorio-os-bancada-pecare5114-lista/laboratorio-os-bancada-pecare5114-lista.component';
import { LaboratorioOSBancadaPecaRE5114DialogComponent } from './laboratorio-os-bancada/laboratorio-os-bancada-pecare5114-dialog/laboratorio-os-bancada-pecare5114-dialog.component';
import { LaboratorioOrcamentoFormComponent } from './laboratorio-orcamento/laboratorio-orcamento-form/laboratorio-orcamento-form.component';
import { LaboratorioOrcamentoPecaListaComponent } from './laboratorio-orcamento/laboratorio-orcamento-peca-lista/laboratorio-orcamento-peca-lista.component';
import { LaboratorioOrcamentoPecaDialogComponent } from './laboratorio-orcamento/laboratorio-orcamento-peca-dialog/laboratorio-orcamento-peca-dialog.component';
import { LaboratorioOrcamentoFiltroComponent } from './laboratorio-orcamento/laboratorio-orcamento-filtro/laboratorio-orcamento-filtro.component';
import { LaboratorioOSBancadaFiltroComponent } from './laboratorio-os-bancada/laboratorio-os-bancada-filtro/laboratorio-os-bancada-filtro.component';
<<<<<<< HEAD
import { LaboratorioProcessoReparoInsumoDialogComponent } from './laboratorio-processo-reparo/laboratorio-processo-reparo-detalhe/laboratorio-processo-reparo-insumo/laboratorio-processo-reparo-insumo-dialog/laboratorio-processo-reparo-insumo-dialog.component';
=======
import { LaboratorioProcessoReparoInsumoDialogComponent } from './laboratorio-processo-reparo/laboratorio-processo-reparo-detalhe/laboratorio-processo-reparo-insumo/laboratorio-insumo-dialog/laboratorio-insumo-dialog.component';
>>>>>>> f9ac25bc22df48dee3388b691da552bbd470e89f



const maskConfigFunction: () => Partial<IConfig> = () =>
{
    return {
        validation: false,
    };
};

@NgModule({
  declarations: [
    LaboratorioPainelControleComponent,
    PainelControlePecasComponent,
    PainelControleTecnicosComponent,
    PainelControleTecnicosItensComponent,
    LaboratorioProcessoReparoListaComponent,
    LaboratorioProcessoReparoFiltroComponent,
    LaboratorioProcessoReparoDetalheComponent,
    LaboratorioBancadaComponent,
    LaboratorioBancadaDialogComponent,
    LaboratorioOrdemReparoListaComponent,
    LaboratorioOrdemReparoFormComponent,
    LaboratorioOrdemReparoFiltroComponent,
    LaboratorioCheckListListaComponent,
    LaboratorioChecklistFormComponent,
    LaboratorioChecklistFiltroComponent,
    LaboratorioChecklistItemFormDialogComponent,
    LaboratorioProcessoReparoHistoricoComponent,
    LaboratorioProcessoReparoFormDefeitoComponent,
    LaboratorioProcessoReparoFormSolucaoComponent,
    LaboratorioProcessoReparoFormChecklistComponent,
    LaboratorioProcessoReparoFormComponent,
    LaboratorioProcessoReparoInsumoComponent,
    ProcessoReparoListaMaisOpcoesComponent,
    LaboratorioDashboardComponent,
    LaboratorioDashboardItensRecebidosReparadosComponent,
    LaboratorioDashboardTopFaltantesComponent,
    LaboratorioDashboardTempoMedioReparoComponent,
    LaboratorioDashboardProdutividadeTecnicaComponent,
    LaboratorioDashboardItensAntigosComponent,
    LaboratorioDashboardReincidenciaComponent,
    LaboratorioOrcamentoListaComponent,
    LaboratorioOSBancadaFormComponent,
    LaboratorioOSBancadaPecasDialogComponent,
    LaboratorioOSBancadaListaComponent,
    LaboratorioOSBancadaPecaRE5114ListaComponent,
    LaboratorioOSBancadaPecaRE5114DialogComponent,
    LaboratorioOrcamentoFormComponent,
    LaboratorioOrcamentoPecaListaComponent,
    LaboratorioOrcamentoPecaDialogComponent,
    LaboratorioOrcamentoFiltroComponent,
    LaboratorioOSBancadaFiltroComponent,
    LaboratorioProcessoReparoInsumoDialogComponent

  ],
  imports: [
    CommonModule,
        CommonModule,
        RouterModule.forChild(laboratorioRoutes),
        NgxMaskModule.forRoot(maskConfigFunction),
        MatButtonToggleModule,
        MatChipsModule,
        MatDatepickerModule,
        MatDividerModule,
        DragDropModule,
        MatMomentDateModule,
        FuseHighlightModule,
        MatRadioModule,
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
        DragDropModule,
        NgxMatSelectSearchModule,
        NgApexchartsModule,
        
  ]
})
export class LaboratorioModule { }

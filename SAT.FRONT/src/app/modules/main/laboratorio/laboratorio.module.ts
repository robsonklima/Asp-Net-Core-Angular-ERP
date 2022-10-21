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
import { LaboratorioPainelControleComponent } from './laboratorio-painel-controle/laboratorio-painel-controle.component';
import { DragDropModule } from '@angular/cdk/drag-drop';
import { IConfig, NgxMaskModule } from 'ngx-mask';
import { laboratorioRoutes } from './laboratorio.routing';
import { PainelControlePecasComponent } from './laboratorio-painel-controle/painel-controle-pecas/painel-controle-pecas.component';
import { PainelControleTecnicosComponent } from './laboratorio-painel-controle/painel-controle-tecnicos/painel-controle-tecnicos.component';
import { PainelControleTecnicosItensComponent } from './laboratorio-painel-controle/painel-controle-tecnicos/painel-controle-tecnicos-itens/painel-controle-tecnicos-itens.component';
import { LaboratorioProcessoReparoListaComponent } from './laboratorio-processo-reparo/laboratorio-processo-reparo-lista/laboratorio-processo-reparo-lista.component';
import { LaboratorioProcessoReparoFiltroComponent } from './laboratorio-processo-reparo/laboratorio-processo-reparo-filtro/laboratorio-processo-reparo-filtro.component';
import { LaboratorioProcessoReparoDetalheComponent } from './laboratorio-processo-reparo/laboratorio-processo-reparo-detalhe/laboratorio-processo-reparo-detalhe.component';
import { LaboratorioBancadaComponent } from './laboratorio-bancada/laboratorio-bancada.component';
import { LaboratorioBancadaDialogComponent } from './laboratorio-bancada/laboratorio-bancada-dialog/laboratorio-bancada-dialog.component';
import { LaboratorioProcessoReparoFormComponent } from './laboratorio-processo-reparo/laboratorio-processo-reparo-form/laboratorio-processo-reparo-form.component';

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
    LaboratorioProcessoReparoFormComponent,
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
        NgxMatSelectSearchModule
  ]
})
export class LaboratorioModule { }

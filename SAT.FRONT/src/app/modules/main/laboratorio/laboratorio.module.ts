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

const maskConfigFunction: () => Partial<IConfig> = () =>
{
    return {
        validation: false,
    };
};

@NgModule({
  declarations: [
    LaboratorioPainelControleComponent
  ],
  imports: [
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
        NgxMatSelectSearchModule
  ]
})
export class LaboratorioModule { }

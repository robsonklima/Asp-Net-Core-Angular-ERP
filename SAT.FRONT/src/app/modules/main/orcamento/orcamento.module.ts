import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
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
import { OrcamentoDetalheLocalComponent } from "./orcamento-detalhe/orcamento-detalhe-local/orcamento-detalhe-local.component";
import { OrcamentoDetalheComponent } from "./orcamento-detalhe/orcamento-detalhe.component";
import { OrcamentoFiltroComponent } from "./orcamento-filtro/orcamento-filtro.component";
import { OrcamentoListaComponent } from "./orcamento-lista/orcamento-lista.component";
import { orcamentoRoutes } from "./orcamento.routing";
import { OrcamentoDetalheMaterialComponent } from './orcamento-detalhe/orcamento-detalhe-material/orcamento-detalhe-material.component';
import { OrcamentoDetalheMaoDeObraComponent } from './orcamento-detalhe/orcamento-detalhe-mao-de-obra/orcamento-detalhe-mao-de-obra.component';
import { OrcamentoDetalheDeslocamentoComponent } from './orcamento-detalhe/orcamento-detalhe-deslocamento/orcamento-detalhe-deslocamento.component';
import { OrcamentoDetalheOutroServicoComponent } from './orcamento-detalhe/orcamento-detalhe-outro-servico/orcamento-detalhe-outro-servico.component';
import { OrcamentoDetalheDescontoComponent } from './orcamento-detalhe/orcamento-detalhe-desconto/orcamento-detalhe-desconto.component';
import { IConfig, NgxMaskModule } from "ngx-mask";

const maskConfigFunction: () => Partial<IConfig> = () =>
{
    return {
        validation: false,
    };
};

@NgModule({
    declarations: [
        OrcamentoListaComponent,
        OrcamentoDetalheComponent,
        OrcamentoFiltroComponent,
        OrcamentoDetalheLocalComponent,
        OrcamentoDetalheMaterialComponent,
        OrcamentoDetalheMaoDeObraComponent,
        OrcamentoDetalheDeslocamentoComponent,
        OrcamentoDetalheOutroServicoComponent,
        OrcamentoDetalheDescontoComponent
    ],
    imports: [
        CommonModule,
        RouterModule.forChild(orcamentoRoutes),
        NgxMaskModule.forRoot(maskConfigFunction),
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
        FiltroModule
    ]
})
export class OrcamentoModule { }

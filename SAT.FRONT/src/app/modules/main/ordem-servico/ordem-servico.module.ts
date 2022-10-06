import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { MatButtonModule } from '@angular/material/button';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatMenuModule } from '@angular/material/menu';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { MatRippleModule } from '@angular/material/core';
import { MatTableModule } from '@angular/material/table';
import { MatSortModule } from '@angular/material/sort';
import { MatSelectModule } from '@angular/material/select';
import { FuseCardModule } from '@fuse/components/card';
import { MatTooltipModule } from '@angular/material/tooltip';
import { SharedModule } from 'app/shared/shared.module';
import { MatBadgeModule } from '@angular/material/badge';
import { MatListModule } from '@angular/material/list';
import { TranslocoModule } from '@ngneat/transloco';
import { MatButtonToggleModule } from '@angular/material/button-toggle';
import { MatChipsModule } from '@angular/material/chips';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatDividerModule } from '@angular/material/divider';
import { MatMomentDateModule } from '@angular/material-moment-adapter';
import { MatTabsModule } from '@angular/material/tabs';
import { FuseHighlightModule } from '@fuse/components/highlight';
import { NgxMatSelectSearchModule } from 'ngx-mat-select-search';
import { MatDialogModule } from '@angular/material/dialog';
import { ordemServicoRoutes } from 'app/modules/main/ordem-servico/ordem-servico.routing';
import { OrdemServicoListaComponent } from './ordem-servico-lista/ordem-servico-lista.component';
import { OrdemServicoFormComponent } from './ordem-servico-form/ordem-servico-form.component';
import { OrdemServicoDetalheComponent } from './ordem-servico-detalhe/ordem-servico-detalhe.component';
import { OrdemServicoImpressaoComponent } from './ordem-servico-impressao/ordem-servico-impressao.component';
import { OrdemServicoFotosComponent } from './ordem-servico-fotos/ordem-servico-fotos.component';
import { OrdemServicoTransferenciaComponent } from './ordem-servico-transferencia/ordem-servico-transferencia.component';
import { MatStepperModule } from '@angular/material/stepper';
import { FuseAlertModule } from '@fuse/components/alert';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { NgxMaskModule, IConfig } from 'ngx-mask';
import { OrdemServicoAgendamentoComponent } from './ordem-servico-agendamento/ordem-servico-agendamento.component';
import { OrdemServicoFiltroComponent } from './ordem-servico-filtro/ordem-servico-filtro.component';
import { OrdemServicoHistoricoComponent } from './ordem-servico-historico/ordem-servico-historico.component';
import { FiltroModule } from '../filtros/filtro.module';
import { OrdemServicoCancelamentoComponent } from './ordem-servico-cancelamento/ordem-servico-cancelamento.component';
import { OrdemServicoEmailDialogComponent } from './ordem-servico-email-dialog/ordem-servico-email-dialog.component';
import { OrdemServicoFotoComponent } from './ordem-servico-foto/ordem-servico-foto.component';
import { OrdemServicoPesquisaComponent } from './ordem-servico-pesquisa/ordem-servico-pesquisa.component';
import { OrdemServicoDetalheOrcamentosComponent } from './ordem-servico-detalhe/ordem-servico-detalhe-orcamentos/ordem-servico-detalhe-orcamentos.component';
import { OrdemServicoLaudoComponent } from './ordem-servico-laudo/ordem-servico-laudo.component';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { NotificationsModule } from 'app/layout/common/notifications/notifications.module';

const maskConfigFunction: () => Partial<IConfig> = () => { return { validation: false } };

@NgModule({
    declarations: [
        OrdemServicoListaComponent,
        OrdemServicoFormComponent,
        OrdemServicoDetalheComponent,
        OrdemServicoImpressaoComponent,
        OrdemServicoFotosComponent,
        OrdemServicoTransferenciaComponent,
        OrdemServicoAgendamentoComponent,
        OrdemServicoFiltroComponent,
        OrdemServicoHistoricoComponent,
        OrdemServicoCancelamentoComponent,
        OrdemServicoEmailDialogComponent,
        OrdemServicoFotoComponent,
        OrdemServicoPesquisaComponent,
        OrdemServicoDetalheOrcamentosComponent,
        OrdemServicoLaudoComponent
    ],
    imports: [
        RouterModule.forChild(ordemServicoRoutes),
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
        MatTooltipModule,
        MatSidenavModule,
        MatListModule,
        MatStepperModule,
        FuseAlertModule,
        MatProgressSpinnerModule,
        FiltroModule,
        NotificationsModule
    ]
})
export class OrdemServicoModule
{
}
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
import { NotificationsModule } from 'app/layout/common/notifications/notifications.module';
import { ordemServicoRoutes } from 'app/modules/main/ordem-servico/ordem-servico.routing';
import { SharedModule } from 'app/shared/shared.module';
import { IConfig, NgxMaskModule } from 'ngx-mask';
import { NgxMatSelectSearchModule } from 'ngx-mat-select-search';
import { FiltroModule } from '../filtros/filtro.module';
import { OrdemServicoAgendamentoComponent } from './ordem-servico-agendamento/ordem-servico-agendamento.component';
import { OrdemServicoCancelamentoComponent } from './ordem-servico-cancelamento/ordem-servico-cancelamento.component';
import { OrdemServicoAgendamentosComponent } from './ordem-servico-detalhe/ordem-servico-agendamentos/ordem-servico-agendamentos.component';
import { OrdemServicoAlertasComponent } from './ordem-servico-detalhe/ordem-servico-alertas/ordem-servico-alertas.component';
import { OrdemServicoDetalheComponent } from './ordem-servico-detalhe/ordem-servico-detalhe.component';
import { OrdemServicoInfoComponent } from './ordem-servico-detalhe/ordem-servico-info/ordem-servico-info.component';
import { OrdemServicoLaudosComponent } from './ordem-servico-detalhe/ordem-servico-laudos/ordem-servico-laudos.component';
import { OrdemServicoLogsComponent } from './ordem-servico-detalhe/ordem-servico-logs/ordem-servico-logs.component';
import { OrdemServicoOrcamentosComponent } from './ordem-servico-detalhe/ordem-servico-orcamentos/ordem-servico-orcamentos.component';
import { OrdemServicoRATFotosComponent } from './ordem-servico-detalhe/ordem-servico-rat-fotos/ordem-servico-rat-fotos.component';
import { OrdemServicoRatsComponent } from './ordem-servico-detalhe/ordem-servico-rats/ordem-servico-rats.component';
import { OrdemServicoEmailDialogComponent } from './ordem-servico-email-dialog/ordem-servico-email-dialog.component';
import { OrdemServicoFiltroComponent } from './ordem-servico-filtro/ordem-servico-filtro.component';
import { OrdemServicoFormComponent } from './ordem-servico-form/ordem-servico-form.component';
import { OrdemServicoFotoComponent } from './ordem-servico-foto/ordem-servico-foto.component';
import { OrdemServicoFotosComponent } from './ordem-servico-fotos/ordem-servico-fotos.component';
import { OrdemServicoHistoricoComponent } from './ordem-servico-historico/ordem-servico-historico.component';
import { OrdemServicoImpressaoComponent } from './ordem-servico-impressao/ordem-servico-impressao.component';
import { OrdemServicoListaComponent } from './ordem-servico-lista/ordem-servico-lista.component';
import { OrdemServicoPesquisaComponent } from './ordem-servico-pesquisa/ordem-servico-pesquisa.component';
import { OrdemServicoTransferenciaComponent } from './ordem-servico-transferencia/ordem-servico-transferencia.component';

const maskConfigFunction: () => Partial<IConfig> = () => { return { validation: false } };

@NgModule({
    declarations: [
        OrdemServicoListaComponent,
        OrdemServicoFormComponent,
        OrdemServicoDetalheComponent,
        OrdemServicoImpressaoComponent,
        OrdemServicoTransferenciaComponent,
        OrdemServicoAgendamentoComponent,
        OrdemServicoFiltroComponent,
        OrdemServicoHistoricoComponent,
        OrdemServicoCancelamentoComponent,
        OrdemServicoEmailDialogComponent,
        OrdemServicoFotoComponent,
        OrdemServicoFotosComponent,
        OrdemServicoPesquisaComponent,
        OrdemServicoOrcamentosComponent,
        OrdemServicoLaudosComponent,
        OrdemServicoInfoComponent,
        OrdemServicoRatsComponent,
        OrdemServicoRATFotosComponent,
        OrdemServicoAgendamentosComponent,
        OrdemServicoAlertasComponent,
        OrdemServicoLogsComponent
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
        MatSlideToggleModule,
        NotificationsModule
    ]
})
export class OrdemServicoModule
{
}
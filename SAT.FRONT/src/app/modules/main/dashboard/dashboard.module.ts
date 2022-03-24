import { MatTableModule } from '@angular/material/table';
import { LOCALE_ID, NgModule } from '@angular/core';
import { DashboardComponent } from './dashboard.component';
import { SharedModule } from 'app/shared/shared.module';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { RouterModule } from '@angular/router';
import { dashboardRoutes } from './dashboard.routing';
import { TranslocoModule } from '@ngneat/transloco';
import { MatButtonToggleModule } from '@angular/material/button-toggle';
import { LeafletModule } from '@asymmetrik/ngx-leaflet';
import { MatMenuModule } from '@angular/material/menu';
import { MatTabsModule } from '@angular/material/tabs';
import { CommonModule, registerLocaleData } from '@angular/common';
import { NgApexchartsModule } from 'ng-apexcharts';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatSelectModule } from '@angular/material/select';
import { ChamadosMaisAntigosComponent } from './chamados-mais-antigos/chamados-mais-antigos.component';
import { IndicadoresFiliaisComponent } from './indicadores-filiais/indicadores-filiais.component';
import { DisponibilidadeTecnicosComponent } from './disponibilidade-tecnicos/disponibilidade-tecnicos.component';
import { MediaGlobalAtendimentoTecnicoComponent } from './media-global-atendimento-tecnico/media-global-atendimento-tecnico.component';
import { DisponibilidadeBbtsFiliaisComponent } from './disponibilidade-bbts-filiais/disponibilidade-bbts-filiais.component';
import { DashboardSpaComponent } from './dashboard-spa/dashboard-spa.component';
import { SlaClientesComponent } from './sla-clientes/sla-clientes.component';
import { DisponibilidadeBBTSRegioesComponent } from './disponibilidade-bbts-regioes/disponibilidade-bbts-regioes.component';
import { ReincidenciaFiliaisComponent } from './reincidencia-filiais/reincidencia-filiais.component';
import { PendenciaFiliaisComponent } from './pendencia-filiais/pendencia-filiais.component';
import { EquipamentosMaisReincidentesComponent } from './equipamentos-mais-reincidentes/equipamentos-mais-reincidentes.component';
import { ReincidenciaClientesComponent } from './reincidencia-clientes/reincidencia-clientes.component';
import { PecasFaltantesFiliaisComponent } from './pecas-faltantes-filiais/pecas-faltantes-filiais.component';
import { CincoPecasMaisFaltantesComponent } from './cinco-pecas-mais-faltantes/cinco-pecas-mais-faltantes.component';
import { PecasFaltantesMaisCriticasComponent } from './pecas-faltantes-mais-criticas/pecas-faltantes-mais-criticas.component';
import { DensidadeComponent } from './densidade/densidade.component';
import { LeafletMarkerClusterModule } from '@asymmetrik/ngx-leaflet-markercluster';
import { DashboardFiltroComponent } from './dashboard-filtro/dashboard-filtro.component';
import { TecnicosReincidentesComponent } from './tecnicos-reincidentes/tecnicos-reincidentes.component';
import { TecnicosDesempenhoSpaComponent } from './tecnicos-desempenho-spa/tecnicos-desempenho-spa.component';
import ptBR from '@angular/common/locales/pt'
import { TecnicosPendentesComponent } from './tecnicos-pendentes/tecnicos-pendentes.component';
import { DisponibilidadeBbtsMultaComponent } from './disponibilidade-bbts-multa/disponibilidade-bbts-multa.component';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { IndicadoresFiliaisDetalhadosComponent } from './indicadores-filiais-detalhados/indicadores-filiais-detalhados.component';
import { IndicadorFilialDetalhadoPerformanceComponent } from './indicadores-filiais-detalhados/indicador-filial-detalhado-performance/indicador-filial-detalhado-performance.component';
import { IndicadorFilialDetalhadoChamadosAntigosComponent } from './indicadores-filiais-detalhados/indicador-filial-detalhado-chamados-antigos/indicador-filial-detalhado-chamados-antigos.component';
import { IndicadorFilialDetalhadoSlaPioresRegioesComponent } from './indicadores-filiais-detalhados/indicador-filial-detalhado-sla-piores-regioes/indicador-filial-detalhado-sla-piores-regioes.component';
import { FuseCardModule } from '@fuse/components/card';
import { IndicadorFilialDetalhadoSlaPioresClientesComponent } from './indicadores-filiais-detalhados/indicador-filial-detalhado-sla-piores-clientes/indicador-filial-detalhado-sla-piores-clientes.component';
import { IndicadorFilialDetalhadoSlaPioresTecnicosComponent } from './indicadores-filiais-detalhados/indicador-filial-detalhado-sla-piores-tecnicos/indicador-filial-detalhado-sla-piores-tecnicos.component';
import { IndicadorFilialDetalhadoPendenciaPioresTecnicosComponent } from './indicadores-filiais-detalhados/indicador-filial-detalhado-pendencia-piores-tecnicos/indicador-filial-detalhado-pendencia-piores-tecnicos.component';
import { IndicadorFilialDetalhadoPendenciaPioresClientesComponent } from './indicadores-filiais-detalhados/indicador-filial-detalhado-pendencia-piores-clientes/indicador-filial-detalhado-pendencia-piores-clientes.component';
import { IndicadorFilialDetalhadoPendenciaPioresRegioesComponent } from './indicadores-filiais-detalhados/indicador-filial-detalhado-pendencia-piores-regioes/indicador-filial-detalhado-pendencia-piores-regioes.component';
import { IndicadorFilialDetalhadoReincidenciaPioresRegioesComponent } from './indicadores-filiais-detalhados/indicador-filial-detalhado-reincidencia-piores-regioes/indicador-filial-detalhado-reincidencia-piores-regioes.component';
import { IndicadorFilialDetalhadoReincidenciaPioresClientesComponent } from './indicadores-filiais-detalhados/indicador-filial-detalhado-reincidencia-piores-clientes/indicador-filial-detalhado-reincidencia-piores-clientes.component';
import { IndicadorFilialDetalhadoReincidenciaPioresTecnicosComponent } from './indicadores-filiais-detalhados/indicador-filial-detalhado-reincidencia-piores-tecnicos/indicador-filial-detalhado-reincidencia-piores-tecnicos.component';
import { FuseAlertModule } from '@fuse/components/alert';
import { PendenciaQuadrimestreFilialComponent } from './pendencia-quadrimestre-filial/pendencia-quadrimestre-filial.component';
import { ReincidenciaQuadrimestreFilialComponent } from './reincidencia-quadrimestre-filial/reincidencia-quadrimestre-filial.component';
import { IndicadoresFiliaisDetalhadosDialogComponent } from './indicadores-filiais-detalhados-dialog/indicadores-filiais-detalhados-dialog.component';
registerLocaleData(ptBR);

@NgModule({
  declarations: [
    DashboardComponent,
    DashboardFiltroComponent,
    DisponibilidadeBBTSRegioesComponent,
    ChamadosMaisAntigosComponent,
    IndicadoresFiliaisComponent,
    DisponibilidadeTecnicosComponent,
    MediaGlobalAtendimentoTecnicoComponent,
    DisponibilidadeBbtsFiliaisComponent,
    DisponibilidadeBbtsMultaComponent,
    DashboardSpaComponent,
    SlaClientesComponent,
    ReincidenciaFiliaisComponent,
    TecnicosReincidentesComponent,
    PendenciaFiliaisComponent,
    TecnicosPendentesComponent,
    EquipamentosMaisReincidentesComponent,
    ReincidenciaClientesComponent,
    PecasFaltantesFiliaisComponent,
    CincoPecasMaisFaltantesComponent,
    PecasFaltantesMaisCriticasComponent,
    DensidadeComponent,
    TecnicosDesempenhoSpaComponent,
    IndicadoresFiliaisDetalhadosComponent,
    IndicadorFilialDetalhadoPerformanceComponent,
    IndicadorFilialDetalhadoChamadosAntigosComponent,
    IndicadorFilialDetalhadoSlaPioresRegioesComponent,
    IndicadorFilialDetalhadoSlaPioresClientesComponent,
    IndicadorFilialDetalhadoSlaPioresTecnicosComponent,
    IndicadorFilialDetalhadoPendenciaPioresTecnicosComponent,
    IndicadorFilialDetalhadoPendenciaPioresClientesComponent,
    IndicadorFilialDetalhadoPendenciaPioresRegioesComponent,
    IndicadorFilialDetalhadoReincidenciaPioresRegioesComponent,
    IndicadorFilialDetalhadoReincidenciaPioresClientesComponent,
    IndicadorFilialDetalhadoReincidenciaPioresTecnicosComponent,
    ReincidenciaQuadrimestreFilialComponent,
    PendenciaQuadrimestreFilialComponent,
    IndicadoresFiliaisDetalhadosDialogComponent,
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(dashboardRoutes),
    LeafletModule,
    MatButtonModule,
    MatIconModule,
    SharedModule,
    TranslocoModule,
    LeafletMarkerClusterModule,
    MatButtonToggleModule,
    MatMenuModule,
    MatTabsModule,
    MatTableModule,
    NgApexchartsModule,
    MatProgressSpinnerModule,
    MatSidenavModule,
    MatTooltipModule,
    MatDatepickerModule,
    MatFormFieldModule,
    MatSelectModule,
    MatProgressBarModule,
    FuseCardModule,
    FuseAlertModule
  ],
  providers: [
    { provide: LOCALE_ID, useValue: 'pt-BR' }
  ]
})
export class DashboardModule { }

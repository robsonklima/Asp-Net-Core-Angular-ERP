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
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
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
import { IndicadorFilialDetalhadoSlaRegioesComponent } from './indicadores-filiais-detalhados/indicador-filial-detalhado-sla-regioes/indicador-filial-detalhado-sla-regioes.component';
import { FuseCardModule } from '@fuse/components/card';
import { IndicadorFilialDetalhadoSlaClientesComponent } from './indicadores-filiais-detalhados/indicador-filial-detalhado-sla-clientes/indicador-filial-detalhado-sla-clientes.component';
import { IndicadorFilialDetalhadoSlaTecnicosComponent } from './indicadores-filiais-detalhados/indicador-filial-detalhado-sla-tecnicos/indicador-filial-detalhado-sla-tecnicos.component';
import { IndicadorFilialDetalhadoPendenciaTecnicosComponent } from './indicadores-filiais-detalhados/indicador-filial-detalhado-pendencia-tecnicos/indicador-filial-detalhado-pendencia-tecnicos.component';
import { IndicadorFilialDetalhadoPendenciaClientesComponent } from './indicadores-filiais-detalhados/indicador-filial-detalhado-pendencia-clientes/indicador-filial-detalhado-pendencia-clientes.component';
import { IndicadorFilialDetalhadoPendenciaRegioesComponent } from './indicadores-filiais-detalhados/indicador-filial-detalhado-pendencia-regioes/indicador-filial-detalhado-pendencia-regioes.component';
import { IndicadorFilialDetalhadoReincidenciaRegioesComponent } from './indicadores-filiais-detalhados/indicador-filial-detalhado-reincidencia-regioes/indicador-filial-detalhado-reincidencia-regioes.component';
import { IndicadorFilialDetalhadoReincidenciaClientesComponent } from './indicadores-filiais-detalhados/indicador-filial-detalhado-reincidencia-clientes/indicador-filial-detalhado-reincidencia-clientes.component';
import { IndicadorFilialDetalhadoReincidenciaTecnicosComponent } from './indicadores-filiais-detalhados/indicador-filial-detalhado-reincidencia-tecnicos/indicador-filial-detalhado-reincidencia-tecnicos.component';
import { FuseAlertModule } from '@fuse/components/alert';
import { PendenciaQuadrimestreFilialComponent } from './pendencia-quadrimestre-filial/pendencia-quadrimestre-filial.component';
import { ReincidenciaQuadrimestreFilialComponent } from './reincidencia-quadrimestre-filial/reincidencia-quadrimestre-filial.component';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatInputModule } from '@angular/material/input';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { MatSortModule } from '@angular/material/sort';
import { FuseHighlightModule } from '@fuse/components/highlight';
import { NgxMatSelectSearchModule } from 'ngx-mat-select-search';
import { FiltroModule } from '../filtros/filtro.module';
import { DensidadeFiltroComponent } from './densidade/densidade-filtro/densidade-filtro.component';
import { IndicadoresFiliaisOpcoesComponent } from './indicadores-filiais/indicadores-filiais-opcoes/indicadores-filiais-opcoes.component';
import { IndicadorFilialDetalhadoProdutividadeTecnicosComponent } from './indicadores-filiais-detalhados/indicador-filial-detalhado-produtividade-tecnicos/indicador-filial-detalhado-produtividade-tecnicos.component';
import { IndicadorFilialDetalhadoSpaTecnicosComponent } from './indicadores-filiais-detalhados/indicador-filial-detalhado-spa-tecnicos/indicador-filial-detalhado-spa-tecnicos.component';
import { IndicadorFilialDetalhadoSpaRegioesComponent } from './indicadores-filiais-detalhados/indicador-filial-detalhado-spa-regioes/indicador-filial-detalhado-spa-regioes.component';
import { IndicadorFilialDetalhadoSpaClientesComponent } from './indicadores-filiais-detalhados/indicador-filial-detalhado-spa-clientes/indicador-filial-detalhado-spa-clientes.component';
registerLocaleData(ptBR);

@NgModule({
  declarations: [
    DashboardComponent,
    DashboardFiltroComponent,
    DensidadeFiltroComponent,
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
    IndicadorFilialDetalhadoSlaRegioesComponent,
    IndicadorFilialDetalhadoSlaClientesComponent,
    IndicadorFilialDetalhadoSlaTecnicosComponent,
    IndicadorFilialDetalhadoPendenciaTecnicosComponent,
    IndicadorFilialDetalhadoPendenciaClientesComponent,
    IndicadorFilialDetalhadoPendenciaRegioesComponent,
    IndicadorFilialDetalhadoReincidenciaRegioesComponent,
    IndicadorFilialDetalhadoReincidenciaClientesComponent,
    IndicadorFilialDetalhadoReincidenciaTecnicosComponent,
    ReincidenciaQuadrimestreFilialComponent,
    PendenciaQuadrimestreFilialComponent,
    IndicadoresFiliaisOpcoesComponent,
    IndicadorFilialDetalhadoProdutividadeTecnicosComponent,
    IndicadorFilialDetalhadoSpaTecnicosComponent,
    IndicadorFilialDetalhadoSpaRegioesComponent,
    IndicadorFilialDetalhadoSpaClientesComponent,
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(dashboardRoutes),
    LeafletModule,
    LeafletMarkerClusterModule,
    MatButtonToggleModule,
    MatTabsModule,
    MatTableModule,
    NgApexchartsModule,
    MatSlideToggleModule,
    FuseCardModule,
    FuseAlertModule,
    MatPaginatorModule,
    MatTooltipModule,
    MatIconModule,
    MatFormFieldModule,
    MatButtonModule,
    TranslocoModule,
    SharedModule,
    FuseHighlightModule,
    MatInputModule,
    MatSortModule,
    MatProgressBarModule,
    MatSnackBarModule,
    MatSelectModule,
    MatProgressSpinnerModule,
    NgxMatSelectSearchModule,
    MatCheckboxModule,
    MatDatepickerModule,
    MatSidenavModule,
    MatMenuModule,
    FiltroModule
  ],
  providers: [
    { provide: LOCALE_ID, useValue: 'pt-BR' }
  ]
})
export class DashboardModule { }

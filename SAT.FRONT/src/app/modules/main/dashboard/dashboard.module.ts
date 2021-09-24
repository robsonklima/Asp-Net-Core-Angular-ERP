import { NgModule } from '@angular/core';
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
import { CommonModule } from '@angular/common';
import { NgApexchartsModule } from 'ng-apexcharts';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatSelectModule } from '@angular/material/select';
import { MapaComponent } from './mapa/mapa.component';
import { ChamadosMaisAntigosComponent } from './chamados-mais-antigos/chamados-mais-antigos.component';
import { IndicadoresFiliaisComponent } from './indicadores-filiais/indicadores-filiais.component';
import { DisponibilidadeTecnicosComponent } from './disponibilidade-tecnicos/disponibilidade-tecnicos.component';
import { MediaGlobalAtendimentoTecnicoComponent } from './media-global-atendimento-tecnico/media-global-atendimento-tecnico.component';
import { DisponibilidadeBbtsRegioesComponent } from './disponibilidade-bbts-regioes/disponibilidade-bbts-regioes.component';
import { DisponibilidadeBbtsFiliaisComponent } from './disponibilidade-bbts-filiais/disponibilidade-bbts-filiais.component';
import { DashboardSpaComponent } from './dashboard-spa/dashboard-spa.component';
import { SlaClientesComponent } from './sla-clientes/sla-clientes.component';
import { ReincidenciaFiliaisComponent } from './reincidencia-filiais/reincidencia-filiais.component';
import { TecnicosMaisReincidentesComponent } from './tecnicos-mais-reincidentes/tecnicos-mais-reincidentes.component';
import { TecnicosMenosReincidentesComponent } from './tecnicos-menos-reincidentes/tecnicos-menos-reincidentes.component';
import { PendenciaFiliaisComponent } from './pendencia-filiais/pendencia-filiais.component';
import { TecnicosMenosPendentesComponent } from './tecnicos-menos-pendentes/tecnicos-menos-pendentes.component';
import { TecnicosMaisPendentesComponent } from './tecnicos-mais-pendentes/tecnicos-mais-pendentes.component';
import { EquipamentosMaisReincidentesComponent } from './equipamentos-mais-reincidentes/equipamentos-mais-reincidentes.component';
import { ReincidenciaClientesComponent } from './reincidencia-clientes/reincidencia-clientes.component';
import { PecasFaltantesFiliaisComponent } from './pecas-faltantes-filiais/pecas-faltantes-filiais.component';
import { CincoPecasMaisFaltantesComponent } from './cinco-pecas-mais-faltantes/cinco-pecas-mais-faltantes.component';
import { PecasFaltantesMaisCriticasComponent } from './pecas-faltantes-mais-criticas/pecas-faltantes-mais-criticas.component';
import { MonitoramentoSatComponent } from './monitoramento-sat/monitoramento-sat.component';
import { DensidadeComponent } from './densidade/densidade.component';

@NgModule({
  declarations: [
    DashboardComponent,
    MapaComponent,
    ChamadosMaisAntigosComponent,
    IndicadoresFiliaisComponent,
    DisponibilidadeTecnicosComponent,
    MediaGlobalAtendimentoTecnicoComponent,
    DisponibilidadeBbtsRegioesComponent,
    DisponibilidadeBbtsFiliaisComponent,
    DashboardSpaComponent,
    SlaClientesComponent,
    ReincidenciaFiliaisComponent,
    TecnicosMaisReincidentesComponent,
    TecnicosMenosReincidentesComponent,
    PendenciaFiliaisComponent,
    TecnicosMenosPendentesComponent,
    TecnicosMaisPendentesComponent,
    EquipamentosMaisReincidentesComponent,
    ReincidenciaClientesComponent,
    PecasFaltantesFiliaisComponent,
    CincoPecasMaisFaltantesComponent,
    PecasFaltantesMaisCriticasComponent,
    MonitoramentoSatComponent,
    DensidadeComponent,
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(dashboardRoutes),
    LeafletModule,
    MatButtonModule,
    MatIconModule,
    SharedModule,
    TranslocoModule,
    MatButtonToggleModule,
    MatMenuModule,
    MatTabsModule,
    NgApexchartsModule,
    MatProgressSpinnerModule,
    MatSidenavModule,
    MatTooltipModule,
    MatDatepickerModule,
    MatFormFieldModule,
    MatSelectModule,
  ]
})
export class DashboardModule { }

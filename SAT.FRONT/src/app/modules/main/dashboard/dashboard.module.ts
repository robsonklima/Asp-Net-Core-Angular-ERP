import { NgModule } from '@angular/core';
import { DashboardComponent } from './dashboard.component';
import { SharedModule } from 'app/shared/shared.module';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { RouterModule } from '@angular/router';
import { dashboardRoutes } from './dashboard.routing';
import { TranslocoModule } from '@ngneat/transloco';
import { MatButtonToggleModule } from '@angular/material/button-toggle';
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
import { ResultadoGeralDssComponent } from './resultado-geral-dss/resultado-geral-dss.component';
import { DashboardSpaComponent } from './dashboard-spa/dashboard-spa.component';
import { SlaClientesComponent } from './sla-clientes/sla-clientes.component';
import { MapaDisponibilidadeComponent } from './mapa-disponibilidade/mapa-disponibilidade.component';

@NgModule({
  declarations: [
    DashboardComponent,
    MapaComponent,
    MapaDisponibilidadeComponent,
    ChamadosMaisAntigosComponent,
    IndicadoresFiliaisComponent,
    DisponibilidadeTecnicosComponent,
    MediaGlobalAtendimentoTecnicoComponent,
    DisponibilidadeBbtsRegioesComponent,
    DisponibilidadeBbtsFiliaisComponent,
    ResultadoGeralDssComponent,
    DashboardSpaComponent,
    SlaClientesComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(dashboardRoutes),
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

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
import { GraficoOrdemServicoComponent } from './grafico-ordem-servico/grafico-ordem-servico.component';
import { GraficoLinhasComponent } from './grafico-linhas/grafico-linhas.component';
import { GraficoMistoComponent } from './grafico-misto/grafico-misto.component';
import { GraficoAreaComponent } from './grafico-area/grafico-area.component';
import { GraficoTreemapComponent } from './grafico-treemap/grafico-treemap.component';
import { GraficoSLAComponent } from './grafico-sla/grafico-sla.component';
import { MatSidenavModule } from '@angular/material/sidenav';
import { DashboardFiltroComponent } from './dashboard-filtro/dashboard-filtro.component';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatSelectModule } from '@angular/material/select';

@NgModule({
  declarations: [
    DashboardComponent,
    GraficoOrdemServicoComponent,
    GraficoLinhasComponent,
    GraficoMistoComponent,
    GraficoAreaComponent,
    GraficoTreemapComponent,
    GraficoSLAComponent,
    DashboardFiltroComponent
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
    MatSelectModule
  ]
})
export class DashboardModule { }

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
import { GraficoOrdemServicoClienteComponent } from './grafico-ordem-servico-cliente/grafico-ordem-servico-cliente.component';
import { GraficoOrdemServicoFilialComponent } from './grafico-ordem-servico-filial/grafico-ordem-servico-filial.component';
import { GraficoSLAFilialComponent } from './grafico-sla-filial/grafico-sla-filial.component';
import { GraficoSLAClienteComponent } from './grafico-sla-cliente/grafico-sla-cliente.component';
import { MatSidenavModule } from '@angular/material/sidenav';
import { DashboardFiltroComponent } from './dashboard-filtro/dashboard-filtro.component';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatSelectModule } from '@angular/material/select';
import { GraficoOrdemServicoDataComponent } from './grafico-ordem-servico-data/grafico-ordem-servico-data.component';
import { GraficoPendenciaClienteComponent } from './grafico-pendencia-cliente/grafico-pendencia-cliente.component';
import { GraficoPendenciaFilialComponent } from './grafico-pendencia-filial/grafico-pendencia-filial.component';
import { GraficoReincidenciaClienteComponent } from './grafico-reincidencia-cliente/grafico-reincidencia-cliente.component';
import { GraficoReincidenciaFilialComponent } from './grafico-reincidencia-filial/grafico-reincidencia-filial.component';
import { GraficoSPAClienteComponent } from './grafico-spa-cliente/grafico-spa-cliente.component';
import { GraficoSPAFilialComponent } from './grafico-spa-filial/grafico-spa-filial.component';
import { GraficoSLAComponent } from './grafico-sla/grafico-sla.component';
import { TelaMapaComponent } from './tela-mapa/tela-mapa.component';

@NgModule({
  declarations: [
    DashboardComponent,
    GraficoOrdemServicoClienteComponent,
    GraficoOrdemServicoFilialComponent,
    GraficoOrdemServicoDataComponent,
    GraficoPendenciaClienteComponent,
    GraficoSPAClienteComponent,
    GraficoSPAFilialComponent,
    GraficoPendenciaFilialComponent,
    GraficoReincidenciaClienteComponent,
    GraficoReincidenciaFilialComponent,
    GraficoSLAComponent,
    GraficoSLAClienteComponent,
    GraficoSLAFilialComponent,
    DashboardFiltroComponent,
    TelaMapaComponent ],
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

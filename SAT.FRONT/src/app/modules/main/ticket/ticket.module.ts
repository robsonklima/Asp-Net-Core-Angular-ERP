import { LOCALE_ID, NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatSortModule } from '@angular/material/sort';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { SharedModule } from 'app/shared/shared.module';
import { TranslocoModule } from '@ngneat/transloco';
import { FuseHighlightModule } from '@fuse/components/highlight';
import { NgxMatSelectSearchModule } from 'ngx-mat-select-search';
import { MatInputModule } from '@angular/material/input';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { MatSelectModule } from '@angular/material/select';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { IConfig, NgxMaskModule } from 'ngx-mask';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { ticketRoutes } from './ticket.routing';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatMenuModule } from '@angular/material/menu';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MatSidenavModule } from '@angular/material/sidenav';
import { TicketFiltroComponent } from './ticket-filtro/ticket-filtro.component';
import { TicketFormComponent } from './ticket-detalhe/ticket-form/ticket-form.component';
import { TicketListaComponent } from './ticket-lista/ticket-lista.component';
import { MatTabsModule } from '@angular/material/tabs';
import { FuseCardModule } from '@fuse/components/card';
import { MatDialogModule } from '@angular/material/dialog';
import { FiltroModule } from '../filtros/filtro.module';
import { DragDropModule } from '@angular/cdk/drag-drop';
import { TicketAnexoFormDialogComponent } from './ticket-detalhe/ticket-anexos/ticket-anexo-form-dialog/ticket-anexo-form-dialog.component';
import { TicketDetalheComponent } from './ticket-detalhe/ticket-detalhe.component';
import { TicketAtendimentoFormDialogComponent } from './ticket-detalhe/ticket-atendimentos/ticket-atendimento-form-dialog/ticket-atendimento-form-dialog.component';
import { TicketAtendimentosComponent } from './ticket-detalhe/ticket-atendimentos/ticket-atendimentos.component';
import { TicketAnexosComponent } from './ticket-detalhe/ticket-anexos/ticket-anexos.component';
import { registerLocaleData } from '@angular/common';
import localeBr from '@angular/common/locales/pt';
import { TicketGraficosComponent } from './ticket-graficos/ticket-graficos.component';
import { NgApexchartsModule } from 'ng-apexcharts';
import { MatButtonToggleModule } from '@angular/material/button-toggle';
import { QuillModule } from 'ngx-quill';
import { FuseAlertModule } from '@fuse/components/alert';
registerLocaleData(localeBr, 'pt')

const maskConfigFunction: () => Partial<IConfig> = () => {
  return {
    validation: false,
  };
};

@NgModule({
  declarations: [
    TicketListaComponent,
    TicketFormComponent,
    TicketFiltroComponent,
    TicketAtendimentoFormDialogComponent,
    TicketAnexoFormDialogComponent,
    TicketDetalheComponent,
    TicketAtendimentosComponent,
    TicketAnexosComponent,
    TicketGraficosComponent,
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(ticketRoutes),
    NgxMaskModule.forRoot(maskConfigFunction),
    QuillModule.forRoot(),
    MatPaginatorModule,
    MatTooltipModule,
    NgApexchartsModule,
    MatIconModule,
    MatFormFieldModule,
    MatButtonModule,
    TranslocoModule,
    MatDialogModule,
    SharedModule,
    FuseHighlightModule,
    MatTabsModule,
    NgxMatSelectSearchModule,
    MatInputModule,
    MatSortModule,
    MatProgressBarModule,
    DragDropModule,
    MatSnackBarModule,
    MatSelectModule,
    MatProgressSpinnerModule,
    NgxMatSelectSearchModule,
    MatCheckboxModule,
    MatDatepickerModule,
    MatSidenavModule,
    MatMenuModule,
    FiltroModule,
    MatButtonToggleModule,
    FuseCardModule,
    FuseAlertModule
  ],
  providers: [
    { provide: LOCALE_ID, useValue: 'pt-BR' }
  ]
})
export class TicketModule { }

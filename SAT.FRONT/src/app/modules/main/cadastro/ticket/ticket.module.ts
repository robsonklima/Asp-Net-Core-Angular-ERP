import { FiltroModule } from '../../filtros/filtro.module';
import { NgModule } from '@angular/core';
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
import { TicketFiltroComponent } from './ticekt-filtro/ticket-filtro.component';
import { TicketFormComponent } from './ticket-form/ticket-form.component';
import { TicketListaComponent } from './ticket-lista/ticket-lista.component';
import { TicketDetalheComponent } from './ticket-detalhe/ticket-detalhe.component';
import { MatTabsModule } from '@angular/material/tabs';
import { FuseCardModule } from '@fuse/components/card';

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
    TicketDetalheComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(ticketRoutes),
    NgxMaskModule.forRoot(maskConfigFunction),
    MatPaginatorModule,
    MatTooltipModule,
    MatIconModule,
    MatFormFieldModule,
    MatButtonModule,
    TranslocoModule,
    SharedModule,
    FuseHighlightModule,
    MatTabsModule,
    NgxMatSelectSearchModule,
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
    FiltroModule,
    FuseCardModule
  ]
})
export class TicketModule { }

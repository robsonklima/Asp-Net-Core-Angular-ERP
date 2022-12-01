import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatMenuModule } from '@angular/material/menu';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatSelectModule } from '@angular/material/select';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatSortModule } from '@angular/material/sort';
import { MatTooltipModule } from '@angular/material/tooltip';
import { RouterModule } from '@angular/router';
import { FuseHighlightModule } from '@fuse/components/highlight';
import { TranslocoModule } from '@ngneat/transloco';
import { SharedModule } from 'app/shared/shared.module';
import { NgxMatSelectSearchModule } from 'ngx-mat-select-search';
import { FiltroModule } from '../filtros/filtro.module';
import { mensagemRoutes } from './mensagem.routing';
import { MensagemTecnicoFiltroComponent } from './tecnico/mensagem-tecnico-filtro/mensagem-tecnico-filtro.component';
import { MensagemTecnicoFormComponent } from './tecnico/mensagem-tecnico-form/mensagem-tecnico-form.component';
import { MensagemTecnicoListaComponent } from './tecnico/mensagem-tecnico-lista/mensagem-tecnico-lista.component';

@NgModule({
  declarations: [
    MensagemTecnicoListaComponent,
    MensagemTecnicoFormComponent,
    MensagemTecnicoFiltroComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(mensagemRoutes),
    MatPaginatorModule,
    MatIconModule,
    MatFormFieldModule,
    MatButtonModule,
    MatSidenavModule,
    SharedModule,
    TranslocoModule,
    MatSortModule,
    MatInputModule,
    FuseHighlightModule,
    NgxMatSelectSearchModule,
    MatProgressBarModule,
    MatProgressSpinnerModule,
    MatCheckboxModule,
    MatSelectModule,
    MatDatepickerModule,
    MatTooltipModule,
    FormsModule,
    FiltroModule,
    MatMenuModule,
  ]
})
export class MensagemModule { }

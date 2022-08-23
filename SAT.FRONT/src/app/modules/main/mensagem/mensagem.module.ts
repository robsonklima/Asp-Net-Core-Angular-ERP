import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { MatPaginatorModule } from '@angular/material/paginator';
import { SharedModule } from 'app/shared/shared.module';
import { MatSortModule } from '@angular/material/sort';
import { MatMenuModule } from '@angular/material/menu';
import { MatIconModule } from '@angular/material/icon';
import { MatFormFieldModule } from '@angular/material/form-field';
import { FormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatInputModule } from '@angular/material/input';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatSelectModule } from '@angular/material/select';
import { MatTooltipModule } from '@angular/material/tooltip';
import { TranslocoModule } from '@ngneat/transloco';
import { mensagemRoutes } from './mensagem.routing';
import { MensagemTecnicoListaComponent } from './tecnico/mensagem-tecnico-lista/mensagem-tecnico-lista.component';
import { MensagemTecnicoFormComponent } from './tecnico/mensagem-tecnico-form/mensagem-tecnico-form.component';
import { MensagemTecnicoFiltroComponent } from './tecnico/mensagem-tecnico-filtro/mensagem-tecnico-filtro.component';
import { MatSidenavModule } from '@angular/material/sidenav';
import { FuseHighlightModule } from '@fuse/components/highlight';
import { NgxMatSelectSearchModule } from 'ngx-mat-select-search';
import { FiltroModule } from '../filtros/filtro.module';

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
    MatMenuModule
  ]
})
export class MensagemModule { }

import { PlanilhaModule } from './../planilha/planilha.module';
import { MatButtonModule } from '@angular/material/button';
import { SharedModule } from './../../../shared/shared.module';
import { ImportacaoComponent } from './importacao.component';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { ImportacaoRoutes } from './importacao.routing';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatMomentDateModule } from '@angular/material-moment-adapter';
import { MatBadgeModule } from '@angular/material/badge';
import { MatButtonToggleModule } from '@angular/material/button-toggle';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatChipsModule } from '@angular/material/chips';
import { MatRippleModule } from '@angular/material/core';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatDialogModule } from '@angular/material/dialog';
import { MatDividerModule } from '@angular/material/divider';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatListModule } from '@angular/material/list';
import { MatMenuModule } from '@angular/material/menu';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { MatSelectModule } from '@angular/material/select';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { MatSortModule } from '@angular/material/sort';
import { MatStepperModule } from '@angular/material/stepper';
import { MatTableModule } from '@angular/material/table';
import { MatTabsModule } from '@angular/material/tabs';
import { MatTooltipModule } from '@angular/material/tooltip';
import { FuseAlertModule } from '@fuse/components/alert';
import { FuseCardModule } from '@fuse/components/card';
import { FuseHighlightModule } from '@fuse/components/highlight';
import { TranslocoModule } from '@ngneat/transloco';
import { NgxMatSelectSearchModule } from 'ngx-mat-select-search';
import { ImportacaoArquivoAdendoComponent } from './importacao-arquivo-adendo/importacao-arquivo-adendo.component';

@NgModule({
  declarations: [
    ImportacaoComponent,
    ImportacaoArquivoAdendoComponent
  ],
  imports: [
    PlanilhaModule,
    RouterModule.forChild(ImportacaoRoutes),
    MatButtonToggleModule,
    MatChipsModule,
    MatDatepickerModule,
    MatDividerModule,
    MatMomentDateModule,
    FuseHighlightModule,
    MatButtonModule,
    MatCheckboxModule,
    MatFormFieldModule,
    MatIconModule,
    MatInputModule,
    MatMenuModule,
    MatPaginatorModule,
    MatProgressBarModule,
    MatDialogModule,
    MatRippleModule,
    MatSortModule,
    MatSelectModule,
    MatSlideToggleModule,
    SharedModule,
    MatTableModule,
    MatTabsModule,
    TranslocoModule,
    NgxMatSelectSearchModule,
    FuseCardModule,
    MatBadgeModule,
    MatTooltipModule,
    MatSidenavModule,
    MatListModule,
    MatStepperModule,
    FuseAlertModule,
    MatProgressSpinnerModule
  ],
  bootstrap: []
})
export class ImportacaoModule { }

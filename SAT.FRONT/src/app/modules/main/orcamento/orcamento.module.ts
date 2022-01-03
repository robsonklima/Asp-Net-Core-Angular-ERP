import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { OrcamentoListaComponent } from './orcamento-lista/orcamento-lista.component';
import { OrcamentoDetalheComponent } from './orcamento-detalhe/orcamento-detalhe.component';
import { orcamentoRoutes } from './orcamento.routing';
import { RouterModule } from '@angular/router';
import { SharedModule } from 'app/shared/shared.module';
import { MatButtonModule } from '@angular/material/button';
import { MatButtonToggleModule } from '@angular/material/button-toggle';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatDialogModule } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatMenuModule } from '@angular/material/menu';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatSortModule } from '@angular/material/sort';
import { MatTableModule } from '@angular/material/table';
import { MatTabsModule } from '@angular/material/tabs';
import { MatTooltipModule } from '@angular/material/tooltip';
import { FuseCardModule } from '@fuse/components/card';
import { TranslocoModule } from '@ngneat/transloco';
import { OrcamentoDetalheLocalComponent } from './orcamento-detalhe/orcamento-detalhe-local/orcamento-detalhe-local.component';

@NgModule({
  declarations: [
    OrcamentoListaComponent,
    OrcamentoDetalheComponent,
    OrcamentoDetalheLocalComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(orcamentoRoutes),
    SharedModule,
    MatButtonToggleModule,
    MatButtonModule,
    MatCheckboxModule,
    MatFormFieldModule,
    MatIconModule,
    MatInputModule,
    MatMenuModule,
    MatProgressBarModule,
    MatDialogModule,
    MatSortModule,
    MatTableModule,
    MatTabsModule,
    TranslocoModule,
    FuseCardModule,
    MatTooltipModule,
    MatSidenavModule,
    MatProgressSpinnerModule
  ]
})
export class OrcamentoModule { }

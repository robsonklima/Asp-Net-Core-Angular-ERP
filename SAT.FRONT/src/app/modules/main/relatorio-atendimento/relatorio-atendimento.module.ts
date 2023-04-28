import { NgModule } from '@angular/core';
import { relatorioAtendimentoRoutes } from './relatorio-atendimento.routing';
import { RelatorioAtendimentoFormComponent } from './relatorio-atendimento-form/relatorio-atendimento-form.component';
import { NgxMatSelectSearchModule } from 'ngx-mat-select-search';
import { RouterModule } from '@angular/router';
import { MatIconModule } from '@angular/material/icon';
import { TranslocoModule } from '@ngneat/transloco';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatSelectModule } from '@angular/material/select';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatRadioModule } from '@angular/material/radio';
import { MatStepperModule } from '@angular/material/stepper';
import { MatInputModule } from '@angular/material/input';
import { SharedModule } from 'app/shared/shared.module';
import { MatButtonModule } from '@angular/material/button';
import { NgxMaskModule, IConfig } from 'ngx-mask'
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatListModule } from '@angular/material/list';
import { MatDialogModule } from '@angular/material/dialog';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { RelatorioAtendimentoDetalheFormComponent } from './relatorio-atendimento-detalhe-form/relatorio-atendimento-detalhe-form.component';
import { FuseCardModule } from '@fuse/components/card';
import { RelatorioAtendimentoDetalhePecaFormComponent } from './relatorio-atendimento-detalhe-peca-form/relatorio-atendimento-detalhe-peca-form.component';
import { RelatorioAtendimentoLaudoImpressaoComponent } from './relatorio-atendimento-laudo-impressao/relatorio-atendimento-laudo-impressao.component';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatSortModule } from '@angular/material/sort';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { RelatorioAtendimentoPosFormComponent } from './relatorio-atendimento-pos-form/relatorio-atendimento-pos-form.component';

const maskConfigFunction: () => Partial<IConfig> = () => {
  return {
      validation: false,
  };
};

@NgModule({
  declarations: [
    RelatorioAtendimentoFormComponent,
    RelatorioAtendimentoDetalheFormComponent,
    RelatorioAtendimentoDetalhePecaFormComponent,
    RelatorioAtendimentoLaudoImpressaoComponent,
    RelatorioAtendimentoPosFormComponent,
  ],
  imports: [
    RouterModule.forChild(relatorioAtendimentoRoutes),
    NgxMaskModule.forRoot(maskConfigFunction),
    MatIconModule,
    TranslocoModule,
    MatDatepickerModule,
    NgxMatSelectSearchModule,
    MatSelectModule,
    MatFormFieldModule,
    MatInputModule,
    SharedModule,
    MatButtonModule,
    MatStepperModule,
    MatPaginatorModule,
    MatCheckboxModule,
    MatSortModule,
    MatRadioModule,
    MatSidenavModule,
    MatListModule,
    MatDialogModule,
    MatTooltipModule,
    MatProgressBarModule,
    MatProgressSpinnerModule,
    MatSlideToggleModule,
    FuseCardModule
  ]
})
export class RelatorioAtendimentoModule { }

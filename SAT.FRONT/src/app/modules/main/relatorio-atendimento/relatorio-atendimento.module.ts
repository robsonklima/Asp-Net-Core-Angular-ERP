import { NgModule } from '@angular/core';
import { relatorioAtendimentoRoutes } from './relatorio-atendimento.routing';
import { RelatorioAtendimentoFormComponent } from './relatorio-atendimento-form/relatorio-atendimento-form.component';
import { NgxMatSelectSearchModule } from 'ngx-mat-select-search';
import { MatSelectInfiniteScrollModule } from 'ng-mat-select-infinite-scroll';
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

const maskConfigFunction: () => Partial<IConfig> = () => {
  return {
      validation: false,
  };
};

@NgModule({
  declarations: [
    RelatorioAtendimentoFormComponent
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
    MatSelectInfiniteScrollModule,
    MatButtonModule,
    MatStepperModule,
    MatCheckboxModule,
    MatRadioModule
  ]
})
export class RelatorioAtendimentoModule { }

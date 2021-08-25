import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LocalAtendimentoListaComponent } from './local-atendimento-lista/local-atendimento-lista.component';
import { LocalAtendimentoFormComponent } from './local-atendimento-form/local-atendimento-form.component';
import { RouterModule } from '@angular/router';
import { localAtendimentoRoutes } from './local-atendimento.routing';
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
import { MatMenuModule } from '@angular/material/menu';

const maskConfigFunction: () => Partial<IConfig> = () => {
  return {
      validation: false,
  };
};

@NgModule({
  declarations: [
    LocalAtendimentoListaComponent,
    LocalAtendimentoFormComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(localAtendimentoRoutes),
    NgxMaskModule.forRoot(maskConfigFunction),
    MatPaginatorModule,
    MatIconModule,
    MatFormFieldModule,
    MatButtonModule,
    TranslocoModule,
    SharedModule,
    FuseHighlightModule,
    NgxMatSelectSearchModule,
    MatInputModule,
    MatSortModule,
    MatProgressBarModule,
    MatSnackBarModule,
    MatSelectModule,
    MatProgressSpinnerModule,
    NgxMatSelectSearchModule,
    MatCheckboxModule,
    MatMenuModule
  ]
})
export class LocalAtendimentoModule { }

import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatIconModule } from '@angular/material/icon';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatButtonModule } from '@angular/material/button';
import { TranslocoModule } from '@ngneat/transloco';
import { MatInputModule } from '@angular/material/input';
import { NgxMatSelectSearchModule } from 'ngx-mat-select-search';
import { MatSortModule } from '@angular/material/sort';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { autorizadaRoutes } from './autorizada.routing'
import { AutorizadaListaComponent } from './autorizada-lista/autorizada-lista.component';
import { AutorizadaFormComponent } from './autorizada-form/autorizada-form.component';
import { MatSelectModule } from '@angular/material/select';
import { SharedModule } from 'app/shared/shared.module';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { IConfig, NgxMaskModule } from 'ngx-mask';
import { MatCheckboxModule } from '@angular/material/checkbox';

const maskConfigFunction: () => Partial<IConfig> = () => {
  return {
      validation: false,
  };
};

@NgModule({
  declarations: [
    AutorizadaListaComponent,
    AutorizadaFormComponent      
  ],
  imports: [
    SharedModule,
    CommonModule,
    RouterModule.forChild(autorizadaRoutes),
    NgxMaskModule.forRoot(maskConfigFunction),
    MatPaginatorModule,
    MatIconModule,
    MatFormFieldModule,
    MatButtonModule,
    TranslocoModule,
    MatSortModule,
    MatInputModule,
    NgxMatSelectSearchModule,
    MatProgressBarModule,
    MatSelectModule,
    MatProgressSpinnerModule,
    MatCheckboxModule
  ]
})
export class AutorizadaModule { }

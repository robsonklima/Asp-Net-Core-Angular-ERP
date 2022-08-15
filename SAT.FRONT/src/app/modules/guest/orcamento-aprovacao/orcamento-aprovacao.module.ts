import { NgModule } from '@angular/core';
import { OrcamentoAprovacaoComponent } from './orcamento-aprovacao.component';
import { orcamentoAprovacaoRoutes } from './orcamento-aprovacao.routing';
import { RouterModule } from '@angular/router';
import { SharedModule } from 'app/shared/shared.module';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { MatButtonModule } from '@angular/material/button';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { FuseCardModule } from '@fuse/components/card';
import { FuseAlertModule } from '@fuse/components/alert';
import { IConfig, NgxMaskModule } from 'ngx-mask';

const maskConfigFunction: () => Partial<IConfig> = () => {
  return {
      validation: false,
  };
};

@NgModule({
  declarations: [
    OrcamentoAprovacaoComponent
  ],
  imports: [
    RouterModule.forChild(orcamentoAprovacaoRoutes),
    NgxMaskModule.forRoot(maskConfigFunction),
    MatButtonModule,
    MatCheckboxModule,
    MatFormFieldModule,
    MatIconModule,
    MatInputModule,
    MatProgressSpinnerModule,
    MatSlideToggleModule,
    FuseCardModule,
    FuseAlertModule,
    SharedModule
  ]
})
export class OrcamentoAprovacaoModule { }

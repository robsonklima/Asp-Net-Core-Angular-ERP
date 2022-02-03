import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClientModule, HttpClientJsonpModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatOptionModule } from '@angular/material/core';
import { MatDialogModule } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatSelectModule } from '@angular/material/select';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MbscModule } from '@mobiscroll/angular';
import { MatExpansionModule } from '@angular/material/expansion';
import { TranslocoModule } from '@ngneat/transloco';
import { SharedModule } from 'app/shared/shared.module';
import { RouterModule } from '@angular/router';
import { docsRoutes } from './docs.routing';
import { SuporteComponent } from './suporte/suporte.component';
import { FuseAlertModule } from '@fuse/components/alert';
import { DocsComponent } from './docs.component';
import { FuseNavigationModule } from '@fuse/components/navigation';
import { LoginComponent } from './autenticacao/login/login.component';
import { IntroducaoComponent } from './inicio/introducao/introducao.component';
import { ListagemComponent } from './ordem-servico/listagem/listagem.component';
import { FiltragemComponent } from './ordem-servico/filtragem/filtragem.component';
import { ExportacaoComponent } from './ordem-servico/exportacao/exportacao.component';
import { NovoComponent } from './ordem-servico/novo/novo.component';
import { DuasEtapasComponent } from './autenticacao/duas-etapas/duas-etapas.component';
import { VersoesComponent } from './versoes/versoes.component';

@NgModule({
  declarations: [
    SuporteComponent,
    DocsComponent,
    IntroducaoComponent,
    LoginComponent,
    ListagemComponent,
    FiltragemComponent,
    ExportacaoComponent,
    NovoComponent,
    DuasEtapasComponent,
    VersoesComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(docsRoutes),
    SharedModule,
    MbscModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    HttpClientJsonpModule,
    TranslocoModule,
    MatSidenavModule,
    MatSelectModule,
    MatIconModule,
    MatTooltipModule,
    MatButtonModule,
    MatProgressSpinnerModule,
    MatFormFieldModule,
    MatInputModule,
    MatDialogModule,
    MatOptionModule,
    MatIconModule,
    FuseAlertModule,
    MatExpansionModule,
    FuseNavigationModule,
  ]
})
export class DocsModule { }

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
import { FuseAlertModule } from '@fuse/components/alert';
import { DocsComponent } from './docs.component';
import { FuseNavigationModule } from '@fuse/components/navigation';
import { IntroducaoComponent } from './inicio/introducao/introducao.component';
import { VersoesComponent } from './inicio/introducao/versoes/versoes.component';
import { OrdemServicoComponent } from './ordem-servico/ordem-servico.component';
import { AutenticacaoComponent } from './autenticacao/autenticacao.component';
import { AppTecnicosComponent } from './app-tecnicos/app-tecnicos.component';
import { BuildComponent } from './build/build.component';
import { EscopoComponent } from './documentacao/escopo/escopo.component';
import { TempoComponent } from './documentacao/tempo/tempo.component';
import { TermoAberturaComponent } from './documentacao/termo-abertura/termo-abertura.component';
import { CustoComponent } from './documentacao/custo/custo.component';
import { DocsIntroComponent } from './documentacao/docs-intro/docs-intro.component';
import { QualidadeComponent } from './documentacao/qualidade/qualidade.component';
import { RecursosHumanosComponent } from './documentacao/recursos-humanos/recursos-humanos.component';
import { ComunicacoesComponent } from './documentacao/comunicacoes/comunicacoes.component';
import { RiscosComponent } from './documentacao/riscos/riscos.component';

@NgModule({
  declarations: [
    DocsComponent,
    IntroducaoComponent,
    VersoesComponent,
    OrdemServicoComponent,
    AutenticacaoComponent,
    AppTecnicosComponent,
    BuildComponent,
    EscopoComponent,
    TempoComponent,
    TermoAberturaComponent,
    CustoComponent,
    DocsIntroComponent,
    QualidadeComponent,
    RecursosHumanosComponent,
    ComunicacoesComponent,
    RiscosComponent
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

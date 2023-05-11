import { NgModule } from '@angular/core';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { instalacaoRoutes } from './instalacao.routing';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatIconModule } from '@angular/material/icon';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatSortModule } from '@angular/material/sort';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { SharedModule } from 'app/shared/shared.module';
import { MatMenuModule } from '@angular/material/menu';
import { MatInputModule } from '@angular/material/input';
import { TranslocoModule } from '@ngneat/transloco';
import { MatButtonModule } from '@angular/material/button';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatOptionModule } from '@angular/material/core';
import { MatSelectModule } from '@angular/material/select';
import { MatTableModule } from '@angular/material/table';
import { InstalacaoContratoListaComponent } from './instalacao-contrato-lista/instalacao-contrato-lista.component';
import { InstalacaoLoteListaComponent } from './instalacao-lote-lista/instalacao-lote-lista.component';
import { InstalacaoLoteFormComponent } from './instalacao-lote-form/instalacao-lote-form.component';
import { InstalacaoListaComponent } from './instalacao-lista/instalacao-lista.component';
import { NgxMatSelectSearchModule } from 'ngx-mat-select-search';
import { InstalacaoListaMaisOpcoesComponent } from './instalacao-lista/instalacao-lista-mais-opcoes/instalacao-lista-mais-opcoes.component';
import { MatTabsModule } from '@angular/material/tabs';
import { InstalacaoRessalvaDialogComponent } from './instalacao-ressalva-dialog/instalacao-ressalva-dialog.component';
import { FuseCardModule } from '@fuse/components/card';
import { FuseAlertModule } from '@fuse/components/alert';
import { FuseHighlightModule } from '@fuse/components/highlight';
import { IConfig, NgxMaskModule } from 'ngx-mask';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { InstalacaoFiltroComponent } from './instalacao-filtro/instalacao-filtro.component';
import { FiltroModule } from '../filtros/filtro.module';
import { InstalacaoContratoFiltroComponent } from './instalacao-contrato-filtro/instalacao-contrato-filtro.component';
import { MatSidenavModule } from '@angular/material/sidenav';
import { InstalacaoPleitoListaComponent } from './instalacao-pleito-lista/instalacao-pleito-lista.component';
import { InstalacaoPleitoFiltroComponent } from './instalacao-pleito-filtro/instalacao-pleito-filtro.component';
import { InstalacaoLoteFiltroComponent } from './instalacao-lote-filtro/instalacao-lote-filtro.component';
import { MatChipsModule } from '@angular/material/chips';
import { InstalacaoAnexoDialogComponent } from './instalacao-anexo-dialog/instalacao-anexo-dialog.component';
import { InstalacaoLoteAnexoDialogComponent } from './instalacao-lote-anexo-dialog/instalacao-lote-anexo-dialog.component';
import { InstalacaoPleitoDetalheComponent } from './instalacao-pleito-detalhe/instalacao-pleito-detalhe.component';
import { InstalacaoPleitoFormComponent } from './instalacao-pleito-detalhe/instalacao-pleito-form/instalacao-pleito-form.component';
import { InstalacaoPleitoInstalacaoListaComponent } from './instalacao-pleito-detalhe/instalacao-pleito-instalacao-lista/instalacao-pleito-instalacao-lista.component';
import { InstalacaoPleitoInstalacaoDialogComponent } from './instalacao-pleito-instalacao-dialog/instalacao-pleito-instalacao-dialog.component';
import { InstalacaoPagtoListaComponent } from './instalacao-pagto-lista/instalacao-pagto-lista.component';
import { InstalacaoPagtoDetalheComponent } from './instalacao-pagto-detalhe/instalacao-pagto-detalhe.component';
import { InstalacaoPagtoFormComponent } from './instalacao-pagto-detalhe/instalacao-pagto-form/instalacao-pagto-form.component';
import { InstalacaoPagtoInstalacaoListaComponent } from './instalacao-pagto-detalhe/instalacao-pagto-instalacao-lista/instalacao-pagto-instalacao-lista.component';
import { InstalacaoPagtoFiltroComponent } from './instalacao-pagto-filtro/instalacao-pagto-filtro.component';
import { InstalacaoPagtoInstalacaoFormComponent } from './instalacao-pagto-detalhe/instalacao-pagto-instalacao-form/instalacao-pagto-instalacao-form.component';
import { InstalacaoPleitoDetalheFiltroComponent } from './instalacao-pleito-detalhe/instalacao-pleito-detalhe-filtro/instalacao-pleito-detalhe-filtro.component';

const maskConfigFunction: () => Partial<IConfig> = () => { return { validation: false } };

@NgModule({
  declarations: [
    InstalacaoContratoListaComponent,
    InstalacaoLoteListaComponent,
    InstalacaoLoteFormComponent,
    InstalacaoListaComponent,
    InstalacaoListaMaisOpcoesComponent,
    InstalacaoRessalvaDialogComponent,
    InstalacaoFiltroComponent,
    InstalacaoContratoFiltroComponent,
    InstalacaoPleitoListaComponent,
    InstalacaoPleitoFiltroComponent,
    InstalacaoLoteFiltroComponent,
    InstalacaoAnexoDialogComponent,
    InstalacaoLoteAnexoDialogComponent,
    InstalacaoPleitoDetalheComponent,
    InstalacaoPleitoFormComponent,
    InstalacaoPleitoInstalacaoListaComponent,
    InstalacaoPleitoInstalacaoDialogComponent,
    InstalacaoPagtoListaComponent,
    InstalacaoPagtoDetalheComponent,
    InstalacaoPagtoFormComponent,
    InstalacaoPagtoInstalacaoListaComponent,
    InstalacaoPagtoFiltroComponent,
    InstalacaoPagtoInstalacaoFormComponent,
    InstalacaoPleitoDetalheFiltroComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(instalacaoRoutes),
    NgxMaskModule.forRoot(maskConfigFunction),
    MatPaginatorModule,
    SharedModule,
    MatIconModule,
    MatSortModule,
    MatMenuModule,
    MatFormFieldModule,
    MatInputModule,
    MatProgressBarModule,
    TranslocoModule,
    MatButtonModule,
    MatTooltipModule,
    MatProgressSpinnerModule,
    MatDatepickerModule,
    MatOptionModule,
    NgxMatSelectSearchModule,
    MatSelectModule,
    MatTableModule,
    MatSlideToggleModule,
    MatTabsModule,
    FuseCardModule,
    FuseAlertModule,
    FuseHighlightModule,
    MatCheckboxModule,
    FiltroModule,
    MatSidenavModule,
    MatChipsModule
  ]
})
export class InstalacaoModule { }
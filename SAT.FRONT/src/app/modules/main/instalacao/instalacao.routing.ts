import { Route } from '@angular/router';
import { InstalacaoContratoListaComponent } from './instalacao-contrato-lista/instalacao-contrato-lista.component';
import { InstalacaoListaComponent } from './instalacao-lista/instalacao-lista.component';
import { InstalacaoLoteFormComponent } from './instalacao-lote-form/instalacao-lote-form.component';
import { InstalacaoLoteListaComponent } from './instalacao-lote-lista/instalacao-lote-lista.component';
import { InstalacaoPagtoDetalheComponent } from './instalacao-pagto-detalhe/instalacao-pagto-detalhe.component';
import { InstalacaoPagtoInstalacaoFormComponent } from './instalacao-pagto-detalhe/instalacao-pagto-instalacao-form/instalacao-pagto-instalacao-form.component';
import { InstalacaoPagtoListaComponent } from './instalacao-pagto-lista/instalacao-pagto-lista.component';
import { InstalacaoPleitoDetalheComponent } from './instalacao-pleito-detalhe/instalacao-pleito-detalhe.component';
import { InstalacaoPleitoListaComponent } from './instalacao-pleito-lista/instalacao-pleito-lista.component';

export const instalacaoRoutes: Route[] = [
    {
        path: '',
        pathMatch: 'full',
        redirectTo: 'contrato'
    },
    {
        path: 'contrato',
        component: InstalacaoContratoListaComponent
    },
    {
        path: 'lista/:codContrato',
        component: InstalacaoListaComponent
    },
    {
        path: 'lote/:codContrato',
        component: InstalacaoLoteListaComponent
    },
    {
        path: 'lote/form/:codContrato',
        component: InstalacaoLoteFormComponent
    },
    {
        path: 'pleito',
        component: InstalacaoPleitoListaComponent
    },
    {
        path: 'pleito/detalhe',
        component: InstalacaoPleitoDetalheComponent
    },
    {
        path: 'pleito/detalhe/:codInstalPleito',
        component: InstalacaoPleitoDetalheComponent
    },
    {
        path: 'lote/lista/:codContrato/:codInstalLote',
        component: InstalacaoListaComponent
    },
    {
        path: 'pagto',
        component: InstalacaoPagtoListaComponent
    },
    {
        path: 'pagto/detalhe',
        component: InstalacaoPagtoDetalheComponent
    },
    {
        path: 'pagto/detalhe/:codInstalPagto',
        component: InstalacaoPagtoDetalheComponent
    },    
    {
        path: 'pagto/instalacao/form',
        component: InstalacaoPagtoInstalacaoFormComponent
    },  
];

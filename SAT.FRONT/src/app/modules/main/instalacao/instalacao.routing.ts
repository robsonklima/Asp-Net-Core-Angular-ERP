import { Route } from '@angular/router';
import { InstalacaoContratoListaComponent } from './instalacao-contrato-lista/instalacao-contrato-lista.component';
import { InstalacaoListaMaisOpcoesComponent } from './instalacao-lista/instalacao-lista-mais-opcoes/instalacao-lista-mais-opcoes.component';
import { InstalacaoListaComponent } from './instalacao-lista/instalacao-lista.component';
import { InstalacaoLoteFormComponent } from './instalacao-lote-form/instalacao-lote-form.component';
import { InstalacaoLoteListaComponent } from './instalacao-lote-lista/instalacao-lote-lista.component';

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
        path: 'lote-form/:codContrato',
        component: InstalacaoLoteFormComponent
    },
    {
        path: 'lote/:codContrato',
        component: InstalacaoLoteListaComponent
    },
    {
        path: ':codContrato',
        component: InstalacaoListaComponent
    },
    {
        path: ':codContrato/:codInstalLote',
        component: InstalacaoListaComponent
    }
    ,
    {
        path: ':codContrato/:codInstalacao',
        component: InstalacaoListaMaisOpcoesComponent
    }    
];

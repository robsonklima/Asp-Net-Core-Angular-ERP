import { Route } from '@angular/router';
import { InstalacaoContratoListaComponent } from './instalacao-contrato-lista/instalacao-contrato-lista.component';
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
        path: 'lote/:codContrato',
        component: InstalacaoLoteListaComponent
    }
];

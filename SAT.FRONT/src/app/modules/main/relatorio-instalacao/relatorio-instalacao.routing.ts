import { RelatorioInstalacaoComponent } from './relatorio-instalacao.component';
import { Route } from '@angular/router';

export const relatorioInstalacaoRoutes: Route[] = [
    {
        path: '',
        pathMatch: 'full',
    },
    {
        path: ':codOS',
        component: RelatorioInstalacaoComponent
    },
];

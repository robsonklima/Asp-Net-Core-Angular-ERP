import { Route } from '@angular/router';
import { RelatorioAtendimentoDetalheComponent } from './relatorio-atendimento-detalhe/relatorio-atendimento-detalhe.component';

export const relatorioAtendimentoRoutes: Route[] = [
    {
        path: '',
        pathMatch: 'full',
    },
    {
        path: 'detalhe/:codOS',
        component: RelatorioAtendimentoDetalheComponent
    },
    {
        path: 'detalhe/:codOS/:codRAT',
        component: RelatorioAtendimentoDetalheComponent
    },
    {
        path: 'detalhe/:codOS/:codRAT/impressao-laudo/:codLaudo',
        component: RelatorioAtendimentoDetalheComponent
    }
];

import { Route } from '@angular/router';
import { RelatorioAtendimentoFormComponent } from './relatorio-atendimento-form/relatorio-atendimento-form.component';
import { RelatorioAtendimentoLaudoImpressaoComponent } from './relatorio-atendimento-laudo-impressao/relatorio-atendimento-laudo-impressao.component';

export const relatorioAtendimentoRoutes: Route[] = [
    {
        path: '',
        pathMatch: 'full',
    },
    {
        path: 'form/:codOS',
        component: RelatorioAtendimentoFormComponent
    },
    {
        path: 'form/:codOS/:codRAT',
        component: RelatorioAtendimentoFormComponent
    },
    {
        path: 'form/:codOS/:codRAT/:codLaudo',
        component: RelatorioAtendimentoLaudoImpressaoComponent
    }
];

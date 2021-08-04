import { Route } from '@angular/router';
import { RelatorioAtendimentoFormComponent } from './relatorio-atendimento-form/relatorio-atendimento-form.component';

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
    }
];

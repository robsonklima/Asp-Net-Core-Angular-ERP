import { Route } from '@angular/router';
import { OrcamentoDetalheComponent } from './orcamento-detalhe/orcamento-detalhe.component';
import { OrcamentoListaComponent } from './orcamento-lista/orcamento-lista.component';

export const orcamentoRoutes: Route[] = [
    {
        path: '',
        pathMatch: 'full',
        redirectTo: 'lista'
    },
    {
        path: 'lista',
        component: OrcamentoListaComponent
    },
    {
        path: 'detalhe/:codOrc',
        component: OrcamentoDetalheComponent
    }
];

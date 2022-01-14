import { Route } from '@angular/router';
import { OrcamentoDetalheComponent } from './orcamento-detalhe/orcamento-detalhe.component';
import { OrcamentoImpressaoComponent } from './orcamento-impressao/orcamento-impressao.component';
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
    },
    {
        path: 'impressao/:codOrc',
        component: OrcamentoImpressaoComponent
    },
];

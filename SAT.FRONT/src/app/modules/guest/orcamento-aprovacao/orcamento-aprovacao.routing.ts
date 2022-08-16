import { Route } from '@angular/router';
import { OrcamentoAprovacaoComponent } from './orcamento-aprovacao.component';

export const orcamentoAprovacaoRoutes: Route[] = [
    {
        path     : ':codOrc',
        component: OrcamentoAprovacaoComponent
    }
];

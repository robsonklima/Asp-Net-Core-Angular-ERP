import { Route } from '@angular/router';
import { FormasPagamentoFormComponent } from './formas-pagamento-form/formas-pagamento-form.component';
import { FormasPagamentoListaComponent } from './formas-pagamento-lista/formas-pagamento-lista.component';

export const formasPagamentoRoutes: Route[] = [
    {
        path: '',
        pathMatch: 'full',
        redirectTo: 'lista'
    },
    {
        path: 'lista',
        component: FormasPagamentoListaComponent,
    },
    {
        path: 'form',
        component: FormasPagamentoFormComponent,
    },
    {
        path: 'form/:codFormaPagto',
        component: FormasPagamentoFormComponent,
    },
];

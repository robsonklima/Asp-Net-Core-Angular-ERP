import { Route } from '@angular/router';
import { SuporteSTNDetalheComponent } from './suporte-stn-detalhe/suporte-stn-detalhe.component';
import { SuporteSTNListaComponent } from './suporte-stn-lista/suporte-stn-lista.component';

export const suporteSTNRoutes: Route[] = [
    {
        path: '',
        pathMatch: 'full',
        redirectTo: 'lista'
    },
    {
        path: 'lista',
        component: SuporteSTNListaComponent
    },
    {
        path: 'detalhe/:codAtendimento',
        component: SuporteSTNDetalheComponent
    }
];

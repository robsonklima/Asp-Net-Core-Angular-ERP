import { Route } from '@angular/router';
import { OrdemServicoSTNDetalheComponent } from './ordem-servico-stn-detalhe/ordem-servico-stn-detalhe.component';
import { OrdemServicoSTNListaComponent } from './ordem-servico-stn-lista/ordem-servico-stn-lista.component';

export const suporteSTNRoutes: Route[] = [
    {
        path: '',
        pathMatch: 'full',
        redirectTo: 'lista'
    },
    {
        path: 'lista',
        component: OrdemServicoSTNListaComponent
    },
    {
        path: 'detalhe/:codAtendimento',
        component: OrdemServicoSTNDetalheComponent
    }
];

import { Route } from '@angular/router';
import { OrdemServicoDetalheComponent } from './ordem-servico-detalhe/ordem-servico-detalhe.component';
import { OrdemServicoFormComponent } from './ordem-servico-form/ordem-servico-form.component';
import { OrdemServicoImpressaoComponent } from './ordem-servico-impressao/ordem-servico-impressao.component';
import { OrdemServicoListaComponent } from './ordem-servico-lista/ordem-servico-lista.component';

export const ordemServicoRoutes: Route[] = [
    {
        path: '',
        pathMatch: 'full',
        redirectTo: 'lista'
    },
    {
        path: 'lista',
        component: OrdemServicoListaComponent
    },
    {
        path: 'form',
        component: OrdemServicoFormComponent
    },
    {
        path: 'form/:codOS',
        component: OrdemServicoFormComponent
    },
    {
        path: 'detalhe/:codOS',
        component: OrdemServicoDetalheComponent
    },
    {
        path: 'impressao/:codOS',
        component: OrdemServicoImpressaoComponent
    }
];

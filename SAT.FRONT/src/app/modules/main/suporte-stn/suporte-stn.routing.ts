import { Route } from '@angular/router';
import { OrdemServicoStnFormComponent } from './ordem-servico-stn-form/ordem-servico-stn-form.component';
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
        path: 'form',
        component: OrdemServicoStnFormComponent
    }
    ,
    {
        path: 'form/:codAtendimento',
        component: OrdemServicoStnFormComponent
    }
];

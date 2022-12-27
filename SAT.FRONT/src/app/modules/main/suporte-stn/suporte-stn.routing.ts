import { Route } from '@angular/router';
import { OrdemServicoStnFormComponent } from './ordem-servico-stn-form/ordem-servico-stn-form.component';
import { OrdemServicoSTNListaComponent } from './ordem-servico-stn-lista/ordem-servico-stn-lista.component';
import { SuporteStnBloquearOSComponent } from './suporte-stn-bloquear-os/suporte-stn-bloquear-os.component';
import { SuporteStnLaudoFormComponent } from './suporte-stn-laudo/suporte-stn-laudo-form/suporte-stn-laudo-form.component';
import { SuporteStnLaudoListaComponent } from './suporte-stn-laudo/suporte-stn-laudo-lista/suporte-stn-laudo-lista.component';

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
        path: 'laudo/form/:codLaudo',
        component: SuporteStnLaudoFormComponent
    }
    ,
    {
        path: 'laudo',
        component: SuporteStnLaudoListaComponent
    }
    ,
    {
        path: 'form/:codAtendimento',
        component: OrdemServicoStnFormComponent
    }
    ,
    {
        path: 'bloquear-os',
        component: SuporteStnBloquearOSComponent
    }
];

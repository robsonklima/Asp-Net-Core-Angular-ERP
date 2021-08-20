import { Route } from '@angular/router';
import { LocalAtendimentoFormComponent } from './local-atendimento-form/local-atendimento-form.component';
import { LocalAtendimentoListaComponent } from './local-atendimento-lista/local-atendimento-lista.component';


export const localAtendimentoRoutes: Route[] = [
    {
        path: '',
        pathMatch: 'full',
        redirectTo: 'lista'
    },
    {
        path: 'lista',
        component: LocalAtendimentoListaComponent,
    },
    {
        path: 'form',
        component: LocalAtendimentoFormComponent,
    },
    {
        path: 'form/:codPosto',
        component: LocalAtendimentoFormComponent,
    },
];

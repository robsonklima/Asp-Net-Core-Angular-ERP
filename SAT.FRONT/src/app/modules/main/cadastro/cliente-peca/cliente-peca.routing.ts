import { Route } from '@angular/router';
import { ClientePecaFormComponent } from './cliente-peca-form/cliente-peca-form.component';
import { ClientePecaListaComponent } from './cliente-peca-lista/cliente-peca-lista.component';

export const clientePecaRoutes: Route[] = [
    {
        path: '',
        pathMatch: 'full',
        redirectTo: 'lista'
    },
    {
        path: 'lista',
        component: ClientePecaListaComponent,
    },
    {
        path: 'form',
        component: ClientePecaFormComponent,
    },
    {
        path: 'form/:codClientePeca',
        component: ClientePecaFormComponent,
    },
];

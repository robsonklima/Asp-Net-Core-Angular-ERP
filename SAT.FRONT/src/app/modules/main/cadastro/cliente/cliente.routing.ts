import { Route } from '@angular/router';
import { ClienteFormComponent } from './cliente-form/cliente-form.component';
import { ClienteListaComponent } from './cliente-lista/cliente-lista.component';

export const clienteRoutes: Route[] = [
    {
        path: '',
        pathMatch: 'full',
        redirectTo: 'lista'
    },
    {
        path: 'lista',
        component: ClienteListaComponent,
    },
    {
        path: 'form',
        component: ClienteFormComponent,
    },
    {
        path: 'form/:codCliente',
        component: ClienteFormComponent,
    },
];

import { Route } from '@angular/router';
import { ClientePecaGenericaFormComponent } from './cliente-peca-generica-form/cliente-peca-generica-form.component';
import { ClientePecaGenericaListaComponent } from './cliente-peca-generica-lista/cliente-peca-generica-lista.component';

export const clientePecagenericaRoutes: Route[] = [
    {
        path: '',
        pathMatch: 'full',
        redirectTo: 'lista'
    },
    {
        path: 'lista',
        component: ClientePecaGenericaListaComponent,
    },
    {
        path: 'form',
        component: ClientePecaGenericaFormComponent,
    },
    {
        path: 'form/:codClientePecaGenerica',
        component: ClientePecaGenericaFormComponent,
    },
];

import { Route } from '@angular/router';
import { ClienteBancadaFormComponent } from './cliente-bancada-form/cliente-bancada-form.component';
import { ClienteBancadaListaComponent } from './cliente-bancada-lista/cliente-bancada-lista.component';

export const clienteBancadaRoutes: Route[] = [
    {
        path: '',
        pathMatch: 'full',
        redirectTo: 'lista'
    },
    {
        path: 'lista',
        component: ClienteBancadaListaComponent,
    },
    {
        path: 'form',
        component: ClienteBancadaFormComponent,
    },
    {
        path: 'form/:codClienteBancada',
        component: ClienteBancadaFormComponent,
    },
];

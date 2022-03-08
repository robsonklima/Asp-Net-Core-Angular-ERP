import { Route } from '@angular/router';
import { FilialFormComponent } from './filial-form/filial-form.component';
import { FilialListaComponent } from './filial-lista/filial-lista.component';

export const filialRoutes: Route[] = [
    {
        path: '',
        pathMatch: 'full',
        redirectTo: 'lista'
    },
    {
        path: 'lista',
        component: FilialListaComponent,
    },
    {
        path: 'form',
        component: FilialFormComponent,
    },
    {
        path: 'form/:codFilial',
        component: FilialFormComponent,
    },
];

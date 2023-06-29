import { Route } from '@angular/router';
import { ANSListaComponent } from './ans-lista/ans-lista.component';
import { ANSFormComponent } from './ans-form/ans-form.component';

export const ansRoutes: Route[] = [
    {
        path: '',
        pathMatch: 'full',
        redirectTo: 'lista'
    },
    {
        path: 'lista',
        component: ANSListaComponent,
    },
    {
        path: 'form',
        component: ANSFormComponent,
    },
    {
        path: 'form/:codANS',
        component: ANSFormComponent,
    },
];

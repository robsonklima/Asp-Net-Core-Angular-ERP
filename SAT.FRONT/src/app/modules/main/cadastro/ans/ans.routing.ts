import { Route } from '@angular/router';
import { AnsListaComponent } from './ans-lista/ans-lista.component';
import { AnsFormComponent } from './ans-form/ans-form.component';

export const ansRoutes: Route[] = [
    {
        path: '',
        pathMatch: 'full',
        redirectTo: 'lista'
    },
    {
        path: 'lista',
        component: AnsListaComponent,
    },
    {
        path: 'form',
        component: AnsFormComponent,
    },
    {
        path: 'form/:codANS',
        component: AnsFormComponent,
    },
];

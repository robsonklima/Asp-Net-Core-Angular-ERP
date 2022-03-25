import { Route } from '@angular/router';
import { SLAFormComponent } from './sla-form/sla-form.component';
import { SLAListaComponent } from './sla-lista/sla-lista.component';

export const filialRoutes: Route[] = [
    {
        path: '',
        pathMatch: 'full',
        redirectTo: 'lista'
    },
    {
        path: 'lista',
        component: SLAListaComponent,
    },
    {
        path: 'form',
        component: SLAFormComponent,
    },
    {
        path: 'form/:codSLA',
        component: SLAFormComponent,
    },
];

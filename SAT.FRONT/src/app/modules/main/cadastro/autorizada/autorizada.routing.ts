import { Route } from '@angular/router';
import { AutorizadaFormComponent } from './autorizada-form/autorizada-form.component';
import { AutorizadaListaComponent } from './autorizada-lista/autorizada-lista.component';

export const autorizadaRoutes: Route[] = [
    {
        path: '',
        pathMatch: 'full',
        redirectTo: 'lista'
    },
    {
        path: 'lista',
        component: AutorizadaListaComponent,
    },
    {
        path: 'form',
        component: AutorizadaFormComponent,
    },
    {
        path: 'form/:codAutorizada',
        component: AutorizadaFormComponent,
    },
];

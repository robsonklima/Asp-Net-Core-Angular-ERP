import { Route } from '@angular/router';
import { PontoPeriodoListaComponent } from './ponto-periodo-lista/ponto-periodo-lista.component';

export const pontoRoutes: Route[] = [
    {
        path: '',
        pathMatch: 'full',
        redirectTo: 'lista'
    },
    {
        path: 'lista',
        component: PontoPeriodoListaComponent
    }
];

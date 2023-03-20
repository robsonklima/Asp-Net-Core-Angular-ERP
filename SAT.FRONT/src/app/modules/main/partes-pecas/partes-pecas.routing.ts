import { Route } from '@angular/router';
import { PartesPecasControleListaComponent } from './controle/partes-peças-controle-lista/partes-pecas-controle-lista.component';

export const partesPecasRoutes: Route[] = [
    {
        path: '',
        pathMatch: 'full',
        redirectTo: 'controle'
    },
    {
        path: 'controle',
        component: PartesPecasControleListaComponent
    },
];

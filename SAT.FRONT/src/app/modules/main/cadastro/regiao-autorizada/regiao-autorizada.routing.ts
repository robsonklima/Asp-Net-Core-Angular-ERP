import { Route } from '@angular/router';
import { RegiaoAutorizadaListaComponent } from './regiao-autorizada-lista/regiao-autorizada-lista.component';

export const regiaoAutorizadaRoutes: Route[] = [
    {
        path: '',
        pathMatch: 'full',
        redirectTo: 'lista'
    },
    {
        path: 'lista',
        component: RegiaoAutorizadaListaComponent,
    }
];

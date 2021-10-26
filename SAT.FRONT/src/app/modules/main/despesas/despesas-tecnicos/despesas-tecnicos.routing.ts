import { Route } from '@angular/router';
import { DespesasTecnicosListaComponent } from './despesas-tecnicos-lista/despesas-tecnicos-lista.component';

export const despesasTecnicosRoutes: Route[] = [
    {
        path: '',
        pathMatch: 'full',
        redirectTo: 'lista'
    },
    {
        path: 'lista',
        component: DespesasTecnicosListaComponent,
    },
];
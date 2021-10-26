import { Route } from '@angular/router';
import { DespesasTecnicosListaComponent } from './despesas-tecnicos/despesas-tecnicos-lista/despesas-tecnicos-lista.component';

export const despesasRoutes: Route[] = [
    {
        path: 'lista',
        component: DespesasTecnicosListaComponent
    },
];
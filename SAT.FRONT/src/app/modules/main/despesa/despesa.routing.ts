import { Route } from '@angular/router';
import { DespesaTecnicoListaComponent } from './despesa-tecnico-lista/despesa-tecnico-lista.component';

export const despesaRoutes: Route[] = [
    {
        path: 'lista',
        component: DespesaTecnicoListaComponent
    },
];
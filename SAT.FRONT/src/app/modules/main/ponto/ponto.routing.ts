import { Route } from '@angular/router';
import { PontoColaboradorListaComponent } from './ponto-colaborador-lista/ponto-colaborador-lista.component';
import { PontoPeriodoListaComponent } from './ponto-periodo-lista/ponto-periodo-lista.component';

export const pontoRoutes: Route[] = [
    {
        path: '',
        pathMatch: 'full',
        redirectTo: 'periodos'
    },
    {
        path: 'periodos',
        component: PontoPeriodoListaComponent
    },
    {
        path: 'colaboradores/:codPontoPeriodo',
        component: PontoColaboradorListaComponent
    }
];

import { Route } from '@angular/router';
import { PontoColaboradorListaComponent } from './ponto-colaborador-lista/ponto-colaborador-lista.component';
import { PontoHorariosListaComponent } from './ponto-horarios-lista/ponto-horarios-lista.component';
import { PontoPeriodoFormComponent } from './ponto-periodo-form/ponto-periodo-form.component';
import { PontoPeriodoListaComponent } from './ponto-periodo-lista/ponto-periodo-lista.component';
import { PontoTurnoFormComponent } from './ponto-turno-form/ponto-turno-form.component';
import { PontoTurnoListaComponent } from './ponto-turno-lista/ponto-turno-lista.component';

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
    },
    {
        path: 'ponto-periodo-form',
        component: PontoPeriodoFormComponent
    },
    {
        path: 'ponto-periodo-form/:codPontoPeriodo',
        component: PontoPeriodoFormComponent
    },
    {
        path: 'turnos',
        component: PontoTurnoListaComponent
    },
    {
        path: 'ponto-turno-form',
        component: PontoTurnoFormComponent
    },
    {
        path: 'ponto-horarios/:codPontoPeriodo/:codUsuario',
        component: PontoHorariosListaComponent
    }
];

import { Route } from '@angular/router';
import { LiderTecnicoFormComponent } from './lider-tecnico-form/lider-tecnico-form.component';
import { LiderTecnicoListaComponent } from './lider-tecnico-lista/lider-tecnico-lista.component';

export const liderTecnicoRoutes: Route[] = [
    {
        path: '',
        pathMatch: 'full',
        redirectTo: 'lista'
    },
    {
        path: 'lista',
        component: LiderTecnicoListaComponent,
    },
    {
        path: 'form',
        component: LiderTecnicoFormComponent,
    },
    {
        path: 'form/:codLiderTecnico',
        component: LiderTecnicoFormComponent,
    },
];

import { Route } from '@angular/router';
import { TecnicoFormComponent } from './tecnico-form/tecnico-form.component';
import { TecnicoListaComponent } from './tecnico-lista/tecnico-lista.component';

export const tecnicoRoutes: Route[] = [
    {
        path: '',
        pathMatch: 'full',
        redirectTo: 'lista'
    },
    {
        path: 'lista',
        component: TecnicoListaComponent,
    },
    {
        path: 'form',
        component: TecnicoFormComponent,
    },
    {
        path: 'form/:codTecnico',
        component: TecnicoFormComponent,
    },
];

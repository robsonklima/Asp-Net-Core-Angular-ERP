import { Route } from '@angular/router';
import { TecnicoPlantaoListaComponent } from './tecnico-plantao-lista/tecnico-plantao-lista.component';

export const tecnicoPlantaoRoutes: Route[] = [
    {
        path: '',
        pathMatch: 'full',
        redirectTo: 'lista'
    },
    {
        path: 'lista',
        component: TecnicoPlantaoListaComponent
    }
];

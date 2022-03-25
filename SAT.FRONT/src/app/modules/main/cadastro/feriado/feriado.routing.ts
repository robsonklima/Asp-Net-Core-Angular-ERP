import { Route } from '@angular/router';
import { FeriadoFormComponent } from './feriado-form/feriado-form.component';
import { FeriadoListaComponent } from './feriado-lista/feriado-lista.component';

export const feriadoRoutes: Route[] = [
    {
        path: '',
        pathMatch: 'full',
        redirectTo: 'lista'
    },
    {
        path: 'lista',
        component: FeriadoListaComponent,
    },
    {
        path: 'form',
        component: FeriadoFormComponent,
    },
    {
        path: 'form/:codFeriado',
        component: FeriadoFormComponent,
    },
];

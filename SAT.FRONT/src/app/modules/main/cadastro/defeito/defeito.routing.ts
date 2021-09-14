import { Route } from '@angular/router';
import { DefeitoListaComponent } from './defeito-lista/defeito-lista.component';
import { DefeitoFormComponent } from './defeito-form/defeito-form.component';


export const defeitoRoutes: Route[] = [
    {
        path: '',
        pathMatch: 'full',
        redirectTo: 'lista'
    },
    {
        path: 'lista',
        component: DefeitoListaComponent,
    },
    {
        path: 'form',
        component: DefeitoFormComponent,
    },
    {
        path: 'form/:codDefeito',
        component: DefeitoFormComponent,
    },
];

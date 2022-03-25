import { Route } from '@angular/router';
import { DefeitoCausaFormComponent } from './defeito-causa-form/defeito-causa-form.component';
import { DefeitoCausaListaComponent } from './defeito-causa-lista/defeito-causa-lista.component';

export const defeitoCausaRoutes: Route[] = [
    {
        path: '',
        pathMatch: 'full',
        redirectTo: 'lista'
    },
    {
        path: 'lista',
        component: DefeitoCausaListaComponent,
    },
    {
        path: 'form',
        component: DefeitoCausaFormComponent,
    },
    {
        path: 'form/:codDefeitoComponente',
        component: DefeitoCausaFormComponent,
    },
];

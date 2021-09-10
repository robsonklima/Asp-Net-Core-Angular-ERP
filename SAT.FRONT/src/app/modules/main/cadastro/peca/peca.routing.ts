import { Route } from '@angular/router';
import { PecaFormComponent } from './peca-form/peca-form.component';
import { PecaListaComponent } from './peca-lista/peca-lista.component';

export const pecaRoutes: Route[] = [
    {
        path: '',
        pathMatch: 'full',
        redirectTo: 'lista'
    },
    {
        path: 'lista',
        component: PecaListaComponent,
    },
    {
        path: 'form',
        component: PecaFormComponent,
    },
    {
        path: 'form/:codPeca',
        component: PecaFormComponent,
    },
];

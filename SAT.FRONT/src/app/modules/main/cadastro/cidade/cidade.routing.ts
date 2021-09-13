import { Route } from '@angular/router';
import { CidadeFormComponent } from './cidade-form/cidade-form.component';
import { CidadeListaComponent } from './cidade-lista/cidade-lista.component';

export const cidadeRoutes: Route[] = [
    {
        path: '',
        pathMatch: 'full',
        redirectTo: 'lista'
    },
    {
        path: 'lista',
        component: CidadeListaComponent,
    },
    {
        path: 'form',
        component: CidadeFormComponent,
    },
    {
        path: 'form/:codCidade',
        component: CidadeFormComponent,
    },
];

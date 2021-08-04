import { Route } from '@angular/router';
import { RegiaoFormComponent } from './regiao-form/regiao-form.component';
import { RegiaoListaComponent } from './regiao-lista/regiao-lista.component';

export const regiaoRoutes: Route[] = [
    {
        path: '',
        pathMatch: 'full',
        redirectTo: 'lista'
    },
    {
        path: 'lista',
        component: RegiaoListaComponent,
    },
    {
        path: 'form',
        component: RegiaoFormComponent,
    },
    {
        path: 'form/:codRegiao',
        component: RegiaoFormComponent,
    },
];

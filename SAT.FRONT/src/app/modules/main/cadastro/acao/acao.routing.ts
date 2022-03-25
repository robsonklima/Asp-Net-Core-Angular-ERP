import { Route } from '@angular/router';
import { AcaoFormComponent } from './acao-form/acao-form.component';
import { AcaoListaComponent } from './acao-lista/acao-lista.component';

export const acaoRoutes: Route[] = [
    {
        path: '',
        pathMatch: 'full',
        redirectTo: 'lista'
    },
    {
        path: 'lista',
        component: AcaoListaComponent,
    },
    {
        path: 'form',
        component: AcaoFormComponent,
    },
    {
        path: 'form/:codAcao',
        component: AcaoFormComponent,
    },
];

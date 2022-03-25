import { Route } from '@angular/router';
import { AcaoCausaFormComponent } from './acao-causa-form/acao-causa-form.component';
import { AcaoCausaListaComponent } from './acao-causa-lista/acao-causa-lista.component';

export const acaoCausaRoutes: Route[] = [
    {
        path: '',
        pathMatch: 'full',
        redirectTo: 'lista'
    },
    {
        path: 'lista',
        component: AcaoCausaListaComponent,
    },
    {
        path: 'form',
        component: AcaoCausaFormComponent,
    },
    {
        path: 'form/:codAcaoComponente',
        component: AcaoCausaFormComponent,
    },
];

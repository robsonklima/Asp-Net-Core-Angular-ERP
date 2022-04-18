import { Route } from '@angular/router';
import { RegiaoAutorizadaFormComponent } from './regiao-autorizada-form/regiao-autorizada-form.component';
import { RegiaoAutorizadaListaComponent } from './regiao-autorizada-lista/regiao-autorizada-lista.component';

export const regiaoAutorizadaRoutes: Route[] = [
    {
        path: '',
        pathMatch: 'full',
        redirectTo: 'lista'
    },
    {
        path: 'lista',
        component: RegiaoAutorizadaListaComponent,
    },
    {
        path: 'form',
        component: RegiaoAutorizadaFormComponent,
    }
];

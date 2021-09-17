import { Route } from '@angular/router';
import { ContratoListaComponent } from './contrato-lista/contrato-lista.component';
import { ContratoFormComponent } from './contrato-form/contrato-form.component';


export const contratoRoutes: Route[] = [
    {
        path: '',
        pathMatch: 'full',
        redirectTo: 'lista'
    },
    {
        path: 'lista',
        component: ContratoListaComponent,
    },
    {
        path: 'form',
        component: ContratoFormComponent,
    },
    {
        path: 'form/:codContrato',
        component: ContratoFormComponent,
    },
];

import { Route } from '@angular/router';
import { ContratoListaComponent } from './contrato-lista/contrato-lista.component';
import { ContratoFormComponent } from './contrato-form/contrato-form.component';
import { ContratoFormLayoutComponent } from './contrato-form-layout/contrato-form-layout.component';


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
        path: ':codContrato',
        component: ContratoFormLayoutComponent,
    },
];

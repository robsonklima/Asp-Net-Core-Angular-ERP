import { Route } from '@angular/router';
import { ContratoListaComponent } from './contrato-lista/contrato-lista.component';
import { ContratoFormComponent } from './contrato-form/contrato-form.component';
import { ContratoFormLayoutComponent } from './contrato-form-layout/contrato-form-layout.component';
import { ContratoModeloFormComponent } from './contrato-modelo/contrato-modelo-form/contrato-modelo-form.component';


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
        path: 'modelo-form/:codContrato',
        component: ContratoModeloFormComponent,
    },
    {
        path: ':codContrato',
        component: ContratoFormLayoutComponent,
    },
];

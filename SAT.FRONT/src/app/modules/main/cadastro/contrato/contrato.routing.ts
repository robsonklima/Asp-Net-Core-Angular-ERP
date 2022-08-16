import { Route } from '@angular/router';
import { ContratoListaComponent } from './contrato-lista/contrato-lista.component';
import { ContratoFormComponent } from './contrato-form/contrato-form.component';
import { ContratoFormLayoutComponent } from './contrato-form-layout/contrato-form-layout.component';
import { ContratoModeloFormComponent } from './contrato-modelo/contrato-modelo-form/contrato-modelo-form.component';
import { ContratoServicoFormComponent } from './contrato-servico/contrato-servico-form/contrato-servico-form.component';


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
        path: ':codContrato/modelo-form',
        component: ContratoModeloFormComponent,
    },
    {
        path: ':codContrato/modelo-form/:codEquip',
        component: ContratoModeloFormComponent,
    },
    {
        path: ':codContrato/contrato-servico-form',
        component: ContratoServicoFormComponent,
    },
    {
        path: ':codContrato/contrato-servico-form/:codContratoServico',
        component: ContratoServicoFormComponent,
    },
    {
        path: ':codContrato',
        component: ContratoFormLayoutComponent,
    },
];

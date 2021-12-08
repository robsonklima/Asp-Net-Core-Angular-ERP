import { Route } from '@angular/router';
import { EquipamentoContratoFormComponent } from './equipamento-contrato-form/equipamento-contrato-form.component';
import { EquipamentoContratoListaComponent } from './equipamento-contrato-lista/equipamento-contrato-lista.component';


export const equipamentoContratoRoutes: Route[] = [
    {
        path: '',
        pathMatch: 'full',
        redirectTo: 'lista'
    },
    {
        path: 'lista',
        component: EquipamentoContratoListaComponent,
    },
    {
        path: 'form',
        component: EquipamentoContratoFormComponent,
    },
    {
        path: 'form/:codEquipContrato',
        component: EquipamentoContratoFormComponent,
    },
    {
        path: 'form/:codEquipContrato/:codContrato',
        component: EquipamentoContratoFormComponent,
    }
];

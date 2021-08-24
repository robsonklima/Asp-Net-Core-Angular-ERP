import { Route } from '@angular/router';
import { EquipamentoFormComponent } from './equipamento-form/equipamento-form.component';
import { EquipamentoListaComponent } from './equipamento-lista/equipamento-lista.component';


export const equipamentoRoutes: Route[] = [
    {
        path: '',
        pathMatch: 'full',
        redirectTo: 'lista'
    },
    {
        path: 'lista',
        component: EquipamentoListaComponent,
    },
    {
        path: 'form',
        component: EquipamentoFormComponent,
    },
    {
        path: 'form/:codEquip',
        component: EquipamentoFormComponent,
    },
];

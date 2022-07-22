import { Route } from '@angular/router';
import { EquipamentoModuloFormComponent } from './equipamento-modulo-form/equipamento-modulo-form.component';
import { EquipamentoModuloListaComponent } from './equipamento-modulo-lista/equipamento-modulo-lista.component';

export const equipamentoModuloRoutes: Route[] = [
    {
        path: '',
        pathMatch: 'full',
        redirectTo: 'lista'
    },
    {
        path: 'lista',
        component: EquipamentoModuloListaComponent,
    },
    {
        path: 'form',
        component: EquipamentoModuloFormComponent,
    },
    {
        path: 'form/:codEquip',
        component: EquipamentoModuloFormComponent,
    },
];

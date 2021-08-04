import { Route } from '@angular/router';
import { GrupoEquipamentoFormComponent } from './grupo-equipamento-form/grupo-equipamento-form.component';
import { GrupoEquipamentoListaComponent } from './grupo-equipamento-lista/grupo-equipamento-lista.component';

export const grupoEquipamentoRoutes: Route[] = [
    {
        path: '',
        pathMatch: 'full',
        redirectTo: 'lista'
    },
    {
        path: 'lista',
        component: GrupoEquipamentoListaComponent,
    },
    {
        path: 'form',
        component: GrupoEquipamentoFormComponent,
    },
    {
        path: 'form/:codGrupoEquip',
        component: GrupoEquipamentoFormComponent,
    },
];

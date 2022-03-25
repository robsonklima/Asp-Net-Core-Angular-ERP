import { Route } from '@angular/router';
import { TipoEquipamentoFormComponent } from './tipo-equipamento-form/tipo-equipamento-form.component';
import { TipoEquipamentoListaComponent } from './tipo-equipamento-lista/tipo-equipamento-lista.component';

export const grupoEquipamentoRoutes: Route[] = [
    {
        path: '',
        pathMatch: 'full',
        redirectTo: 'lista'
    },
    {
        path: 'lista',
        component: TipoEquipamentoListaComponent,
    },
    {
        path: 'form',
        component: TipoEquipamentoFormComponent,
    },
    {
        path: 'form/:codTipoEquip',
        component: TipoEquipamentoFormComponent,
    },
];

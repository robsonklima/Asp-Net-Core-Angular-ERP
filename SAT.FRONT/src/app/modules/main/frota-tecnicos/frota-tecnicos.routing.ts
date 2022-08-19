import { Route } from '@angular/router';
import { AuditoriaListaComponent } from './auditoria/auditoria-lista/auditoria-lista.component';
import { ValoresCombustivelFormComponent } from './valores-combustivel/valores-combustivel-form/valores-combustivel-form.component';
import { ValoresCombustivelListaComponent } from './valores-combustivel/valores-combustivel-lista/valores-combustivel-lista.component';

export const frotaTecnicosRoutes: Route[] = [
    {
        path: '',
        pathMatch: 'full',
        redirectTo: 'lista'
    },
    {
        path: 'lista',
        component: AuditoriaListaComponent
    },
    {
        path: 'valores-combustivel',
        component: ValoresCombustivelListaComponent
    },
    {
        path: 'valores-combustivel/form',
        component: ValoresCombustivelFormComponent
    },
    {
        path: 'valores-combustivel/form/:codDespesaConfiguracaoCombustivel',
        component: ValoresCombustivelFormComponent
    },
];

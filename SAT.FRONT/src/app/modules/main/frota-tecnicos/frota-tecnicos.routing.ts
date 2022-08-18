import { Route } from '@angular/router';
import { AuditoriaListaComponent } from './auditoria/auditoria-lista/auditoria-lista.component';
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
];

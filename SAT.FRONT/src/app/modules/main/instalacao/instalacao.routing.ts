import { Route } from '@angular/router';
import { InstalacaoContratoComponent } from './instalacao-contrato/instalacao-contrato.component';

export const instalacaoRoutes: Route[] = [
    {
        path: '',
        pathMatch: 'full',
        redirectTo: 'instalacao/contrato'
    },
    {
        path: 'instalacao/contrato',
        component: InstalacaoContratoComponent
    }
];

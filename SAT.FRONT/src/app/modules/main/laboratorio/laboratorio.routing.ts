import { Route } from '@angular/router';
import { LaboratorioPainelControleComponent } from './laboratorio-painel-controle/laboratorio-painel-controle.component';

export const laboratorioRoutes: Route[] = [
    {
        path: '',
        pathMatch: 'full',
        redirectTo: 'painel-controle'
    },
    {
        path: 'painel-controle',
        component: LaboratorioPainelControleComponent
    }    
];

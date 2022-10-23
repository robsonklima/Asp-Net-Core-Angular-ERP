import { Route } from '@angular/router';
import { LaboratorioBancadaComponent } from './laboratorio-bancada/laboratorio-bancada.component';
import { LaboratorioPainelControleComponent } from './laboratorio-painel-controle/laboratorio-painel-controle.component';
import { LaboratorioProcessoReparoDetalheComponent } from './laboratorio-processo-reparo/laboratorio-processo-reparo-detalhe/laboratorio-processo-reparo-detalhe.component';
import { LaboratorioProcessoReparoListaComponent } from './laboratorio-processo-reparo/laboratorio-processo-reparo-lista/laboratorio-processo-reparo-lista.component';

export const laboratorioRoutes: Route[] = [
    {
        path: '',
        pathMatch: 'full',
        redirectTo: 'painel-controle'
    },
    {
        path: 'painel-controle',
        component: LaboratorioPainelControleComponent
    },
    {
        path: 'bancada',
        component: LaboratorioBancadaComponent
    } ,
    {
        path: 'processo-reparo',
        component: LaboratorioProcessoReparoListaComponent
    } ,
    {
        path: 'processo-reparo/:codOR',
        component: LaboratorioProcessoReparoDetalheComponent
    }    
];

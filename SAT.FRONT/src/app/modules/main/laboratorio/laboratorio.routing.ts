import { Route } from '@angular/router';
import { LaboratorioBancadaComponent } from './laboratorio-bancada/laboratorio-bancada.component';
import { LaboratorioOrdemReparoFormComponent } from './laboratorio-ordem-reparo/laboratorio-ordem-reparo-form/laboratorio-ordem-reparo-form.component';
import { LaboratorioOrdemReparoListaComponent } from './laboratorio-ordem-reparo/laboratorio-ordem-reparo-lista/laboratorio-ordem-reparo-lista.component';
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
    },
    {
        path: 'processo-reparo/:codOR',
        component: LaboratorioProcessoReparoDetalheComponent
    },
    {
        path: 'ordem-reparo',
        component: LaboratorioOrdemReparoListaComponent
    },
    {
        path: 'ordem-reparo/form',
        component: LaboratorioOrdemReparoFormComponent
    },
    {
        path: 'ordem-reparo/form/:codOR',
        component: LaboratorioOrdemReparoFormComponent
    }
];

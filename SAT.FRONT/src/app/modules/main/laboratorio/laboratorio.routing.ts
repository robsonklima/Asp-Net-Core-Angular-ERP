import { Route } from '@angular/router';
import { LaboratorioBancadaComponent } from './laboratorio-bancada/laboratorio-bancada.component';
import { LaboratorioCheckListListaComponent } from './laboratorio-checklist/laboratorio-checklist-lista/laboratorio-checklist-lista.component';
import { LaboratorioDashboardComponent } from './laboratorio-dashboard/laboratorio-dashboard.component';
import { LaboratorioOrcamentoListaComponent } from './laboratorio-orcamento/laboratorio-orcamento-lista/laboratorio-orcamento-lista.component';
import { LaboratorioOSBancadaFormComponent } from './laboratorio-os-bancada/laboratorio-os-bancada-form/laboratorio-os-bancada-form.component';
import { LaboratorioOrdemReparoFormComponent } from './laboratorio-ordem-reparo/laboratorio-ordem-reparo-form/laboratorio-ordem-reparo-form.component';
import { LaboratorioOrdemReparoListaComponent } from './laboratorio-ordem-reparo/laboratorio-ordem-reparo-lista/laboratorio-ordem-reparo-lista.component';
import { LaboratorioOSBancadaListaComponent } from './laboratorio-os-bancada/laboratorio-os-bancada-lista/laboratorio-os-bancada-lista.component';
import { LaboratorioPainelControleComponent } from './laboratorio-painel-controle/laboratorio-painel-controle.component';
import { LaboratorioProcessoReparoDetalheComponent } from './laboratorio-processo-reparo/laboratorio-processo-reparo-detalhe/laboratorio-processo-reparo-detalhe.component';
import { LaboratorioProcessoReparoHistoricoComponent } from './laboratorio-processo-reparo/laboratorio-processo-reparo-historico/laboratorio-processo-reparo-historico.component';
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
        path: 'processo-reparo/historico/:codORItem',
        component: LaboratorioProcessoReparoHistoricoComponent
    },
    {
        path: 'processo-reparo/detalhe',
        component: LaboratorioProcessoReparoDetalheComponent
    },
    {
        path: 'processo-reparo/detalhe/:codORItem',
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
    },
    {
        path: 'checklist',
        component: LaboratorioCheckListListaComponent
    },
    {
        path: 'dashboard',
        component: LaboratorioDashboardComponent
    },
    {
        path: 'orcamento-bancada',
        component: LaboratorioOrcamentoListaComponent
    },
    {
        path: 'os-bancada/form',
        component: LaboratorioOSBancadaFormComponent
    },
    {
        path: 'os-bancada/form/:codOsbancada',
        component: LaboratorioOSBancadaFormComponent
    },
    {
        path: 'os-bancada',
        component: LaboratorioOSBancadaListaComponent
    },
];

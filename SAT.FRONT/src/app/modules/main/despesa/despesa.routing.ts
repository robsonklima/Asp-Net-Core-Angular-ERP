import { Route } from '@angular/router';
import { DespesaAtendimentoListaComponent } from './despesa-atendimento-lista/despesa-atendimento-lista.component';
import { DespesaAtendimentoRelatorioListaComponent } from './despesa-atendimento-relatorio-lista/despesa-atendimento-relatorio-lista.component';
import { DespesaManutencaoComponent } from './despesa-manutencao/despesa-manutencao.component';
import { DespesaTecnicoListaComponent } from './despesa-tecnico-lista/despesa-tecnico-lista.component';

export const despesaRoutes: Route[] = [
    {
        path: '',
        pathMatch: 'full',
        redirectTo: 'tecnicos'
    },
    {
        path: 'tecnicos',
        component: DespesaTecnicoListaComponent
    },
    {
        path: 'atendimentos',
        component: DespesaAtendimentoListaComponent
    },
    {
        path: 'atendimentos/relatorios/:codDespesaPeriodo',
        component: DespesaAtendimentoRelatorioListaComponent
    },
    {
        path: 'atendimentos/relatorios/:codDespesaPeriodo/despesa/:codRAT',
        component: DespesaManutencaoComponent
    }
];
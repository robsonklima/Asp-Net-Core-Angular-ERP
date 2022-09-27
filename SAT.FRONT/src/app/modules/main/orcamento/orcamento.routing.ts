import { Route } from '@angular/router';
import { OrcamentoDetalheComponent } from './orcamento-detalhe/orcamento-detalhe.component';
import { OrcamentoFaturamentoFormComponent } from './orcamento-faturamento/orcamento-faturamento-form/orcamento-faturamento-form.component';
import { OrcamentoFaturamentoListaComponent } from './orcamento-faturamento/orcamento-faturamento-lista/orcamento-faturamento-lista.component';
import { OrcamentoFinanceiroFaturamentoListaComponent } from './orcamento-financeiro/orcamento-financeiro-faturamento/orcamento-financeiro-faturamento-lista/orcamento-financeiro-faturamento-lista.component';
import { OrcamentoImpressaoComponent } from './orcamento-impressao/orcamento-impressao.component';
import { OrcamentoListaComponent } from './orcamento-lista/orcamento-lista.component';
import { OrcamentoPesquisaComponent } from './orcamento-pesquisa/orcamento-pesquisa.component';

export const orcamentoRoutes: Route[] = [
    {
        path: '',
        pathMatch: 'full',
        redirectTo: 'lista'
    },
    {
        path: 'lista',
        component: OrcamentoListaComponent
    },
    {
        path: 'pesquisa',
        component: OrcamentoPesquisaComponent 
    },
    {
        path: 'detalhe/:codOrc/:codOS',
        component: OrcamentoDetalheComponent
    },
    {
        path: 'impressao/:codOrc',
        component: OrcamentoImpressaoComponent
    },
    {
        path: 'faturamento/form',
        component: OrcamentoFaturamentoFormComponent
    },  
    {
        path: 'faturamento/form/:codLocalEnvioNFFaturamento',
        component: OrcamentoFaturamentoFormComponent
    },    
    {
        path: 'faturamento',
        component: OrcamentoFaturamentoListaComponent
    },      
    {
        path: 'financeiro/faturamento',
        component: OrcamentoFinanceiroFaturamentoListaComponent
    },     
];

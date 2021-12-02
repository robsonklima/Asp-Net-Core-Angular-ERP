import { Route } from "@angular/router";
import { DespesaAdiantamentoFormComponent } from "./despesa-adiantamento-form/despesa-adiantamento-form.component";
import { DespesaAdiantamentoListaComponent } from "./despesa-adiantamento-lista/despesa-adiantamento-lista.component";
import { DespesaAtendimentoListaComponent } from "./despesa-atendimento-lista/despesa-atendimento-lista.component";
import { DespesaAtendimentoReprovacaoListaComponent } from "./despesa-atendimento-lista/despesa-atendimento-reprovacao-lista/despesa-atendimento-reprovacao-lista.component";
import { DespesaAtendimentoRelatorioListaComponent } from "./despesa-atendimento-relatorio-lista/despesa-atendimento-relatorio-lista.component";
import { DespesaCartaoCombustivelDetalheComponent } from "./despesa-cartao-combustivel-detalhe/despesa-cartao-combustivel-detalhe.component";
import { DespesaCartaoCombustivelFormComponent } from "./despesa-cartao-combustivel-form/despesa-cartao-combustivel-form.component";
import { DespesaCartaoCombustivelListaComponent } from "./despesa-cartao-combustivel-lista/despesa-cartao-combustivel-lista.component";
import { DespesaConfiguracaoFormComponent } from "./despesa-configuracao-form/despesa-configuracao-form.component";
import { DespesaConfiguracaoListaComponent } from "./despesa-configuracao-lista/despesa-configuracao-lista.component";
import { DespesaCreditoCartaoListaComponent } from "./despesa-credito-cartao-lista/despesa-credito-cartao-lista.component";
import { DespesaManutencaoComponent } from "./despesa-manutencao/despesa-manutencao.component";
import { DespesaPeriodoFormComponent } from "./despesa-periodo-form/despesa-periodo-form.component";
import { DespesaPeriodoListaComponent } from "./despesa-periodo-lista/despesa-periodo-lista.component";
import { DespesaProtocoloDetalheComponent } from "./despesa-protocolo-detalhe/despesa-protocolo-detalhe.component";
import { DespesaProtocoloListaComponent } from "./despesa-protocolo-lista/despesa-protocolo-lista.component";
import { DespesaTecnicoListaComponent } from "./despesa-tecnico-lista/despesa-tecnico-lista.component";


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
        path: 'atendimentos/:codTecnico',
        component: DespesaAtendimentoListaComponent
    },
    {
        path: 'atendimentos/:codTecnico/reprovacao/:codDespesaPeriodoTecnico',
        component: DespesaAtendimentoReprovacaoListaComponent
    },
    {
        path: 'atendimentos/relatorios/:codDespesaPeriodo',
        component: DespesaAtendimentoRelatorioListaComponent
    },
    {
        path: 'atendimentos/:codTecnico/relatorios/:codDespesaPeriodo',
        component: DespesaAtendimentoRelatorioListaComponent
    },
    {
        path: 'atendimentos/:codTecnico/relatorios/:codDespesaPeriodo/despesa/:codRAT',
        component: DespesaManutencaoComponent
    },
    {
        path: 'atendimentos/relatorios/:codDespesaPeriodo/despesa/:codRAT',
        component: DespesaManutencaoComponent
    },
    {
        path: 'cartoes-combustivel',
        component: DespesaCartaoCombustivelListaComponent
    },
    {
        path: 'cartoes-combustivel/form',
        component: DespesaCartaoCombustivelFormComponent
    },
    {
        path: 'cartoes-combustivel/form/:codDespesaCartaoCombustivel',
        component: DespesaCartaoCombustivelFormComponent
    },
    {
        path: 'cartoes-combustivel/detalhe/:codDespesaCartaoCombustivel',
        component: DespesaCartaoCombustivelDetalheComponent
    },
    {
        path: 'adiantamentos',
        component: DespesaAdiantamentoListaComponent
    },
    {
        path: 'adiantamentos/form',
        component: DespesaAdiantamentoFormComponent
    },
    {
        path: 'adiantamentos/form/:codDespesaAdiantamento',
        component: DespesaAdiantamentoFormComponent
    },
    {
        path: 'protocolos',
        component: DespesaProtocoloListaComponent
    },
    {
        path: 'protocolos/detalhe/:codDespesaProtocolo',
        component: DespesaProtocoloDetalheComponent
    },
    {
        path: 'creditos-cartao',
        component: DespesaCreditoCartaoListaComponent
    },
    {
        path: 'periodos',
        component: DespesaPeriodoListaComponent
    },
    {
        path: 'periodos/form',
        component: DespesaPeriodoFormComponent
    },
    {
        path: 'periodos/form/:codDespesaPeriodo',
        component: DespesaPeriodoFormComponent
    },
    {
        path: 'configuracoes',
        component: DespesaConfiguracaoListaComponent
    },
    {
        path: 'configuracoes/form',
        component: DespesaConfiguracaoFormComponent
    }
];
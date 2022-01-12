import { Cidade } from "./cidade.types";
import { Meta, QueryStringParameters } from "./generic.types";
import { OrcamentoISS } from "./orcamento.types";

export interface Filial
{
    codFilial: number;
    razaoSocial: string;
    nomeFilial: string;
    cidade: Cidade;
    endereco: string;
    cep: string;
    fone?: string;
    cnpj?: string;
    bairro: string;
    orcamentoISS?: OrcamentoISS;
}

export interface FilialData extends Meta
{
    items: Filial[];
};

export interface FilialParameters extends QueryStringParameters
{
    codFilial?: number;
    codFiliais?: string;
    indAtivo?: number;
    SiglaUF?: string;
    include?: FilialIncludeEnum;
    filterType?: FilialFilterEnum;
    periodoInicioAtendendimento?: string;
    periodoFimAtendendimento?: string;
};

export enum FilialIncludeEnum
{
    FILIAL_ORDENS_SERVICO = 1
}

export enum FilialFilterEnum
{
    FILTER_DASHBOARD_DISPONIBILIDADE_TECNICOS = 1
}

export class DashboardTecnicoDisponibilidadeFilialViewModel implements Filial
{
    codFilial: number;
    razaoSocial: string;
    nomeFilial: string;
    cidade: Cidade;
    endereco: string;
    cep: string;
    qtdOSNaoTransferidasCorretivas: number;
    bairro: string;
}
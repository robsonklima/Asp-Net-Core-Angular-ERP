import { Meta, QueryStringParameters } from "./generic.types";

export interface OrcamentoFaturamento {
    codOrcamentoFaturamento: number | null;
    codOrcamento: number | null;
    codClienteBancada: string;
    codFilial: number | null;
    numOSPerto: number | null;
    numOrcamento: string;
    descricaoNotaFiscal: string;
    valorPeca: string;
    qtdePeca: number | null;
    valorServico: string;
    numNF: number | null;
    dataEmissaoNF: string;
    indFaturado: number | null;
    indRegistroDanfe: string;
    caminhoDanfe: string;
    codUsuarioCad: string;
    dataHoraCad: string;
}

export interface OrcamentoFaturamentoParameters extends QueryStringParameters {
    codOrc?: number | null;
}

export interface OrcamentoFaturamentoData extends Meta
{
    items: OrcamentoFaturamento[];
};
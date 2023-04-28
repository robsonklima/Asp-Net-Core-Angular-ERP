import { Meta, QueryStringParameters } from "./generic.types";

export interface OrcamentoFaturamento {
    codOrcamentoFaturamento?: number;
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

export interface OrcamentoFaturamentoViewModel {
    codigo?: number;
    cliente: string;
    filial: string;
    codOS: number | null;
    numOSCliente: string;
    numOrcamento: string;
    descNF: string;
    numNF: number | null;
    dataEmissao: string;
    indFaturado: number | null;
    codOrc: number | null;
    tipo: OrcamentoFaturamentoTipoEnum;
    codFilial: number | null;
    codClienteBancada: string;
    valorPeca: string;
    qtdePeca: number | null;
    valorServico: string;
    indRegistroDanfe: string;
    caminhoDanfe: string;
}

export interface OrcamentoFaturamentoParameters extends QueryStringParameters {
    codOrc?: number | null;
}

export interface OrcamentoFaturamentoData extends Meta
{
    items: OrcamentoFaturamentoViewModel[];
};

export enum OrcamentoFaturamentoTipoEnum {
    SERVICO = 1,
    MATERIAL = 2,
    FATURAMENTO = 3
}
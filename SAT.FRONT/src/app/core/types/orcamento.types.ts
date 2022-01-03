import { Meta, QueryStringParameters } from "./generic.types";

export interface Orcamento
{
    codOrc: number;
    codigoMotivo: number;
    codigoStatus: number;
    codigoSla: number;
    codigoEquipamento: number;
    codigoCliente: number;
    codigoPosto: number;
    codigoFilial: number;
    codigoContrato: number;
    isMaterialEspecifico: number;
    codigoOrdemServico: number;
    codigoEquipamentoContrato: number;
    descricaoOutroMotivo: string;
    detalhe: string;
    nomeContrato: string;
    numero: string;
    data: string;
    valorIss: number;
    valorTotal: number;
    valorTotalDesconto: number;
    dataCadastro: string;
    usuarioCadastro: string;
    dataEnvioAprovacao: string;
    dataAprovacaoCliente: string;
}

export interface OrcamentoData extends Meta
{
    items: Orcamento[];
};

export interface OrcamentoParameters extends QueryStringParameters
{
    codStatusServicos?: string;
};

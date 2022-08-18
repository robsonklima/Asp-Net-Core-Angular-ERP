import { Meta, QueryStringParameters } from "./generic.types";

export interface OrcamentoOutroServico
{
    codOrcOutroServico: number;
    codOrc: number;
    tipo: string;
    descricao: string;
    valorUnitario: number;
    quantidade: number;
    valorTotal: number;
    dataCadastro: string;
    usuarioCadastro: string;
}

export interface OrcamentoOutroServicoData extends Meta
{
    items: OrcamentoOutroServico[];
};

export interface OrcamentoOutroServicoParameters extends QueryStringParameters
{
};
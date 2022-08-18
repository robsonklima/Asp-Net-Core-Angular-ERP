import { Meta, QueryStringParameters } from "./generic.types";
import { Peca } from "./peca.types";

export interface OrcamentoMaterial
{
    codOrcMaterial?: number;
    codOrc: number;
    codigoMagnus: string;
    codigoPeca: string;
    descricao: string;
    valorUnitario: number;
    valorDesconto?: number;
    valorTotal?: number;
    quantidade: number;
    dataCadastro: string;
    usuarioCadastro: string;
    valorIpi: number;
    valorUnitarioFinanceiro: number;
    seqItemPedido?: number;
    peca?: Peca;
}

export interface OrcamentoMaterialData extends Meta
{
    items: OrcamentoMaterial[];
};

export interface OrcamentoMaterialParameters extends QueryStringParameters
{
};
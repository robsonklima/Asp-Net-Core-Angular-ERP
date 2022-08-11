import { Meta, QueryStringParameters } from "./generic.types";

export class OrcFormaPagamento {
    CodOrcFormaPagamento: number;
    Nome: string;
    CodigoLogix: number;
}

export interface OrcFormaPagamentoData extends Meta {
    items: OrcFormaPagamento[];
};

export interface OrcFormaPagamentoParameters extends QueryStringParameters {

}
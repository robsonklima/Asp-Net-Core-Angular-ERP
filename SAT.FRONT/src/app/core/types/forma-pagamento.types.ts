import { Meta, QueryStringParameters } from "./generic.types";

export class FormaPagamento {
    codFormaPagto: number;
    descFormaPagto: string;
    percAjuste: number;
    indAtivo: number;
}

export interface FormaPagamentoData extends Meta {
    items: FormaPagamento[];
};

export interface FormaPagamentoParameters extends QueryStringParameters {
    indAtivo?: number;
    codFormaPagto?: number;
};
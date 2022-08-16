import { Meta, QueryStringParameters } from "./generic.types";

export class OrcDadosBancarios {
    CodOrcFormaPagamento: number;
    Nome: string;
    CodigoLogix: number;
}

export interface OrcDadosBancariosData extends Meta {
    items: OrcDadosBancarios[];
};

export interface OrcDadosBancariosParameters extends QueryStringParameters {

}
import { Meta, QueryStringParameters } from "./generic.types";

export class Contrato {
    codContrato: number;
    nroContrato: string;
    nomeContrato: string;
}

export interface ContratoData extends Meta {
    items: Contrato[];
};

export interface ContratoParameters extends QueryStringParameters {
    indAtivo?: number;
    codCliente?: number;
};
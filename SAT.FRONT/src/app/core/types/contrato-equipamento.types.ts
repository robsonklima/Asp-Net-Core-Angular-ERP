import { Meta, QueryStringParameters } from "./generic.types";

export class Acao {
    codContrato?: number;
    codEquip: string;
    codTipoEquip: string;
    indAtivo?: number;
}

export interface AcaoData extends Meta {
    acoes: Acao[]
};

export interface AcaoParameters extends QueryStringParameters {
    indAtivo?: number;
};
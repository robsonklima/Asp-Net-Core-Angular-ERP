import { Meta, QueryStringParameters } from "./generic.types";

export class Acao {
    codAcao?: number;
    codEAcao: string;
    nomeAcao: string;
    indAtivo?: number;
}

export interface AcaoData extends Meta {
    acoes: Acao[]
};

export interface AcaoParameters extends QueryStringParameters {
    indAtivo?: number;
};
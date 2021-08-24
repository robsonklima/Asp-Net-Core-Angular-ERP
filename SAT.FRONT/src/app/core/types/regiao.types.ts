import { Meta, QueryStringParameters } from "./generic.types";

export class Regiao {
    codRegiao: number;
    nomeRegiao: string;
    indAtivo: number;
}

export interface RegiaoData extends Meta {
    regioes: Regiao[];
};

export interface RegiaoParameters extends QueryStringParameters {
    codRegiao?: number;
    indAtivo?: number;
};
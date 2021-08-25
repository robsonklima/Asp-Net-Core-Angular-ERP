import { Meta, QueryStringParameters } from "./generic.types";

export class Defeito {
    codDefeito: number;
    codEDefeito: string;
    nomeDefeito: string;
    indAtivo: number;
}

export interface DefeitoData extends Meta {
    items: Defeito[];
};

export interface DefeitoParameters extends QueryStringParameters {
    codDefeito?: number;
    indAtivo?: number;
};
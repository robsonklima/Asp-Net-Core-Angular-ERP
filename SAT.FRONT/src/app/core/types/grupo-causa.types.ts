import { Meta, QueryStringParameters } from "./generic.types";

export class GrupoCausa {
    codGrupoCausa: number;
    codEGrupoCausa: string;
    nomeGrupoCausa: string;
}

export interface GrupoCausaData extends Meta {
    items: GrupoCausa[];
};

export interface GrupoCausaParameters extends QueryStringParameters {
    indAtivo?: number;
    codEGrupoCausa?: string;
};
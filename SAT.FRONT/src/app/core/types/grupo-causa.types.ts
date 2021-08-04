import { Meta, QueryStringParameters } from "./generic.types";

export class GrupoCausa {
    codGrupoCausa: number;
    codEGrupoCausa: string;
    nomeGrupoCausa: string;
}

export interface GrupoCausaData extends Meta {
    gruposCausa: GrupoCausa[];
};

export interface GrupoCausaParameters extends QueryStringParameters {
    indAtivo?: number;
    codEGrupoCausa?: string;
};
import { Meta, QueryStringParameters } from "./generic.types";

export interface Causa {
    codCausa?: number;
    codTipoCausa?: number;
    codGrupoCausa?: number;
    codECausa?: string;
    nomeCausa?: string;
    codServico?: number;
    indAtivo?: number;
    codTraducao?: any;
}

export interface CausaData extends Meta {
    items: Causa[];
};

export interface CausaParameters extends QueryStringParameters {
    CodCausa?: number;
    indAtivo?: number;
    apenasModulos?: number;
    codECausa?: string;
};
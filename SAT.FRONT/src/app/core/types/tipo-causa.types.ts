import { Meta, QueryStringParameters } from "./generic.types";

export class TipoCausa {
    codTipoCausa: number;
    codETipoCausa: string;
    nomeTipoCausa: string;
}

export interface TipoCausaData extends Meta {
    items: TipoCausa[];
};

export interface TipoCausaParameters extends QueryStringParameters {
    indAtivo?: number;
    codETipoCausa?: string;
};

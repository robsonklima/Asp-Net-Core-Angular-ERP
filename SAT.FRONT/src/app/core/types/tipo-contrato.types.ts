import { Meta, QueryStringParameters } from "./generic.types";

export class TipoContrato {
    codTipoContrato: number;
    nomeTipoContrato: string;
}

export interface TipoContratoData extends Meta {
    items: TipoContrato[];
};

export interface TipoContratoParameters extends QueryStringParameters {
    codTipoContrato?: number;
    nomeTipoContrato?: string;
};

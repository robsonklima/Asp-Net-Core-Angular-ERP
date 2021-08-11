import { Meta, QueryStringParameters } from "./generic.types";

export class TipoRota {
    codTipoRota: number;
    nomeTipoRota: string;
}


export interface TipoRotaData extends Meta {
    tiposRota: TipoRota[];
};

export interface TipoRotaParameters extends QueryStringParameters {
    codTipoRota?: number;
};
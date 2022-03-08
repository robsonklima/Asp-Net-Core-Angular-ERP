import { Meta, QueryStringParameters } from "./generic.types";

export class TipoFrete {
    codTipoFrete: number;
    siglaTipoFrete: string;
    nomeTipoFrete: string;
    descTipoFrete: string;
}

export interface TipoFreteData extends Meta {
    items: TipoFrete[];
};

export interface TipoFreteParameters extends QueryStringParameters {
};
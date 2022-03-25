import { Meta, QueryStringParameters } from "./generic.types";

export class BancadaLista {
    codBancadaLista: number;
    nomeBancadaLista: string;
    indAtivo: number;
}

export interface BancadaListaData extends Meta {
    items: BancadaLista[];
};

export interface BancadaListaParameters extends QueryStringParameters {
    indAtivo?: number;
};
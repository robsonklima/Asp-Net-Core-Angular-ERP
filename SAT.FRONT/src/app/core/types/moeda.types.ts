import { Meta, QueryStringParameters } from "./generic.types";

export class Moeda {
    codMoeda: number;
    nomeMoeda: string;
    siglaMoeda: string;
}

export interface MoedaData extends Meta {
    items: Moeda[];
};

export interface MoedaParameters extends QueryStringParameters {
};
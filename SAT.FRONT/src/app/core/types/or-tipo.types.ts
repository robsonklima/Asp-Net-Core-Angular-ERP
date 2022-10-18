import { Meta, QueryStringParameters } from "./generic.types";

export interface ORTipo {
    codTipoOR: number;
    descricaoTipo: string;
}

export interface ORTipoData extends Meta {
    items: ORTipo[];
};

export interface ORTipoParameters extends QueryStringParameters {

}
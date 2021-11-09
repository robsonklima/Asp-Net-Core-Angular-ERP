import { Meta, QueryStringParameters } from "./generic.types";

export class PontoPeriodoStatus {
    codPontoPeriodoStatus: number;
    nomePeriodoStatus: string;
    indAtivo: number;
}

export interface PontoPeriodoStatusData extends Meta {
    items: PontoPeriodoStatus[];
};

export interface PontoPeriodoStatusParameters extends QueryStringParameters {

};
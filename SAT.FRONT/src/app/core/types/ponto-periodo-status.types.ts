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

export const pontoPeriodoStatusConst = {
    INDISPONIVEL: 1,
    ABERTO: 2,
    EM_ANALISE: 3,
    CONSOLIDADO: 4
}
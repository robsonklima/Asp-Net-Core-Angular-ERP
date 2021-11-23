import { Meta, QueryStringParameters } from "./generic.types";

export class PontoPeriodoModoAprovacao {
    codPontoPeriodoModoAprovacao: number;
    descricao: string;
    dataHoraCad: string;
    codUsuarioCad: string;
}

export interface PontoPeriodoModoAprovacaoData extends Meta {
    items: PontoPeriodoModoAprovacao[];
};

export interface PontoPeriodoModoAprovacaoParameters extends QueryStringParameters {

};

export const pontoPeriodoModoAprovacaoConst = {
    DIARIO: 1,
    FINAL_DO_PERIODO: 2
}
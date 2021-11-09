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
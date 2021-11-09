import { Meta, QueryStringParameters } from "./generic.types";

export class PontoPeriodo {
    codPontoPeriodo: number;
    dataInicio: string;
    dataFim: string;
    codPontoPeriodoStatus: number;
    codUsuarioCad: string;
    dataHoraCad: string;
    codUsuarioManut: string;
    dataHoraManut: string;
    codPontoPeriodoModoAprovacao: number;
    codPontoPeriodoIntervaloAcessoData: number;
}

export interface PontoPeriodoData extends Meta {
    items: PontoPeriodo[];
};

export interface PontoPeriodoParameters extends QueryStringParameters {

};
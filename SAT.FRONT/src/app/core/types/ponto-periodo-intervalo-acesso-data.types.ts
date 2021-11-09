import { Meta, QueryStringParameters } from "./generic.types";

export class PontoPeriodoIntervaloAcessoData {
    codPontoPeriodoIntervaloAcessoData: number;
    intervaloDias: number;
    descricao: string;
    codUsuarioCad: string;
    dataHoraCad: string;
}

export interface PontoPeriodoIntervaloAcessoDataData extends Meta {
    items: PontoPeriodoIntervaloAcessoData[];
};

export interface PontoPeriodoIntervaloAcessoDataParameters extends QueryStringParameters {

};
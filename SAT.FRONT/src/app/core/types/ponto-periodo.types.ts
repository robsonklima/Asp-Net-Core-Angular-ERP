import { Meta, QueryStringParameters } from "./generic.types";
import { PontoPeriodoIntervaloAcessoData } from "./ponto-periodo-intervalo-acesso-data.types";
import { PontoPeriodoModoAprovacao } from "./ponto-periodo-modo-aprovacao.types";
import { PontoPeriodoStatus } from "./ponto-periodo-status.types";

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
    pontoPeriodoStatus: PontoPeriodoStatus;
    pontoPeriodoModoAprovacao: PontoPeriodoModoAprovacao;
    pontoPeriodoIntervaloAcessoData: PontoPeriodoIntervaloAcessoData;
}

export interface PontoPeriodoData extends Meta {
    items: PontoPeriodo[];
};

export interface PontoPeriodoParameters extends QueryStringParameters {

};
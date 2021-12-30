import { Meta, QueryStringParameters } from "./generic.types";

export class PontoPeriodoUsuarioStatus {
    codPontoPeriodoUsuarioStatus: string;
    descricao: string;
    dataHoraCad: string;
    codUsuarioCad: string;
}

export interface PontoPeriodoUsuarioStatusData extends Meta {
    items: PontoPeriodoUsuarioStatus[];
};

export interface PontoPeriodoUsuarioStatusParameters extends QueryStringParameters {

};

export const pontoPeriodoUsuarioStatusConst = {
    INCONSISTENTE: 1,
    CONFERIDO: 2,
    AGUARDANDO_CONFERENCIA: 3,
    SEM_REGISTRO: 4
}
import { Meta, QueryStringParameters } from "./generic.types";
import { PontoPeriodoUsuarioStatus } from "./ponto-periodo-usuario-status.types";
import { PontoPeriodo } from "./ponto-periodo.types";

export class PontoPeriodoUsuario {
    codPontoPeriodoUsuario: number;
    codPontoPeriodo : number;
    codUsuario: string;
    dataHoraCad: string;
    codUsuarioCad: string;
    dataHoraManut: string;
    codUsuarioManut: string;
    codPontoPeriodoUsuarioStatus: number;
    pontoPeriodo: PontoPeriodo;
    pontoPeriodoUsuarioStatus: PontoPeriodoUsuarioStatus;
}

export interface PontoPeriodoUsuarioData extends Meta {
    items: PontoPeriodoUsuario[];
};

export interface PontoPeriodoUsuarioParameters extends QueryStringParameters {
    codPontoPeriodo?: number;
    codUsuario?: string;
};
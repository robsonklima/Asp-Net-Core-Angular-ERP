import { Meta, QueryStringParameters } from "./generic.types";
import { PontoUsuario } from "./ponto-usuario.types";

export class PontoUsuarioData {
    codPontoUsuarioData: number;
    codUsuario: string;
    codPontoPeriodo: number;
    codPontoUsuarioDataStatus: number;
    dataRegistro: string;
    dataHoraCad: string;
    codUsuarioCad: string;
    dataHoraManut: string;
    codUsuarioManut: string;
    codPontoUsuarioDataStatusAcesso: number;
    pontosUsuario: PontoUsuario[];
}

export interface PontoUsuarioDataData extends Meta {
    items: PontoUsuarioData[];
};

export interface PontoUsuarioDataParameters extends QueryStringParameters {
    codUsuario?: string;
    codPontoPeriodo?: number;
};
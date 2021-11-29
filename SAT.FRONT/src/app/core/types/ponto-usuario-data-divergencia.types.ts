import { Meta, QueryStringParameters } from "./generic.types";

export interface PontoUsuarioDataDivergencia {
    codPontoUsuarioDataDivergencia: number;
    codPontoUsuarioData: number;
    codPontoUsuarioDataModoDivergencia: number;
    dataHoraCad: string;
    codUsuarioCad: string;
    codPontoUsuarioDataMotivoDivergencia: number;
    divergenciaValidada: number;
}

export interface PontoUsuarioDataDivergenciaData extends Meta {
    items: PontoUsuarioDataDivergencia[];
};

export interface PontoUsuarioDataDivergenciaParameters extends QueryStringParameters {
    codPontoUsuarioData?: number;
};
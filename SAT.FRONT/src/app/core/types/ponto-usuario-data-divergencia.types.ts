import { Meta, QueryStringParameters } from "./generic.types";

export interface PontoUsuarioDataDivergencia {
    codPontoUsuarioDataDivergencia?: number;
    codPontoUsuarioData: number;
    codPontoUsuarioDataModoDivergencia: number;
    dataHoraCad: string;
    codUsuarioCad: string;
    codPontoUsuarioDataMotivoDivergencia: number;
    divergenciaValidada: number;
    pontoUsuarioDataModoDivergencia?: PontoUsuarioDataModoDivergencia;
    pontoUsuarioDataMotivoDivergencia?: PontoUsuarioDataMotivoDivergencia;
}

export interface PontoUsuarioDataModoDivergencia {
    codPontoUsuarioDataModoDivergencia: number;
    descricao: string;
    codUsuarioCad: string;
    dataHoraCad: string;
}

export interface PontoUsuarioDataMotivoDivergencia {
    codPontoUsuarioDataMotivoDivergencia: number;
    descricao: string;
    codUsuarioCad: string;
    dataHoraCad: string;
}

export interface PontoUsuarioDataDivergenciaData extends Meta {
    items: PontoUsuarioDataDivergencia[];
};

export interface PontoUsuarioDataDivergenciaParameters extends QueryStringParameters {
    codPontoUsuarioData?: number;
};
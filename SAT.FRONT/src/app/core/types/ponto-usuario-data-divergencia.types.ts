import { Meta, QueryStringParameters } from "./generic.types";

export class PontoUsuarioDataDivergencia {
    codPontoUsuarioDataDivergencia: number;
    codPontoUsuarioData: number;
    codPontoUsuarioDataModoDivergencia: number;
    dataHoraCad: string;
    codUsuarioCad: string;
    codPontoUsuarioDataMotivoDivergencia: number;
    divergenciaValidada: number;
}

export interface PontoUsuarioDivergenciaData extends Meta {
    items: PontoUsuarioDataDivergencia[];
};

export interface PontoUsuarioDataDivergenciaParameters extends QueryStringParameters {
    
};
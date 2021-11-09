import { Meta, QueryStringParameters } from "./generic.types";

export class PontoUsuarioDataMotivoDivergencia {
    codPontoUsuarioDataMotivoDivergencia: number;
    descricao: string;
    codUsuarioCad: string;
    dataHoraCad: string;    
}

export interface PontoUsuarioDataMotivoDivergenciaData extends Meta {
    items: PontoUsuarioDataMotivoDivergencia[];
};

export interface PontoUsuarioDataMotivoDivergenciaParameters extends QueryStringParameters {
    
};
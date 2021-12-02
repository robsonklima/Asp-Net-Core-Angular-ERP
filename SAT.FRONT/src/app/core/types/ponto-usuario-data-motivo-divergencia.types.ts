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

export const pontoUsuarioDataMotivoDivergenciaConst = {
    FALTA_MARCACAO: 1,
    OUTROS: 2,
    AUSENCIA_DE_DISPOSITIVO_MOVEL: 3,
    AUSENCIA_DE_INTERVALO: 4,
    INTERVALO_MAX_2_HORAS_EXCEDIDO: 5,
    INTERVALO_MIN_1_HORA_NAO_REALIZADO: 6,
    HORA_EXTRA_SEM_ACORDO_PREVIO: 7,
    RAT_SEM_PONTO: 8,
    RAT_ANTES_PRIMEIRO_PONTO: 9,
    RAT_APOS_ULTIMO_PONTO: 10,
    INTERVALO_RAT_MENOR_QUE_PONTO: 11,
    INTERVALO_RAT_DIFERENTE_DO_PONTO: 12,
}
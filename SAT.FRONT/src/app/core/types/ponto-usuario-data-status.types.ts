import { Meta, QueryStringParameters } from "./generic.types";

export class PontoUsuarioDataStatus {
    codPontoUsuarioDataStatus: number;
    descricao: string;
    dataHoraCad: string;
    codUsuarioCad: string;
}

export interface PontoUsuarioDataStatusData extends Meta {
    items: PontoUsuarioDataStatus[];
};

export interface PontoUsuarioDataStatusParameters extends QueryStringParameters {
    
};

export const pontoUsuarioDataStatusConst = {
    INCONSISTENTE: 1,
    CONFERIDO: 2,
    AGUARDANDO_CONFERENCIA: 3,
    NENHUM_HORARIO_REGISTRADO: 4
}
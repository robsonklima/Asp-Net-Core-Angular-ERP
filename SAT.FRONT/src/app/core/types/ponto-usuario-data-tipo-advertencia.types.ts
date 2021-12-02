import { Meta, QueryStringParameters } from "./generic.types";

export class PontoUsuarioDataTipoAdvertencia {
    codPontoUsuarioDataTipoAdvertencia: number;
    descricao: string;
    codUsuarioCad: string;
    dataHoraCad: string;
}

export interface PontoUsuarioDataTipoAdvertenciaData extends Meta {
    items: PontoUsuarioDataTipoAdvertencia[];
};

export interface PontoUsuarioDataTipoAdvertenciaParameters extends QueryStringParameters {
    
};

export const pontoUsuarioDataTipoAdvertenciConst = {
    MANUAL: 1,
    AUTOMATICA: 2
}
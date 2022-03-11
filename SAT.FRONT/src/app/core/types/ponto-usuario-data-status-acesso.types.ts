import { Meta, QueryStringParameters } from "./generic.types";

export class PontoUsuarioDataStatusAcesso {
    codPontoUsuarioDataStatusAcesso: number;
    descricao: string;
    dataHoraCad: string;
    codUsuarioCad: string;
}

export interface PontoUsuarioDataStatusAcessoData extends Meta {
    items: PontoUsuarioDataStatusAcesso[];
};

export interface PontoUsuarioDataStatusAcessoParameters extends QueryStringParameters {
    
};

export const pontoUsuarioDataStatusAcessoConst = {
    BLOQUEADO: 1,
    DESBLOQUEADO: 2
}
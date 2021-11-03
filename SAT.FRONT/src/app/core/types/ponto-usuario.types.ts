import { Meta, QueryStringParameters } from "./generic.types";

export class PontoUsuario {
    codPontoUsuario: number;
    codPontoPeriodo: number;
    codUsuario: string;
    dataHoraRegistro: string;
    dataHoraEnvio: string;
    indAprovado: number;
    indRevisado: number;
    indAtivo: number;
    observacao: string;
    codUsuarioCad: string;
    codUsuarioAprov: string;
    codUsuarioManut: string;
    latitude: number;
    longitude: number;
}

export interface PontoUsuarioData extends Meta {
    items: PontoUsuario[];
};

export interface PontoUsuarioParameters extends QueryStringParameters {
    codUsuario?: string;
    dataHoraRegistroInicio: string;
    dataHoraRegistroFim: string;
};
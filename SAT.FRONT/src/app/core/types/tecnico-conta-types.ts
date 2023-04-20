import { Meta, QueryStringParameters } from "./generic.types";

export interface TecnicoConta {
    codTecnicoConta: number;
    codTecnico: number;
    numBanco: string;
    numAgencia: string;
    numConta: string;
    indPadrao: number;
    indAtivo: number;
    codUsuarioCad: string;
    dataHoraCad: string;
    codUsuarioManut: string;
    dataHoraManut: string | null;
}

export interface TecnicoContaData extends Meta {
    items: TecnicoConta[];
};

export interface TecnicoContaParameters extends QueryStringParameters {
    codTecnico?: number;
}
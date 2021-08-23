import { Meta, QueryStringParameters } from "./generic.types";

export class UnidadeFederativa {
    codUF: number;
    siglaUF: string;
    nomeUF: string;
    codPais: number;
    indAtivo: number;
    codUsuarioCad: string;
    dataHoraCad: string;
    codUsuarioManut: string;
    dataHoraManut: string;
}

export interface UnidadeFederativaData extends Meta {
    unidadesFederativas: UnidadeFederativa[];
};

export interface UnidadeFederativaParameters extends QueryStringParameters {
    codUF?: number;
    codPais?: number;
};
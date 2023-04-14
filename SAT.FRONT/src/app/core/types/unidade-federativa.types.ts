import { Meta, QueryStringParameters } from "./generic.types";
import { Pais } from "./pais.types";

export class UnidadeFederativa
{
    codUF: number;
    siglaUF: string;
    nomeUF: string;
    codPais: number;
    pais: Pais;
    indAtivo: number;
    codUsuarioCad: string;
    dataHoraCad: string;
    codUsuarioManut: string;
    dataHoraManut: string;
}

export interface UnidadeFederativaData extends Meta
{
    items: UnidadeFederativa[];
};

export interface UnidadeFederativaParameters extends QueryStringParameters
{
    codUF?: number;
    codPais?: number;
    siglaUF?: string;
};
import { Filial } from "./filial.types";
import { Meta, QueryStringParameters } from "./generic.types";
import { Regiao } from "./regiao.types";
import { UnidadeFederativa } from "./unidade-federativa.types";

export class Cidade
{
    codCidade: number;
    nomeCidade: string;
    latitude: string;
    longitude: string;
    codUF: number;
    unidadeFederativa: UnidadeFederativa;
    codFilial: number;
    filial: Filial;
    codSlAParametroAdicional: number;
    indAtivo: number;
    indConsulta: number;
    indCapital: number;
    codUsuarioCad: string;
    dataHoraCad: string;
    codUsuarioManut: string;
    dataHoraManut: string;
    latitudeMetros: number;
    longitudeMetros: number;
    regiao: Regiao;
    horasRAcesso: number;
    codRegiaoPOS: number;
}

export interface CidadeData extends Meta
{
    items: Cidade[];
};

export interface CidadeParameters extends QueryStringParameters
{
    codCidade?: number;
    indAtivo?: number;
    codUF?: number;
};

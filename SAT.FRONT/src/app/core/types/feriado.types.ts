import { Cidade } from "./cidade.types";
import { Meta, QueryStringParameters } from "./generic.types";
import { Pais } from "./pais.types";
import { UnidadeFederativa } from "./unidade-federativa.types";

export interface Feriado {
    codFeriado: number;
    nomeFeriado: string;
    data?: any;
    qtdeDias?: any;
    codPais: number;
    codUf: number;
    codCidade: number;
    cidade: Cidade;
    unidadeFederativa: UnidadeFederativa;
    pais: Pais;
}

export interface FeriadoData extends Meta {
    feriados: Feriado[];
};

export interface FeriadoParameters extends QueryStringParameters {
    codFeriado?: number;
};
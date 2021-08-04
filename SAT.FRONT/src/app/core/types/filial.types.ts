import { Cidade } from "./cidade.types";
import { Meta, QueryStringParameters } from "./generic.types";

export interface Filial {
    codFilial: number;
    razaoSocial: string;
    nomeFilial: string;
    cidade: Cidade;
}

export interface FilialData extends Meta {
    filiais: Filial[];
};

export interface FilialParameters extends QueryStringParameters {
    codFilial?: number;
    indAtivo?: number;
};

import { Cidade } from "./cidade.types";
import { Meta, QueryStringParameters } from "./generic.types";

export interface Filial {
    codFilial: number;
    razaoSocial: string;
    nomeFilial: string;
    cidade: Cidade;
    endereco: string;
    cep: string;
}

export interface FilialData extends Meta {
    items: Filial[];
};

export interface FilialParameters extends QueryStringParameters {
    codFilial?: number;
    codFiliais?: string;
    indAtivo?: number;
    SiglaUF?: string;
};

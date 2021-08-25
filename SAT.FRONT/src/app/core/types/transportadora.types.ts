import { Cidade } from "./cidade.types";
import { Meta, QueryStringParameters } from "./generic.types";

export class Transportadora {
    codTransportadora: number;
    codCidade?: number;
    cidade?: Cidade;
    razaoSocial: string;
    nomeTransportadora: string;
    cnpj: string;
    endereco: string;
    bairro: string;
    nomeResponsavel?: any;
    indAtivo: number;
    siglaUf: number;
    pais?: any;
    cep: string;
    telefone1: string;
    telefone2: string;
    celular: string;
    fax: string;
    email: string;
    site: string;
    numeroEnd: string;
    complemEnd: string;
}

export interface TransportadoraData extends Meta {
    items: Transportadora[];
};

export interface TransportadoraParameters extends QueryStringParameters {
    codTransportadora?: number;
};
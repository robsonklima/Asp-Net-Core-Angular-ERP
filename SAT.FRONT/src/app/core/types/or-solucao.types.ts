import { Meta, QueryStringParameters } from "./generic.types";

export interface ORSolucao {
    codSolucao?: number;
    descricao: string;
    codSolucaoLab?: number | null;
    indAtivo?: number | null;
}

export interface ORSolucaoData extends Meta {
    items: ORSolucao[];
};

export interface ORSolucaoParameters extends QueryStringParameters {
    codSolucao?: number;
}
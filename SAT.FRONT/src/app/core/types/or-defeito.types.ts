import { Meta, QueryStringParameters } from "./generic.types";

export interface ORDefeito {
    codDefeito: number;
    descricao: string;
    codDefeitoLab: number;
    indAtivo: number;
}

export interface ORDefeitoData extends Meta {
    items: ORDefeito[];
};

export interface ORDefeitoParameters extends QueryStringParameters {
    codDefeito?: number;
}
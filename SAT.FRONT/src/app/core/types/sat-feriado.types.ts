import { Meta, QueryStringParameters } from "./generic.types";

export interface SATFeriado {
    codSATFeriado: number;
    data: string;
    nome: string;
    tipo: string;
    descricao: string;
    uF: string;
    municipio: string;
    dataHoraCad: string;
    codUsuarioCad: string;
}

export interface SATFeriadoData extends Meta {
    items: SATFeriado[];
};

export interface SATFeriadoParameters extends QueryStringParameters {
    municipio: string;
    tipo: string;
    uF: string;
    mes: number | null;
}
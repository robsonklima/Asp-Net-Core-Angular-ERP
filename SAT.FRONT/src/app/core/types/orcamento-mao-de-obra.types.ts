import { Meta, QueryStringParameters } from "./generic.types";

export interface OrcamentoMaoDeObra
{
    codOrcMaoObra?: number;
    codOrc: number;
    previsaoHoras?: number;
    valorHoraTecnica: number;
    valorTotal?: number;
    redutor?: number;
    dataCadastro: string;
    usuarioCadastro: string;
}

export interface OrcamentoMaoDeObraParameters extends QueryStringParameters {
    codOrc: number | null;
}

export interface OrcamentoMaoDeObraData extends Meta
{
    items: OrcamentoMaoDeObra[];
};
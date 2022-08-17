import { Meta, QueryStringParameters } from "./generic.types";

export class DespesaConfiguracaoCombustivel
{
    codDespesaConfiguracaoCombustivel: number;
    codFilial: number;
    codUf: number;
    precoLitro: number;
    dataHoraCad: string;
    codUsuarioCad: string;
    dataHoraManut: string;
    codUsuarioManut: string;
}

export interface DespesaConfiguracaoCombustivelData extends Meta
{
    items: DespesaConfiguracaoCombustivel[]
};

export interface DespesaConfiguracaoCombustivelParameters extends QueryStringParameters
{
    codDespesaConfiguracaoCombustivel: number;
    codFilial?: number;
    codUf?: number;
};
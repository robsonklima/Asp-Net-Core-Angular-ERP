import { Filial } from "./filial.types";
import { Meta, QueryStringParameters } from "./generic.types";
import { UnidadeFederativa } from "./unidade-federativa.types";
import { Usuario } from "./usuario.types";

export class DespesaConfiguracaoCombustivel
{
    codDespesaConfiguracaoCombustivel: number;
    codFilial: number;
    //filial?: Filial;
    codUf: number;
   // unidadeFederativa?: UnidadeFederativa;
    precoLitro: number;
    dataHoraCad?: string;
    codUsuarioCad?: string;
    dataHoraManut?: string;
    codUsuarioManut?: string;
    //usuario?: Usuario;
}

export interface DespesaConfiguracaoCombustivelData extends Meta
{
    items: DespesaConfiguracaoCombustivel[]
};

export interface DespesaConfiguracaoCombustivelParameters extends QueryStringParameters
{
    //codDespesaConfiguracaoCombustivel: number;
    codFilial?: number;
    codUf?: number;
};
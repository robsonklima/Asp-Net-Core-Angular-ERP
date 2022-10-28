import { Meta, QueryStringParameters } from "./generic.types";

export class TipoServico
{
    codServico: number;
    nomeServico: string;
    codETipoServico: string;
    valServico?: any;
    indValHora?: any;
    valPrimHora?: any;
    valSegHora?: any;
    indAtivo?: any;
    codTraducao: number;
}

export interface TipoServicoData extends Meta
{
    items: TipoServico[];
};

export interface TipoServicoParameters extends QueryStringParameters
{
    codServico?: number;
    indAtivo?: number;
};

export enum TipoServicoEnum
{
    ATIVACAO = 26,
    HORA_TECNICA = 47,
    KM_RODADO = 48,
    HORA_DE_VIAGEM = 49,
    MANUTENCAO_MENSAL = 50
}
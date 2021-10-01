import { QueryStringParameters } from "./generic.types";

export interface Indicador {
    label: string;
    valor: number;
    filho?: Indicador[]
}

export interface IndicadorParameters extends QueryStringParameters {
    agrupador?: IndicadorAgrupadorEnum;
    tipo?: IndicadorTipoEnum;
    codFiliais?: string;
    codClientes?: string;
    dataInicio?: string;
    dataFim?: string;
    codTiposIntervencao?: string;
    codAutorizadas?: string;
    codTiposGrupo?: string;
};

export enum IndicadorAgrupadorEnum
{
    CLIENTE,
    FILIAL,
    STATUS_SERVICO,
    TIPO_INTERVENCAO,
    DATA
}
export enum IndicadorTipoEnum
{
    ORDEM_SERVICO,
    SLA,
    SPA,
    PENDENCIA,
    REINCIDENCIA
}
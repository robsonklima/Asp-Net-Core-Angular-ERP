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

export enum IndicadorAgrupadorEnum {
    CLIENTE,
    FILIAL,
    STATUS_SERVICO,
    TIPO_INTERVENCAO,
    DATA,
    TECNICO_PERCENT_REINCIDENTES,
    TECNICO_QNT_CHAMADOS_REINCIDENTES,
    EQUIPAMENTO_PERCENT_REINCIDENTES, 
    TECNICO_PERCENT_SPA,
    TECNICO_QNT_CHAMADOS_SPA,
    TECNICO_PERCENT_PENDENTES,
    TECNICO_QNT_CHAMADOS_PENDENTES,
    TOP_PECAS_FALTANTES,
    ESTOQUE_PECA_FILIAIS
}
export enum IndicadorTipoEnum {
    ORDEM_SERVICO,
    SLA,
    SPA,
    PENDENCIA,
    REINCIDENCIA,
    PECA_FALTANTE
}
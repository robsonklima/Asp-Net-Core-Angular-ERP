import { QueryStringParameters } from "./generic.types";
import { OrdemServicoFilterEnum, OrdemServicoIncludeEnum } from "./ordem-servico.types";


export class DadosPeca {
    codMagnus: string;
    descricao: string;
    index: number;
    quantidade: number;
    chamadosPeca: ChamadosPeca[];
}

export class ChamadosPeca {
    filial: string;
    ordemServico: string;
    dataSolucao: string;
    cliente: string;
    equipamento: string;
}

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
    include?: OrdemServicoIncludeEnum;
    filterType?: OrdemServicoFilterEnum;
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
    NOVAS_CADASTRADAS,
    NOVAS_LIBERADAS,
    REGIAO,
    TOP_CINCO_PECAS_MAIS_FALTANTES
}
export enum IndicadorTipoEnum {
    ORDEM_SERVICO,
    SLA,
    SPA,
    PENDENCIA,
    REINCIDENCIA,
    PECA_FALTANTE,
    DISPONIBILIDADE,
    PERFORMANCE_FILIAIS
}
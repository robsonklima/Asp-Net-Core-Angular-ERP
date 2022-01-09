import { Meta, QueryStringParameters } from "./generic.types";

export class Monitoramento {
    codLogAlerta: number;
    servidor: string;
    item: string;
    mensagem: string;
    tipo: string;
    emUso: number;
    total: number;
    disco: string;
    status: string;
    dataHoraProcessamento: string;
    dataHoraCad: string;
}

export interface MonitoramentoData extends Meta {
    items: Monitoramento[];
};

export interface MonitoramentoParameters extends QueryStringParameters {
    tipo?: string;
    servidor?: string;
    item?: string;
    dataHoraProcessamentoInicio?: string;
    dataHoraProcessamentoFim?: string;
};

export const monitoramentoTipoConst = {
    SERVICO: "SERVICO",
    INTEGRACAO: 'INTEGRACAO',
    STORAGE: 'STORAGE',
    MEMORY: 'MEMORY',
    CPU: 'CPU',
    CHAMADO: 'CHAMADO'
}

export const monitoramentoStatusConst = {
    DANGER: "DANGER",
    WARNING: "WARNING",
    OK: "OK"
}
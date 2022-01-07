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
};

export enum MonitoramentoTipoEnum {
    SERVICO = "SERVICO",
    INTEGRACAO = 'INTEGRACAO',
    STORAGE = 'STORAGE',
    MEMORY = 'MEMORY',
    CPU = 'CPU',
    CHAMADO = 'CHAMADO'
}
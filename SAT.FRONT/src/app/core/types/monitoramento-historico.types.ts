import { Meta, QueryStringParameters } from "./generic.types";

export class MonitoramentoHistorico {
    codHistLogAlerta: number;
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

export interface MonitoramentoHistoricoData extends Meta {
    items: MonitoramentoHistorico[];
};
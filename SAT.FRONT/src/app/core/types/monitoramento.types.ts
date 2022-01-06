import { Meta, QueryStringParameters } from "./generic.types";

export class Monitoramento {
    codLogAlerta: number;
    servidor: string;
    item: string;
    mensagem: string;
    tipo: string;
    espacoEmGb: number;
    tamanhoEmGb: number;
    disco: string;
    dataHoraProcessamento: string;
    dataHoraCad: string;
}

export interface MonitoramentoData extends Meta {
    items: Monitoramento[];
};

export interface MonitoramentoParameters extends QueryStringParameters {

};

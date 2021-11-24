import { Meta, QueryStringParameters } from "./generic.types";

export class ContratoEquipamentoData {
    codContratoEquipData: number;
    nomeData: string;
    descData: string;
    indEntrega: number;
    indInstalacao: number;
    indGarantia: number;
    indAtivo: string;
}

export interface ContratoEquipamentoDataData extends Meta {
    items: ContratoEquipamentoData[];
};

export interface ContratoEquipamentoDataParameters extends QueryStringParameters {
    nomeData?: string;
    indAtivo?: number;
};

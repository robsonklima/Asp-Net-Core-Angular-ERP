import { Meta, QueryStringParameters } from "./generic.types";

export interface MotivoCancelamento {
    codMotivoCancelamento: number;
    motivo: string;
    geraNotaServico: boolean;
    ativo: boolean;
    dataAlteracao: string;
    pos: boolean;
}

export interface MotivoCancelamentoData extends Meta {
    items: MotivoCancelamento[];
};

export interface MotivoCancelamentoParameters extends QueryStringParameters {
    
};
import { Meta, QueryStringParameters } from "./generic.types";

export interface MotivoComunicacao {
    codMotivoComunicacao: number;
    motivo: string;
    ativo: boolean;
    dataAlteracao: string;
}

export interface MotivoComunicacaoData extends Meta {
    items: MotivoComunicacao[];
};

export interface MotivoComunicacaoParameters extends QueryStringParameters {
    
};
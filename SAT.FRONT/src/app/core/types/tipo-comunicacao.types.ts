import { Meta, QueryStringParameters } from "./generic.types";

export interface TipoComunicacao {
    codTipoComunicacao: number;
    tipo: string;
    ativo: boolean;
    dataAlteracao: string;
}

export interface TipoComunicacaoData extends Meta {
    items: TipoComunicacao[];
};

export interface TipoComunicacaoParameters extends QueryStringParameters {
    
};

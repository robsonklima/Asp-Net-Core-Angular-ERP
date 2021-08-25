import { Meta, QueryStringParameters } from "./generic.types";

export class DespesaCartaoCombustivel {
    codDespesaCartaoCombustivel: number;
    numero: string;
    carro: string;
    placa: string;
    ano: string;
    cor: string;
    combustivel: string;
    codUsuarioCad?: string;
    codUsuarioManut?: string;
    dataHoraCad: Date;
    indAtivo: number;
}

export interface DespesaCartaoCombustivelData extends Meta {
    items: DespesaCartaoCombustivel[]
};

export interface DespesaCartaoCombustivelParameters extends QueryStringParameters {
    codDespesaCartaoCombustivel?: number;
    indAtivo?: number;
};
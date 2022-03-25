import { Meta, QueryStringParameters } from "./generic.types";
import { Peca } from "./peca.types";

export class ClientePecaGenerica {
    codClientePecaGenerica: number;
    codPeca: number;
    valorUnitario: number;
    valorIPI: number;
    vlrSubstituicaoNovo: number;
    vlrBaseTroca: number;
    codUsuarioCad: string;
    dataHoraCad: string;
    codUsuarioManut: string;
    dataHoraManut: string;
    peca: Peca;
}

export interface ClientePecaGenericaData extends Meta {
    items: ClientePecaGenerica[];
};

export interface ClientePecaGenericaParameters extends QueryStringParameters {
    codClientePecaGenerica?: number;
    codMagnus?: string;
};
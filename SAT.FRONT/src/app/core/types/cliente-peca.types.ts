import { Cliente } from "./cliente.types";
import { Contrato } from "./contrato.types";
import { Meta, QueryStringParameters } from "./generic.types";
import { Peca } from "./peca.types";

export class ClientePeca {
    codClientePeca: number;
    codCliente: number;
    codContrato: number;
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
    cliente: Cliente;
    contrato: Contrato;
}

export interface ClientePecaData extends Meta {
    items: ClientePeca[];
};

export interface ClientePecaParameters extends QueryStringParameters {
    codClientePeca?: number;
    codMagnus?: string;
    codPeca?: number;
    codCliente?: number;
    codContrato?: number;
};
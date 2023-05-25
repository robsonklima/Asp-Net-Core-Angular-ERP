import { Meta } from "@angular/platform-browser";
import { QueryStringParameters } from "./generic.types";

export interface MRPLogix {
    codMRPLogix: number;
    numPedido: string;
    dataPedido: string | null;
    nomecliente: string;
    codItem: string;
    nomeItem: string;
    codCliente: string;
    qtdPedido: number;
    qtdSolicitada: number;
    localProd: string;
    qtdCancelada: number;
    preco: number;
    qtdAtendida: number;
    prazoEntrega: string | null;
    diasPedido: number;
    codEmpresa: number;
    localEstoque: string;
    numSequencia: number;
    iPI: number;
    codUsuario: string;
    tipo: string;
    saldoTotal: number;
    numSequenciaPed: number;
    saldo: number;
}

export interface MRPLogixData extends Meta
{
    items: MRPLogix[];
};

export interface MRPLogixParameters extends QueryStringParameters
{
    codMRPLogix?: number;
};
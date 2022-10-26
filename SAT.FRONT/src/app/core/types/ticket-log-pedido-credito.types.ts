import { Meta, QueryStringParameters } from "./generic.types";

export interface TicketLogPedidoCredito {
    codTicketLogPedidoCredito: number;
    codDespesaPeriodoTecnico: number | null;
    valor: number | null;
    numeroCartao: string;
    indProcessado: number;
    dataHoraProcessamento: string | null;
    observacao: string;
    codUsuarioCad: string;
    dataHoraCad: string | null;
}

export interface TicketLogPedidoCreditoData extends Meta
{
    items: TicketLogPedidoCredito[];
};

export interface TicketLogPedidoCreditoParameters extends QueryStringParameters {
    codDespesaPeriodoTecnico?: number | null;
}
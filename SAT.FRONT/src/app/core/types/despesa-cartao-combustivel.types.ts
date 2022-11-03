import { Meta, QueryStringParameters } from "./generic.types";
import { Tecnico } from "./tecnico.types";
import { TicketLogTransacao } from "./ticket-log-transacao.types";
import { TicketLogUsuarioCartaoPlaca } from "./ticketlog-types";

export class DespesaCartaoCombustivel
{
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
    ticketLogUsuarioCartaoPlaca: TicketLogUsuarioCartaoPlaca;
    transacoes: TicketLogTransacao[];
}

export interface DespesaCartaoCombustivelData extends Meta
{
    items: DespesaCartaoCombustivel[]
};

export interface DespesaCartaoCombustivelParameters extends QueryStringParameters
{
    codDespesaCartaoCombustivel?: number;
    indAtivo?: number;
};

export class DespesaCartaoCombustivelTecnico
{
    codDespesaCartaoCombustivelTecnico?: number;
    codDespesaCartaoCombustivel: number;
    codTecnico: number;
    tecnico?: Tecnico;
    dataHoraInicio?: string;
    codUsuarioCad?: string;
    dataHoraCad?: string;
    despesaCartaoCombustivel?: DespesaCartaoCombustivel;
}

export interface DespesaCartaoCombustivelTecnicoData extends Meta
{
    items: DespesaCartaoCombustivelTecnico[]
};

export interface DespesaCartaoCombustivelTecnicoParameters extends QueryStringParameters
{
    codDespesaCartaoCombustivel?: number;
};
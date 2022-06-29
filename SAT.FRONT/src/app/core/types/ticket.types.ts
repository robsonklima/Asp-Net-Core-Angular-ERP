import { Meta, QueryStringParameters } from "./generic.types";

export interface TicketData extends Meta {
    items: Ticket[];
};

export interface TicketParameters extends QueryStringParameters {
    codUsuario?: string;
};

export class Ticket {
    codTicket: number;
    codUsuario: string;
    codModulo: number;
    titulo: string;
    descricao: string;
    codClassificacao: number;
    codStatus: number;
    dataCadastro: string;
    usuarioManut: string;
    dataManut: string;
    dataFechamento: string;
    codPrioridade: number;
}
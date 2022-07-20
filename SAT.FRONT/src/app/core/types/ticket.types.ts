import { Meta, QueryStringParameters } from "./generic.types";
import { Usuario } from "./usuario.types";

export interface TicketData extends Meta {
    items: Ticket[];
};

export interface TicketAtendimentoData extends Meta {
    items: TicketAtendimento[];
};

export interface TicketModuloData extends Meta {
	items: TicketModulo[];
     };

export interface TicketStatusData extends Meta {
    items: TicketStatus[];
   
};

export interface TicketClassificacaoData extends Meta {
    items: TicketClassificacao[];

    
};

export interface TicketPrioridadeData extends Meta {
    items: TicketPrioridade[];
    
};

export interface TicketParameters extends QueryStringParameters {
    codUsuario?: string;
};

export interface TicketAtendimentoParameters extends QueryStringParameters {
    usuarioAtend?: string;
    codTicket?: number;
};

export interface TicketModuloParameters extends QueryStringParameters{
    codModulo: number;
};

export interface TicketStatusParameters extends QueryStringParameters{
    codStatus: number;
};

export interface TicketClassificacaoParameters extends QueryStringParameters{
    codClassificacao: number;
};

export interface TicketPrioridadeParameters extends QueryStringParameters{
    codClassificacao: number;
};

export class Ticket {
    codTicket: number;
    codUsuario?: string;
    codModulo?: number;
    titulo?: string;
    descricao?: string;
    codClassificacao?: number;
    codStatus?: number;
    dataCadastro?: string;
    usuarioManut?: string;
    dataManut?: Date;
    dataFechamento?: Date;
    codPrioridade?: number;
    ticketModulo?: TicketModulo;
    ticketClassificacao?: TicketClassificacao;
    ticketPrioridade?: TicketPrioridade;
    ticketStatus?: TicketStatus;
    usuario?: Usuario;
    
}

export class TicketModulo {
    codModulo: number;
    descricao: string;
}
export class TicketClassificacao {
    codClassificacao: number;
    descricao: string;
	items: TicketClassificacao[];
}
export class TicketPrioridade {
    codPrioridade: number;
    descricao: string;
	items: TicketPrioridade[];
}
export class TicketStatus {
    codStatus: number;
    descricao: string;
}
export class TicketAtendimento {
    codTicketAtendimento: number;
    codTicket: number;
    descricao: string;
    codStatus: number;
    usuarioAtend: number;
    dataCadastro: string;
    ticketStatus: TicketStatus;
    usuario: Usuario;
}
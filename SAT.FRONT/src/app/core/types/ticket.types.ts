import { Meta, QueryStringParameters } from "./generic.types";
import { TicketAnexo } from "./ticket-anexo.types";
import { Usuario } from "./usuario.types";

export interface Ticket {
    codTicket?: number;
    codModulo: number;
    ticketModulo: TicketModulo;
    titulo: string;
    descricao: string;
    codPrioridade: number;
    ticketPrioridade: TicketPrioridade;
    codClassificacao: number;
    ticketClassificacao: TicketClassificacao;
    codStatus: number;
    ticketStatus: TicketStatus;
    codUsuarioCad: string;
    usuarioCad: Usuario;
    dataHoraCad: string;
    codUsuarioManut: string;
    usuarioManut: Usuario;
    dataHoraManut: string | null;
    dataFechamento: string | null;
    ordem: number;
    indAtivo: number;
    atendimentos?: TicketAtendimento[];
    anexos?: TicketAnexo[];
    codUsuarioAtendente?: string;
    usuarioAtendente?: Usuario;
}

export interface TicketAtendimento {
    codTicketAtend?: number;
    codTicket: number;
    descricao: string;
    codStatus: number;
    ticketStatus?: TicketStatus;
    codUsuarioCad: string;
    usuarioCad?: Usuario;
    dataHoraCad: string;
    codUsuarioManut?: string;
    usuarioManut?: Usuario;
    dataHoraManut?: string;
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
    codUsuarioCad?: string;
    codModulo?: number;
    codStatus?: number;
    codPrioridade?: number;
    codClassificacao?: number;
}

export interface TicketAtendimentoParameters extends QueryStringParameters {
    codUsuarioAtend?: string;
    codTicket?: number;
}

export interface TicketModuloParameters extends QueryStringParameters {
    codModulo?: number;
};

export interface TicketStatusParameters extends QueryStringParameters {
    codStatus?: number;
};

export interface TicketClassificacaoParameters extends QueryStringParameters {
    codClassificacao?: number;
};

export interface TicketPrioridadeParameters extends QueryStringParameters {
    codClassificacao?: number;
};

export const ticketStatusConst = {
    EM_ATENDIMENTO: 1,
    CANCELADO: 2,
    CONCLUIDO: 3,
    AGUARDANDO: 4
}
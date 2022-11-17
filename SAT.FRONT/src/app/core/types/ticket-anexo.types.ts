import { Meta, QueryStringParameters } from "./generic.types";
import { Usuario } from "./usuario.types";

export interface TicketAnexo {
    codTicketAnexo?: number;
    codTicket: number;
    nome: string;
    indAtivo: number;
    codUsuarioCad: string;
    base64: string;
    tamanho: number;
    tipo: string;
    usuarioCad?: Usuario;
    dataHoraCad: string;
}

export interface TicketAnexoData extends Meta {
    items: TicketAnexo[];
};

export interface TicketAnexoParameters extends QueryStringParameters {
    
};
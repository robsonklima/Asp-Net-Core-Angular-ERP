import { Meta, QueryStringParameters } from "./generic.types";
import { Usuario } from "./usuario.types";

export interface TicketAnexo {
    codTicketAnexo: number;
    codTicket: number;
    nome: string;
    indAtivo: number;
    codUsuarioCad: string;
    usuarioCad: Usuario;
    dataHoraCad: string;
    base64: string;
}

export interface TicketAnexoData extends Meta {
    items: TicketAnexo[];
};

export interface TicketAnexoParameters extends QueryStringParameters {
    
};
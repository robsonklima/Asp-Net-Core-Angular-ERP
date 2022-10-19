import { Meta, QueryStringParameters } from "./generic.types";
import { Usuario } from "./usuario.types";

export interface Mensagem {
    codMsg: number;
    conteudo: string;
    codUsuarioRemetente: string;
    usuarioRemetente: Usuario;
    codUsuarioDestinatario: string;
    usuarioDestinatario: Usuario;
    dataHoraCad: string;
    indLeitura: number | null;
    dataHoraLeitura: string;
}

export interface MensagemData extends Meta {
    items: Mensagem[];
};

export interface MensagemParameters extends QueryStringParameters {
};
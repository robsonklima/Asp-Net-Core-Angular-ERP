import { Meta, QueryStringParameters } from "./generic.types";
import { Usuario } from "./usuario.types";

export interface ConferenciaParticipante {
    codConferenciaParticipante: number;
    codConferencia: number;
    codUsuarioParticipante: string;
    usuarioParticipante: Usuario;
    codUsuarioCad: string;
    usuarioCadastro: Usuario;
    dataHoraCad: string | null;
}

export interface ConferenciaParticipanteParameters extends QueryStringParameters {

}

export interface ConferenciaParticipanteData extends Meta {
    items: ConferenciaParticipante[];
}
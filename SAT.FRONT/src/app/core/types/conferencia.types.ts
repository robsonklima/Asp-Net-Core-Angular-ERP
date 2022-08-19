import { Meta, QueryStringParameters } from "./generic.types";
import { Usuario } from "./usuario.types";

export interface Conferencia {
    codConferencia: number;
    link: string;
    codUsuarioCad: string;
    usuarioCadastro: Usuario;
    dataHoraCad: string;
    codUsuarioManut: string;
    usuarioManut: Usuario;
    dataHoraManut: string | null;
    indAtivo: number;
}

export interface ConferenciaParameters extends QueryStringParameters {

}

export interface ConferenciaData extends Meta {
    items: Conferencia[];
};

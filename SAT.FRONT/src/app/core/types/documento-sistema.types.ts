import { Meta, QueryStringParameters } from "./generic.types";
import { Usuario } from "./usuario.types";

export interface DocumentoSistema {
    codDocumentoSistema: number;
    titulo: string;
    conteudo: string;
    indAtivo: number | null;
    categoria: string;
    usuarioCad: Usuario;
    usuarioManut: Usuario;
    dataHoraCad: string;
    dataHoraManut: string;
}

export interface DocumentoSistemaData extends Meta {
    items: DocumentoSistema[]
};

export interface DocumentoSistemaParameters extends QueryStringParameters {

};
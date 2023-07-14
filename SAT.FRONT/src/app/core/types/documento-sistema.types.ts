import { Meta, QueryStringParameters } from "./generic.types";

export interface DocumentoSistema {
    codDocumentoSistema: number;
    titulo: string;
    conteudo: string;
    indAtivo: number | null;
    categoria: string;
}

export interface DocumentoSistemaData extends Meta {
    items: DocumentoSistema[]
};

export interface DocumentoSistemaParameters extends QueryStringParameters {

};
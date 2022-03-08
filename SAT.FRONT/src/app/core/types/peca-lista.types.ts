import { Meta, QueryStringParameters } from "./generic.types";

export class PecaLista {
    codPecaLista: number;
    nomePecaLista: string;
    descPecaLista: string;
    codUsuarioCad: string;
    dataHoraCad: string;
    codUsuarioManut: string;
    dataHoraManut?: string;
}

export interface PecaListaData extends Meta {
    items: PecaLista[];
};

export interface PecaListaParameters extends QueryStringParameters {
};
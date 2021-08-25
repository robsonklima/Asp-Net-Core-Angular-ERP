import { Meta, QueryStringParameters } from "./generic.types";

export class Pais {
    codPais: number;
    siglaPais: string;
    nomePais: string;
    indAtivo: number;
    codUsuarioCad?: string;
    dataHoraCad?: string;
    codUsuarioManut?: string;
    dataHoraManut?: string;
}

export interface PaisData extends Meta {
    items: Pais[];
};

export interface PaisParameters extends QueryStringParameters {
    codPais?: number;
};
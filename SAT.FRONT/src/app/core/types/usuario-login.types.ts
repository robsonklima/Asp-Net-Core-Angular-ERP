import { Meta, QueryStringParameters } from "./generic.types";

export interface UsuarioLogin {
    codUsuarioLogin: number;
    codUsuario: string;
    dataHoraCad: string;
}

export interface UsuarioLoginData extends Meta {
    items: UsuarioLogin[];
};

export interface UsuarioLoginParameters extends QueryStringParameters {
    dataHoraCadInicio?: string;
    dataHoraCadFim?: string;
};
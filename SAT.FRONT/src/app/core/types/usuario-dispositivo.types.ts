import { Meta, QueryStringParameters } from "./generic.types";

export class UsuarioDispositivo {
    codUsuarioDispositivo?: number;
    sistemaOperacional: string;
    versaoSO: string;
    navegador: string;
    versaoNavegador: string;
    tipoDispositivo: string;
    codUsuario: string;
    dataHoraCad: string;
    indAtivo: number;
}

export interface UsuarioDispositivoData extends Meta {
    items: UsuarioDispositivo[];
};

export interface UsuarioDispositivoParameters extends QueryStringParameters {
    codUsuario?: string;
    sistemaOperacional?: string;
    versaoSO?: string;
    navegador?: string;
    versaoNavegador?: string;
    tipoDispositivo?: string;
    indAtivo?: number;
};
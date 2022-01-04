import { Meta, QueryStringParameters } from "./generic.types";

export class Perfil
{
    codPerfil: number;
    nomePerfil: string;
    descPerfil: string;
    indResumo?: number;
    codSistema?: number;
    indAbreChamado?: number;
}

export interface PerfilData extends Meta
{
    items: Perfil[];
};

export interface PerfilParameters extends QueryStringParameters
{
    codPerfil?: number;
};

export const perfilConst = {
    'FILIAL_TECNICO_DE_CAMPO': 35
}
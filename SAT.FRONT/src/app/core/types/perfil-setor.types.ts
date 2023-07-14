import { Meta, QueryStringParameters } from "./generic.types";
import { Perfil } from "./perfil.types";
import { Setor } from "./setor.types";

export class PerfilSetor
{
    codPerfilSetor?: number;
    codPerfil: number;
    codSetor: number;
    indAtivo: number;
    setor?: Setor;
    perfil?: Perfil;
}

export interface PerfilSetorData extends Meta
{
    items: PerfilSetor[];
};

export interface PerfilSetorParameters extends QueryStringParameters
{
    codPerfilSetor?: number;
    codPerfil?: number;
    codSetor?: number;
    codSetores?: string;
};


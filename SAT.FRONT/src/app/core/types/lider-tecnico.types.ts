import { Meta, QueryStringParameters } from "./generic.types";
import { Tecnico } from "./tecnico.types";
import { Usuario } from "./usuario.types";

export class LiderTecnico {
    codLiderTecnico: number;
    codUsuarioLider: string;
    codTecnico: number;
    codUsuarioCad: string;
    usuarioLider: Usuario;
    tecnico: Tecnico;
}

export interface LiderTecnicoData extends Meta {
    items: LiderTecnico[];
};

export interface LiderTecnicoParameters extends QueryStringParameters {
};
import { Meta, QueryStringParameters } from "./generic.types";

export interface RecursoBloqueado {
    codRecursoBloqueado?: number;
    codPerfil: number;
    codSetor: number;
    claims: string;
    url: string;
    indAtivo: number;
}

export interface RecursoBloqueadoData extends Meta {
    items: RecursoBloqueado[];
};

export interface RecursoBloqueadoParameters extends QueryStringParameters { 
    indAtivo?: number;
    codPerfil?: number;
    codSetor?: number;
};
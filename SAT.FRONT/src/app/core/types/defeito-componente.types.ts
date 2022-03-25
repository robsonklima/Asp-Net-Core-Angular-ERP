import { Defeito } from "./defeito.types";
import { Causa } from "./causa.types";
import { Meta, QueryStringParameters } from "./generic.types";

export class DefeitoComponente {
    codDefeitoComponente: number;
    codECausa: string;
    codDefeito: number;
    selecionado?: number;
    codUsuarioCad: string;
    dataHoraCadastro?: string;
    causa: Causa;
    defeito: Defeito;
}

export interface DefeitoComponenteData extends Meta {
    items: DefeitoComponente[]
};

export interface DefeitoComponenteParameters extends QueryStringParameters {
    codECausa?: string;
};
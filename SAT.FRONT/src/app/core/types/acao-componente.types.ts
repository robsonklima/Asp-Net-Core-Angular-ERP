import { Acao } from "./acao.types";
import { Causa } from "./causa.types";
import { Meta, QueryStringParameters } from "./generic.types";

export class AcaoComponente {
    codAcaoComponente: number;
    codECausa: string;
    codAcao: number;
    selecionado?: number;
    codUsuarioCad: string;
    dataHoraCadastro?: string;
    causa: Causa;
    acao: Acao;
}

export interface AcaoComponenteData extends Meta {
    items: AcaoComponente[]
};

export interface AcaoComponenteParameters extends QueryStringParameters {
    codECausa?: string;
};
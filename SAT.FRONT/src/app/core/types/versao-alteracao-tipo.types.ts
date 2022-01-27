import { Meta, QueryStringParameters } from "./generic.types";

export class VersaoAlteracaoTipo {
    codVersaoAlteracaoTipo: number;
    nome: string;
    dataHoraCad: string;
}

export interface VersaoAlteracaoTipoData extends Meta {
    items: VersaoAlteracaoTipo[];
};

export interface VersaoAlteracaoTipoParameters extends QueryStringParameters {
    
};
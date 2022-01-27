import { Meta, QueryStringParameters } from "./generic.types";
import { VersaoAlteracaoTipo } from "./versao-alteracao-tipo.types";

export class VersaoAlteracao {
    codSatVersaoAlteracao: number;
    codSatVersao: number;
    codSatVersaoAlteracaoTipo: number;
    nome: string;
    versaoAlteracaoTipo: VersaoAlteracaoTipo;
    dataHoraCad: string;
}

export interface VersaoAlteracaoData extends Meta {
    items: VersaoAlteracao[];
};

export interface VersaoAlteracaoParameters extends QueryStringParameters {
    
};
import { Meta, QueryStringParameters } from "./generic.types";
import { VersaoAlteracao } from "./versao-alteracao.types";

export class Versao {
    codSatVersao: number;
    nome: string;
    alteracoes: VersaoAlteracao[];
    dataHoraCad: string;
}

export interface VersaoData extends Meta {
    items: Versao[];
};

export interface VersaoParameters extends QueryStringParameters {
    
};
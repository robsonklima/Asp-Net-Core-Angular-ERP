import { Meta, QueryStringParameters } from "./generic.types";
import { TipoIndiceReajuste } from "./tipo-indice-reajuste.types";

export class ContratoReajuste {
    codContratoReajuste: number;
    codContrato: number;
    codTipoIndiceReajuste: number;
    tipoIndiceReajuste: TipoIndiceReajuste;
    percReajuste: number;        
    indAtivo: number;
    codUsuarioCad: string;
    dataHoraCad: Date;  
}

export interface ContratoReajusteData extends Meta {
    items: ContratoReajuste[];
};

export interface ContratoReajusteParameters extends QueryStringParameters {
    codContrato?:number;
    indAtivo?: number;
    codCliente?: number;
    filter?: string;
};
import { AcordoNivelServico } from "./acordo-nivel-servico.types";
import { Contrato } from "./contrato.types";
import { Meta, QueryStringParameters } from "./generic.types";

export class ContratoSLA {
    codContrato: number;
    contrato?: Contrato;
    codSLA: number;
    sla?: AcordoNivelServico;
    indAgendamento?: any;
    codUsuarioCad: string;
    dataHoraCad: string;
}

export interface ContratoSLAData extends Meta {
    items: ContratoSLA[]
};

export interface ContratoSLAParameters extends QueryStringParameters {
    codContrato?: number;
    codSLA?: number;
};
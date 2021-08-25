import { Meta, QueryStringParameters } from "./generic.types";

export class TipoServico {
    codServico: number;
    nomeServico: string;
    codETipoServico: string;
    valServico?: any;
    indValHora?: any;
    valPrimHora?: any;
    valSegHora?: any;
    indAtivo?: any;
    codTraducao: number;
}

export interface TipoServicoData extends Meta {
    items: TipoServico[];
};

export interface TipoServicoParameters extends QueryStringParameters {
    codServico?: number;
    indAtivo?: number;
};
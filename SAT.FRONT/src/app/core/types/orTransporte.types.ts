import { Meta, QueryStringParameters } from "./generic.types";

export interface ORTransporte {
    codTransportadora: number;
    nomeTransportadora: string;
    indAtivo: number;
    codModal: number;
    
}

export interface ORTransporteData extends Meta {
    items: ORTransporte[]
};

export interface ORTransporteParameters extends QueryStringParameters {
    codTransportadora?: number;
}
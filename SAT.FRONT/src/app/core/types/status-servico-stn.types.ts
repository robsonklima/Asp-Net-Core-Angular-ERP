import { Meta, QueryStringParameters } from "./generic.types";

export interface StatusServicoSTN {
    codStatusServicoSTN: number;
    descStatusServicoSTN: string;
    indAtivo: number;
}

export interface StatusServicoSTNData extends Meta {
    items: StatusServicoSTN[];
};

export interface StatusServicoSTNParameters extends QueryStringParameters {
    
};
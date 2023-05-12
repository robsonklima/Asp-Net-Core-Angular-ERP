import { Meta, QueryStringParameters } from "./generic.types";

export interface OperadoraTelefonia {
    codOperadoraTelefonia: number;
    nomeOperadoraTelefonia: string;
    indAtivo: boolean;
}

export interface OperadoraTelefoniaData extends Meta {
    items: OperadoraTelefonia[];
};

export interface OperadoraTelefoniaParameters extends QueryStringParameters {
    
};
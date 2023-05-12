import { Meta, QueryStringParameters } from "./generic.types";

export interface DefeitoPOS {
    codDefeitoPOS: number;
    nomeDefeitoPOS: string;
    dataCadastro: string;
    codUsuarioCadastro: string;
    ativo: boolean;
    codDefeito: number;
    exigeTrocaEquipamento: boolean;
}

export interface DefeitoPOSData extends Meta {
    items: DefeitoPOS[];
};

export interface DefeitoPOSParameters extends QueryStringParameters {
    
};
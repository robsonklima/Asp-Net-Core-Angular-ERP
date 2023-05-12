import { Meta, QueryStringParameters } from "./generic.types";

export interface RedeBanrisul {
    codRedeBanrisul: number;
    rede: string;
    ativo: boolean;
    faturaProduto: boolean | null;
    faturaServico: boolean | null;
}

export interface RedeBanrisulData extends Meta {
    items: RedeBanrisul[];
};

export interface RedeBanrisulParameters extends QueryStringParameters {

};
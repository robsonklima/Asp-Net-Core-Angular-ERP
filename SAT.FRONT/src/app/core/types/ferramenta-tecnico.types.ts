import { Meta, QueryStringParameters } from "./generic.types";

export class FerramentaTecnico {
    codFerramentaTecnico: number;
    nome: string;
    status: number;
}

export interface FerramentaTecnicoData extends Meta {
    items: FerramentaTecnico[];
};

export interface FerramentaTecnicoParameters extends QueryStringParameters {
    indAtivo?: number;
};
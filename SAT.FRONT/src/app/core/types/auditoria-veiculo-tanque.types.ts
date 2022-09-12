import { Meta, QueryStringParameters } from "./generic.types";

export class AuditoriaVeiculoTanque {
    codAuditoriaVeiculoTanque : number;
    nome: string;
    qtdLitros: number;
}

export interface AuditoriaVeiculoTanqueData extends Meta {
    items: AuditoriaVeiculoTanque[]
};

export interface AuditoriaVeiculoTanqueParameters extends QueryStringParameters {
    nome?: string;
    codAuditoriaVeiculoTanque?: number;
};
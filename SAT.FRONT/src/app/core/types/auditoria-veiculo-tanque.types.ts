import { AuditoriaVeiculo } from "./auditoria-veiculo.types";
import { Meta, QueryStringParameters } from "./generic.types";

export class AuditoriaVeiculoTanque {
    codAuditoriaVeiculoTanque : number;
    nome: string;
}

export interface AuditoriaVeiculoTanqueData extends Meta {
    items: AuditoriaVeiculoTanque[]
};

export interface AuditoriaVeiculoTanqueParameters extends QueryStringParameters {
    nome?: string;
};
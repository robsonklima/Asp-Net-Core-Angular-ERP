import { AuditoriaVeiculo } from "./auditoria-veiculo.types";
import { Meta, QueryStringParameters } from "./generic.types";

export class AuditoriaVeiculoAcessorio {
    codAuditoriaVeiculoAcessorio : number;
    codAuditoriaVeiculo: number;
    auditoriaVeiculo: AuditoriaVeiculo;
    nome: string;
    selecionado: number;
    justificativa: string;
    dataHoraCad: string;
}

export interface AuditoriaVeiculoAcessorioData extends Meta {
    items: AuditoriaVeiculoAcessorio[]
};

export interface AuditoriaVeiculoAcessorioParameters extends QueryStringParameters {
    nome?: string;
};
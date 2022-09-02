import { AuditoriaVeiculoAcessorio } from "./auditoria-veiculo-acessorio.types";
import { AuditoriaVeiculoTanque } from "./auditoria-veiculo-tanque.types";
import { Meta, QueryStringParameters } from "./generic.types";

export class AuditoriaVeiculo {
    codAuditoriaVeiculo : number;
    codAuditoriaVeiculoTanque : number;
    auditoriaVeiculoTanque ? : AuditoriaVeiculoTanque;
    placa : string;
    odometro : string;
    dataHoraCad : string;
    acessorios: AuditoriaVeiculoAcessorio[]
}

export interface AuditoriaVeiculoData extends Meta {
    items: AuditoriaVeiculo[]
};

export interface AuditoriaVeiculoParameters extends QueryStringParameters {
    placa?: string;
};
import { AuditoriaStatus } from "./auditoria-status.types";
import { Meta, QueryStringParameters } from "./generic.types";
import { Usuario } from "./usuario.types";

export class Auditoria {
    codAuditoria : number;
    codUsuario : number ;
    usuario?: Usuario;
    codAuditoriaVeiculo ?: number;
    //auditoriaVeiculo ?: AuditoriaVeiculo;
    dataHoraRetiradaVeiculo ?: Date;
    dataHoraCad : string;
    codAuditoriaStatus : number;
    auditoriaStatus ?: AuditoriaStatus;
    totalMesesEmUso ?: number;
    valorCombustivel ?: number;
    dataRetiradaVeiculo ?: Date;
    creditosCartao ?: number;
    despesasSAT ?: number;
    totalDiasEmUso ?: number;
    despesasCompensadasValor ?: number;
    odometroInicialRetirada ?: number;
    odometroPeriodoAuditado ?: number;
    saldoCartao ?: number;
    kmPercorrido ?: number;
    kmCompensado ?: number
    valorTanque ?: number;
    kmFerias ?: number;
    usoParticular : number;
    kmParticular ?: number;
    observacoes ?: string;
    kmParticularMes ?: number;
    dataHoraManut ?: Date
    codUsuarioManut ?: string;
}

export interface AuditoriaData extends Meta {
    items: Auditoria[]
};

export interface AuditoriaParameters extends QueryStringParameters {
    codAuditoria?: number;
    codUsuario?: number;
};
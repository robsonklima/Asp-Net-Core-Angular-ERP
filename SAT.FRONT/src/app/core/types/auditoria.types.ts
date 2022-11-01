import { AuditoriaFoto } from "./auditoria-foto.types";
import { AuditoriaStatus } from "./auditoria-status.types";
import { AuditoriaVeiculo } from "./auditoria-veiculo.types";
import { Meta, QueryStringParameters } from "./generic.types";
import { Usuario } from "./usuario.types";

export class Auditoria {
    codAuditoria : number;
    codUsuario : number ;
    usuario?: Usuario;
    codAuditoriaVeiculo ?: number;
    auditoriaVeiculo ?: AuditoriaVeiculo;
    dataHoraRetiradaVeiculo ?: string;
    dataHoraCad : string;
    codAuditoriaStatus : number;
    auditoriaStatus ?: AuditoriaStatus;
    totalMesesEmUso ?: number;
    valorCombustivel ?: number;
    dataRetiradaVeiculo ?: string;
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
    dataHoraManut ?: string;
    codUsuarioManut ?: string;
    qtdDespesasPendentes ?: number;
    fotos ?: AuditoriaFoto[]
}

export interface AuditoriaData extends Meta {
    items: Auditoria[]
};

export interface AuditoriaParameters extends QueryStringParameters {
    codAuditoria?: number;
    codUsuario?: number;
};

export interface AuditoriaView {
    codAuditoria: number;
    codUsuario: string;
    nomeUsuario: string;
    codAuditoriaStatus: number;
    nomeAuditoriaStatus: string;
    numeroCartao: string;
    codFilial: number;
    nomeFilial: string;
    qtdDiasAuditoriaAnterior: number | null;
    qtdDespesasPendentes: number | null;
    odometroAnterior: number | null;
    odometroAtual: number | null;
    quilometrosPorLitro?: number;
}

export interface AuditoriaViewData extends Meta {
    items: AuditoriaView[]
};
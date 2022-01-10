import { Meta, QueryStringParameters } from "./generic.types";

export class Monitoramento
{
    codLogAlerta: number;
    servidor: string;
    item: string;
    mensagem: string;
    tipo: string;
    emUso: number;
    total: number;
    disco: string;
    status: string;
    descricao: string;
    dataHoraProcessamento: string;
    dataHoraCad: string;
}

export interface MonitoramentoData extends Meta
{
    items: Monitoramento[];
};

export interface MonitoramentoParameters extends QueryStringParameters
{
    tipo?: string;
    servidor?: string;
    item?: string;
    dataHoraProcessamento?: string;
};

export const monitoramentoTipoConst = {
    SERVICO: "SERVICO",
    INTEGRACAO: 'INTEGRACAO',
    STORAGE: 'STORAGE',
    MEMORY: 'MEMORY',
    CPU: 'CPU',
    CHAMADO: 'CHAMADO'
}

export const monitoramentoStatusConst = {
    DANGER: "DANGER",
    WARNING: "WARNING",
    OK: "OK"
}

export const monitoramentoItemConst = {
    SERVICOSATINTEGRACAOBRBV2: "ServicoSatIntegracaoBRBV2",
    SERVICOSATINTEGRACAOMETROSP: 'ServicoSatIntegracaoMetroSP',
    SERVICOSATDADOIMPORTACAO: 'ServicoSatDadoImportacao',
    BANRISULENVIAEMAILSERVICE: 'BanrisulEnviaEmailService',
    ANALISABLOQUEIOPONTO: 'Analisa Bloqueio Ponto',
    ANALISAINCONSISTENCIAPONTO: 'Analisa Inconsistencia Ponto',
    TICKETLOG: 'Ticket Log'
}
import { DespesaProtocoloPeriodoTecnico } from "./despesa-protocolo.types";
import { Despesa } from "./despesa.types";
import { Meta, QueryStringParameters } from "./generic.types";
import { Tecnico } from "./tecnico.types";
import { TicketLogPedidoCredito } from "./ticketlog-types";

export class DespesaPeriodo
{
    codDespesaPeriodo: number;
    codDespesaConfiguracao: number;
    dataInicio: string;
    dataFim: string;
    indAtivo: number;
    codUsuarioCad: string;
    dataHoraCad: string;
    codUsuarioManut: string;
    dataHoraManut: string;
}

export interface DespesaPeriodoData extends Meta
{
    items: DespesaPeriodo[]
};

export interface DespesaPeriodoParameters extends QueryStringParameters
{
    indAtivo?: number;
    inicioPeriodo?: string;
    fimPeriodo?: string;
};

export class DespesaPeriodoTecnicoStatus
{
    codDespesaPeriodoTecnicoStatus: string;
    nomeDespesaPeriodoTecnicoStatus: string;
}

export class DespesaPeriodoTecnico
{
    codDespesaPeriodoTecnico: number;
    codDespesaPeriodo: number;
    despesaPeriodo: DespesaPeriodo;
    despesas: Despesa[];
    codTecnico: number;
    tecnico: Tecnico;
    despesaPeriodoTecnicoStatus: DespesaPeriodoTecnicoStatus;
    codDespesaPeriodoTecnicoStatus: number;
    codUsuarioCad: string;
    dataHoraCad: string;
    codUsuarioManut: string;
    dataHoraManut: string;
    codUsuarioCredito: string;
    dataHoraCredito: string;
    codUsuarioCreditoCancelado: string;
    dataHoraCreditoCancelado: string;
    indCredito: number;
    codUsuarioVerificacao: string;
    dataHoraVerificacao: string;
    indVerificacao: number;
    codUsuarioVerificacaoCancelado: string;
    dataHoraVerificacaoCancelado: string;
    indCompensacao: number;
    dataHoraCompensacao: string;
    codUsuarioCompensacao: string;
    despesaProtocoloPeriodoTecnico?: DespesaProtocoloPeriodoTecnico;
    ticketLogPedidoCredito?: TicketLogPedidoCredito;
}

export interface DespesaPeriodoTecnicoData extends Meta
{
    items: DespesaPeriodoTecnico[]
};

export interface DespesaPeriodoTecnicoParameters extends QueryStringParameters
{
    codTecnico?: string;
    codFilial?: string;
    indAtivoPeriodo?: number;
    codDespesaPeriodoStatus?: string;
    inicioPeriodo?: string;
    fimPeriodo?: string;
    filterType?: DespesaPeriodoTecnicoFilterEnum;
};

export enum DespesaPeriodoTecnicoStatusEnum
{
    "LIBERADO PARA ANÁLISE" = "1",
    "APROVADO" = "2",
    "REPROVADO" = "3"
}

export enum DespesaPeriodoTecnicoFilterEnum
{
    FILTER_PERIODOS_APROVADOS = 1,
    FILTER_CREDITOS_CARTAO = 2
}
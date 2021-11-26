import { DespesaAdiantamentoPeriodoConsultaTecnicoItem } from "./despesa-adiantamento.types";
import { DespesaProtocoloPeriodoTecnico } from "./despesa-protocolo.types";
import { Despesa } from "./despesa.types";
import { Meta, QueryStringParameters } from "./generic.types";
import { Tecnico, TecnicoCategoriaCredito } from "./tecnico.types";
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
    nomeDespesaPeriodoTecnicoStatus?: string;
}

export class DespesaPeriodoTecnico
{
    codDespesaPeriodoTecnico?: number;
    codDespesaPeriodo: number;
    despesaPeriodo?: DespesaPeriodo;
    despesas?: Despesa[];
    codTecnico: number;
    tecnico?: Tecnico;
    despesaPeriodoTecnicoStatus?: DespesaPeriodoTecnicoStatus;
    codDespesaPeriodoTecnicoStatus: number;
    codUsuarioCad: string;
    dataHoraCad: string;
    codUsuarioManut?: string;
    dataHoraManut?: string;
    codUsuarioCredito?: string;
    dataHoraCredito?: string;
    codUsuarioCreditoCancelado?: string;
    dataHoraCreditoCancelado?: string;
    indCredito?: number;
    codUsuarioVerificacao?: string;
    dataHoraVerificacao?: string;
    indVerificacao?: number;
    codUsuarioVerificacaoCancelado?: string;
    dataHoraVerificacaoCancelado?: string;
    indCompensacao?: number;
    dataHoraCompensacao?: string;
    codUsuarioCompensacao?: string;
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
    codDespesaPeriodo?: number;
    indAtivoPeriodo?: number;
    codDespesaPeriodoStatus?: string;
    inicioPeriodo?: string;
    fimPeriodo?: string;
    filterType?: DespesaPeriodoTecnicoFilterEnum;
};


export interface DespesaCreditosCartaoListView 
{
    protocolo: string;
    rd: number;
    cadastro: string;
    tecnico: string;
    categoriaCredito: TecnicoCategoriaCredito;
    filial: string;
    cartao: string;
    dataManutSaldo: string;
    saldo: number;
    integrado: string;
    obs?: string;
    inicio: string;
    fim: string;
    combustivel: number;
    indCreditado: boolean;
    indCompensado: boolean;
    indVerificado: boolean;
}

export enum DespesaCreditoCartaoStatusEnum 
{
    "AGUARDANDO VERIFICAÇÃO",
    "VERIFICADO",
    "CREDITADO",
    "COMPENSADO"
}

export interface DespesaPeriodoTecnicoAtendimentoItem
{
    codDespesaPeriodo: number;
    codDespesaPeriodoTecnico?: number;
    codTecnico: string;
    dataInicio: string;
    dataFim: string;
    totalDespesa: number;
    totalAdiantamento: number;
    restituirAEmpresa: number;
    gastosExcedentes: number;
    status: DespesaPeriodoTecnicoStatus;
    indAtivo: boolean;
};


export interface DespesaPeriodoTecnicoAtendimentoData extends Meta
{
    items: DespesaPeriodoTecnicoAtendimentoItem[];
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
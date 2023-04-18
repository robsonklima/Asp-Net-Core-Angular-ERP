import { DespesaPeriodo, DespesaPeriodoTecnicoStatus } from "./despesa-periodo.types";
import { Meta, QueryStringParameters } from "./generic.types";
import { Tecnico } from "./tecnico.types";

export interface DespesaAdiantamentoPeriodo
{
    codDespesaAdiantamentoPeriodo?: number;
    codDespesaAdiantamento: number;
    codDespesaPeriodo: number;
    valorAdiantamentoUtilizado: number;
    codUsuarioCad: string;
    dataHoraCad: string;
    despesaAdiantamento?: DespesaAdiantamento;
    despesaPeriodo?: DespesaPeriodo;
}

export interface DespesaAdiantamentoPeriodoParameters extends QueryStringParameters 
{
    codDespesaPeriodo?: number;
    codTecnico?: number;
    indAtivoPeriodo?: number;
    indAtivoAdiantamento?: number;
    indTecnicoLiberado?: number;
    indAtivoTecnico?: number;
    codFiliais?: string;
    codAutorizadas?: string;
};

export interface AdiantamentoRDsPendentesView {
    codTecnico: number;
    tecnico: string;
    nroRD: number;
    dataInicio: string;
    dataFim: string;
    totalRD: number | null;
    despesas: number | null;
    adiantamento: number | null;
    reembolso: number;
    saldoAdiantamentoSAT: number;
    protocolo: string;
    dtEnvioProtocolo: string;
    situacao: string;
    controladoria: string;
    cor: string;
    nomeDespesaPeriodoTecnicoStatus: string;
    dataHoraManut: string | null;
    dataHoraCad: string | null;
    dataInicio2: string | null;
}

export interface DespesaAdiantamentoPeriodoData extends Meta
{
    items: DespesaAdiantamentoPeriodo[]
};

export interface AdiantamentoRDsPendentesViewData extends Meta
{
    items: AdiantamentoRDsPendentesView[]
};

export interface DespesaAdiantamentoPeriodoConsultaTecnicoData extends Meta
{
    items: DespesaAdiantamentoPeriodoConsultaTecnicoItem[];
};

export interface DespesaAdiantamento
{
    codDespesaAdiantamento?: number;
    tecnico?: Tecnico;
    codTecnico: number;
    codDespesaAdiantamentoTipo: number;
    despesaAdiantamentoTipo: DespesaAdiantamentoTipo;
    dataAdiantamento: string;
    valorAdiantamento: number;
    indAtivo: number;
    codUsuarioCad: string;
    dataHoraCad: string;
    codUsuarioManut: string;
    dataHoraManut: string;
}

export interface DespesaAdiantamentoSolicitacao {
    codDespesaAdiantamentoSolicitacao: number;
    codTecnico: number;
    nome: string;
    cPF: string;
    banco: string;
    agencia: string;
    contaCorrente: string;
    saldoLogix: number;
    mediaMensal: number;
    mediaQuinzenal: number;
    mediaSemanal: number;
    saldoAbertoLogixMensal: number;
    saldoAbertoLogixQuinzenal: number;
    saldoAbertoLogixSemanal: number;
    rDsEmAbertoMensal: number;
    rDsEmAbertoQuinzenal: number;
    rDsEmAbertoSemanal: number;
    saldoAdiantamentoSATMensal: number;
    saldoAdiantamentoSATQuinzenal: number;
    saldoAdiantamentoSATSemanal: number;
    maximoParaSolicitarMensal: number;
    maximoParaSolicitarQuinzenal: number;
    maximoParaSolicitarSemanal: number;
    valorAdiantamentoSolicitado: number;
    justificativa: string;
    emails: string;
    codUsuarioCad: string;
    dataHoraCad: string;
}

export interface DespesaAdiantamentoData extends Meta
{
    items: DespesaAdiantamento[]
};

export enum DespesaAdiantamentoTipoEnum
{
    "FIXO" = "1",
    "PROVISÓRIO" = "2"
};

export interface DespesaAdiantamentoParameters extends QueryStringParameters
{
    indAtivo?: number;
    codDespesaAdiantamentoTipo?: string;
    codTecnicos?: string;
};

export interface DespesaAdiantamentoPeriodoConsultaTecnicoItem
{
    tecnico: Tecnico;
    saldoAdiantamento: number;
    status: DespesaPeriodoTecnicoStatus;
    indAtivo: boolean;
    liberado: boolean;
};

export interface DespesaAdiantamentoTipo
{
    codDespesaAdiantamentoTipo?: number;
    nomeAdiantamentoTipo?: string;
};

export interface DespesaAdiantamentoTipoParameters extends QueryStringParameters
{
};

export interface DespesaAdiantamentoTipoData extends Meta
{
    items: DespesaAdiantamentoTipo[]
};

export interface ViewMediaDespesasAdiantamento {
    codTecnico: number;
    tecnico: string;
    cpf: string;
    banco: string;
    agencia: string;
    contaCorrente: string;
    emailDefault: string;
    mediaMensal?: number;
    mediaQuinzenal?: number;
    mediaSemanal?: number;
    saldoAbertoLogixMensal: number;
    saldoAbertoLogixQuinzenal: number;
    saldoAbertoLogixSemanal: number;
    rdsEmAbertoMensal?: number;
    rdsEmAbertoQuinzenal?: number;
    rdsEmAbertoSemanal?: number;
    saldoAdiantamentoSatmensal: number;
    saldoAdiantamentoSatquinzenal: number;
    saldoAdiantamentoSatsemanal: number;
    maximoParaSolicitarMensal?: number;
    maximoParaSolicitarQuinzenal?: number;
    maximoParaSolicitarSemanal?: number;
}
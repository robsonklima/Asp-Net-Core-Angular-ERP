import { DespesaPeriodo, DespesaPeriodoTecnicoStatus } from "./despesa-periodo.types";
import { Meta, QueryStringParameters } from "./generic.types";
import { Tecnico } from "./tecnico.types";

export interface DespesaAdiantamentoPeriodo
{
    codDespesaAdiantamentoPeriodo: number;
    codDespesaAdiantamento: number;
    codDespesaPeriodo: number;
    valorAdiantamentoUtilizado: number;
    codUsuarioCad: string;
    dataHoraCad: string;
    despesaAdiantamento: DespesaAdiantamento;
    despesaPeriodo: DespesaPeriodo;
}

export interface DespesaAdiantamentoPeriodoParameters extends QueryStringParameters 
{
    codDespesaPeriodos?: string;
    codTecnicos?: string;
    indAtivoPeriodo?: number;
    indAtivoAdiantamento?: number;
    indTecnicoLiberado?: number;
    indAtivoTecnico?: number;
    codFiliais?: string;
};

export interface DespesaAdiantamentoPeriodoData extends Meta
{
    items: DespesaAdiantamentoPeriodo[]
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

export interface DespesaAdiantamentoData extends Meta
{
    items: DespesaAdiantamento[]
};

export interface DespesaAdiantamentoParameters extends QueryStringParameters
{
    indAtivo?: number;
};

export interface DespesaPeriodoTecnicoAtendimentoItem
{
    codDespesaPeriodo: number;
    dataInicio: string;
    dataFim: string;
    totalDespesa: number;
    totalAdiantamento: number;
    restituirAEmpresa: number;
    gastosExcedentes: number;
    status: DespesaPeriodoTecnicoStatus;
    indAtivo: boolean;
};

export interface DespesaAdiantamentoPeriodoConsultaTecnicoData extends Meta
{
    items: DespesaAdiantamentoPeriodoConsultaTecnicoItem[];
};

export interface DespesaPeriodoTecnicoAtendimentoData extends Meta
{
    items: DespesaPeriodoTecnicoAtendimentoItem[];
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
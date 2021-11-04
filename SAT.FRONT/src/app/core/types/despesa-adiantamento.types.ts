import { DespesaPeriodo, DespesaPeriodoTecnicoStatus } from "./despesa-periodo.types";
import { Meta, QueryStringParameters } from "./generic.types";
import { Tecnico } from "./tecnico.types";

export class DespesaAdiantamentoPeriodo
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

export class DespesaAdiantamento
{
    codDespesaAdiantamento: number;
    codTecnico: number;
    codDespesaAdiantamentoTipo: number;
    dataAdiantamento: string;
    valorAdiantamento: number;
    indAtivo: number;
    codUsuarioCad: string;
    dataHoraCad: string;
    codUsuarioManut: string;
    dataHoraManut: string;
}

export interface DespesaPeriodoTecnicoAtendimentoData extends Meta
{
    items: DespesaPeriodoTecnicoAtendimentoItem[];
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

export interface DespesaAdiantamentoPeriodoConsultaTecnicoItem
{
    tecnico: Tecnico;
    saldoAdiantamento: number;
    status: DespesaPeriodoTecnicoStatus;
    indAtivo: boolean;
    liberado: boolean;
};
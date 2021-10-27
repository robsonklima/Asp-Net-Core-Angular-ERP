import { Meta, QueryStringParameters } from "./generic.types";

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
};

export class DespesaPeriodoTecnico
{
    codDespesaPeriodoTecnico: number;
    codDespesaPeriodo: number;
    codTecnico: number;
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
}

export interface DespesaPeriodoTecnicoData extends Meta
{
    items: DespesaPeriodoTecnico[]
};

export interface DespesaPeriodoTecnicoParameters extends QueryStringParameters
{
    codTecnico?: number;
    codDespesaPeriodo?: number;
    indAtivoPeriodo?: number;
};
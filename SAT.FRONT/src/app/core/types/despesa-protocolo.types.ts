import { DespesaPeriodoTecnico } from "./despesa-periodo.types";
import { Meta, QueryStringParameters } from "./generic.types";

export interface DespesaProtocolo
{
    codDespesaProtocolo: number;
    codFilial?: number;
    nomeProtocolo: string;
    obsProtocolo: string;
    indAtivo: number;
    indFechamento: number;
    dataHoraFechamento: number;
    codUsuarioCad: string;
    dataHoraCad: string;
    codUsuarioManut: string;
    dataHoraManut: string;
    indIntegracao: number;
    indImpresso: number;
    despesaProtocoloPeriodoTecnico: DespesaProtocoloPeriodoTecnico[];
}

export interface DespesaProtocoloData extends Meta
{
    items: DespesaProtocolo[]
};

export interface DespesaProtocoloParameters extends QueryStringParameters
{

};

export interface DespesaProtocoloPeriodoTecnico
{
    codDespesaProtocolo?: number;
    codDespesaPeriodoTecnico?: number;
    despesaPeriodoTecnico?: DespesaPeriodoTecnico[];
    codUsuarioCad?: string;
    dataHoraCad?: string;
    codUsuarioCredito?: string;
    dataHoraCredito?: string;
    codUsuarioCreditoCancelado?: string;
    dataHoraCreditoCancelado?: string;
    indCreditado?: number;
    indAtivo?: number;
}

export interface DespesaProtocoloPeriodoListView
{
    dataInicial: string;
    dataFinal: string;
    tecnico: string;
    valor: number;
}

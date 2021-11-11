import { Meta, QueryStringParameters } from "./generic.types";

export interface DespesaProtocolo
{
    codDespesaProtocolo: number;
    codFilial?: number;
    nomeProtocolo: string;
    obsProtocolo: string;
    indAtivo: number;
    ondFechamento: number;
    dataHoraFechamento: number;
    codUsuarioCad: string;
    dataHoraCad: string;
    codUsuarioManut: string;
    dataHoraManut: string;
    indIntegracao: number;
    indImpresso: number;
}

export interface DespesaProtocoloData extends Meta
{
    items: DespesaProtocolo[]
};

export interface DespesaProtocoloParameters extends QueryStringParameters
{

};
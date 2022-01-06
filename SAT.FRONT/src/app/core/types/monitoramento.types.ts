import { QueryStringParameters } from "./generic.types";

// export class Monitoramento
// {
//     public integracaoServidor: MonitoramentoDados[] = [];
//     public storageAPL1: MonitoramentoStorage[] = [];
//     public storageINT1: MonitoramentoStorage[] = [];
// }
// 
// export class MonitoramentoDados
// {
//     public servidor: string;
//     public item: string;
//     public mensagem: string;
//     public tipo: string;
//     public espacoEmGb: number;
//     public tamanhoEmGb: number;
//     public disco: string;
//     public dataHoraProcessamento: string;
//     public dataHoraCad: Date;
//     public ociosidade: string;
//     public servidorOk: boolean;
// }
// 
// export class MonitoramentoStorage
// {
//     public unidade: string;
//     public valor: number;
// }

export interface Monitoramento
{
    nome: string;
    dataProcessamento: string;
    mensagem?: string;
}

export interface MonitoramentoParameters extends QueryStringParameters
{
    tipo: MonitoramentoTipoEnum;
}

export enum MonitoramentoTipoEnum
{
    CLIENTE = 1,
    SERVICO = 2
}
import { QueryStringParameters } from "./generic.types";

export class Monitoramento
{
    public integracaoServidor: MonitoramentoDados[] = [];
    public storageAPL1: MonitoramentoStorage[] = [];
    public storageINT1: MonitoramentoStorage[] = [];
}

export class MonitoramentoDados
{
    public servidor: string;
    public item: string;
    public mensagem: string;
    public tipo: string;
    public espacoEmGb: number;
    public tamanhoEmGb: number;
    public disco: string;
    public dataHoraProcessamento: string;
    public dataHoraCad: Date;
    public ociosidade: string;
    public servidorOk: boolean;
}

export class MonitoramentoStorage
{
    public unidade: string;
    public valor: number;
}

export interface MonitoramentoCliente
{
    nome: string;
    dataProcessamento: string;
}

export interface MonitoramentoClienteParameters extends QueryStringParameters
{

}

export enum MonitoramentoTipoEnum
{
    CLIENTE = 0,
    SERVICO = 2,
    INTEGRACAO = 2,
    STORAGE = 3,
    MEMORY = 4,
    CPU = 5,
    CHAMADO = 6
}
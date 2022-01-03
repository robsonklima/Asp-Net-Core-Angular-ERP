
export class Monitoramento {
    public integracaoServidor: MonitoramentoDados[] = [];
    public storageAPL1: MonitoramentoStorage[] = [];
    public storageINT1: MonitoramentoStorage[] = [];
}

export class MonitoramentoDados {
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

export class MonitoramentoStorage {
    public unidade: string;
    public valor: number;
}

export class MonitoramentoClienteViewModel {
    public nomeCliente: string;
    public dataUltimoChamado: string;
    public ociosidade: string;
}
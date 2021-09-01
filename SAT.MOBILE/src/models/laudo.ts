import { LaudoSituacao } from "./laudo-situacao";

export class Laudo {
    relatoCliente: string;
    conclusao: string;
    situacoes: LaudoSituacao[];
    codOS: number;
    codRAT: number;
    codTecnico: number;
    assinaturaTecnico: string;
    assinaturaCliente: string;
    indAtivo: number;
    dataHoraCad: string;
    tensaoComCarga: string;
    tensaoSemCarga: string;
    tensaoTerraENeutro: string;
    temperatura: string;
    indRedeEstabilizada: number;
    indPossuiNobreak: number;
    indPossuiArCond: number;
    justificativa: string;
    nomeCliente: string;
    matriculaCliente: string;
  }
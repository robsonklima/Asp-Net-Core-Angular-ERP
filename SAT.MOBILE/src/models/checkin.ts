import { Localizacao } from './localizacao';

export class Checkin {
    dataHoraCadastro: string;
    localizacao: Localizacao;
    tentativas: string[];
    status: string;
    codOS: number;
    codUsuario: string;
}
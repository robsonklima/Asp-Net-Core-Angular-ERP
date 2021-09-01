import { Localizacao } from './localizacao';

export class Checkout {
    dataHoraCadastro: string;
    localizacao: Localizacao;
    tentativas: string[];
    status: string;
    codOS: number;
    codUsuario: string;
}
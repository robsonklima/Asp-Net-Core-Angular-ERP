import { BancadaLista } from "./bancada-lista.types";
import { Cidade } from "./cidade.types";
import { Meta, QueryStringParameters } from "./generic.types";

export class ClienteBancada {
    codClienteBancada: number;
    codCidade: number;
    nomeCliente: string;
    apelido: string;
    cnpJ_CGC: string;
    endereco: string;
    numero: string;
    complem: string;
    bairro: string;
    telefone: string;
    cep: string;
    contato: string;
    codUsuarioCadastro: string;
    dataCadastro: string;
    codUsuarioManut: string;
    dataManut: string;
    email: string;
    icms: number;
    codFormaPagamento: number;
    codTransportadora: number;
    indAtivo: number;
    inflacao: number;
    inflacaoObs: string;
    deflacao: number;
    deflacaoObs: string;
    codTipoFrete: number;
    indOrcamento: number;
    cidade: Cidade;
    codBancadaLista: number;
}

export interface ClienteBancadaData extends Meta {
    items: ClienteBancada[];
};

export interface ClienteBancadaParameters extends QueryStringParameters {
    indAtivo?: number;
};

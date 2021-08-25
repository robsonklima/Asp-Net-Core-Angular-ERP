import { Cidade } from "./cidade.types";
import { Meta, QueryStringParameters } from "./generic.types";
import { Transportadora } from "./transportadora.types";

export class Cliente {
    codCliente: number;
    codFormaPagto?: any;
    codMoeda: number;
    codTipoFrete?: any;
    codPecaLista: number;
    codTransportadora?: any;
    transportadora?: Transportadora;
    codCidade?: any;
    cidade?: Cidade;
    razaoSocial: string;
    nomeFantasia: string;
    numBanco: string;
    cnpj: string;
    inscricaoEstadual?: any;
    cep?: any;
    endereco?: any;
    bairro: string;
    fone?: any;
    fax?: any;
    email?: any;
    site?: any;
    observacao?: any;
    inflacao: number;
    deflacao: number;
    percIcms: number;
    indHabilitaVendaPecas: number;
    indPecaListaSomente: number;
    indRevisao: number;
    indAtivo: number;
    codUsuarioCad: string;
    dataHoraCad: Date;
    codUsuarioManut: string;
    dataHoraManut: Date;
    totalEquipAtivos?: any;
    totalEquipDeastivados?: any;
    totalEquip?: any;
    numeroEnd?: any;
    obsCliente?: any;
    complemEnd?: any;
    siglaUF: string;
    telefone1: string;
    telefone2?: any;
    dataCadastro: Date;
    codUsuarioCadastro: string;
    dataManutencao: Date;
    codUsuarioManutencao: string;
    codTipoMercado: number;
    codUsuarioCoordenadorContrato?: any;
    icmsLab?: any;
    inflacaoLab?: any;
    inflacaoObsLab?: any;
    deflacaoLab?: any;
    deflacaoObsLab?: any;
    codPecaListaLab?: any;
    codFormaPagtoLab?: any;
    codTipoFreteLab?: any;
    codTransportadoraLab?: any;
    indOrcamentoLab?: any;
}

export interface ClienteData extends Meta {
    items: Cliente[];
};

export interface ClienteParameters extends QueryStringParameters {
    codCliente?: number;
    indAtivo?: number;
};
import { Autorizada } from "./autorizada.types";
import { Cidade } from "./cidade.types";
import { Cliente } from "./cliente.types";
import { Filial } from "./filial.types";
import { Meta, QueryStringParameters } from "./generic.types";
import { Regiao } from "./regiao.types";
import { TipoRota } from "./tipo-rota.types";

export class LocalAtendimento {
    codPosto: number;
    codCliente: number;
    cliente: Cliente;
    nomeLocal: string;
    numAgencia: string;
    dcPosto: string;
    codTipoRota: number;
    tipoRota: TipoRota;
    cnpj: string;
    inscricaoEstadual?: any;
    cep: string;
    endereco: string;
    enderecoComplemento?: any;
    bairro: string;
    codCidade: number;
    cidade: Cidade;
    email?: any;
    site?: any;
    fone: string;
    fax?: any;
    descTurno?: any;
    longitude: string;
    latitude: string;
    enderecoCoordenadas: string;
    bairroCoordenadas: string;
    cidadeCoordenadas: string;
    ufCoordenadas: string;
    paisCoordenadas: string;
    distanciaKmPatRes?: any;
    observacao?: any;
    indAtivo: number;
    codUsuarioCad: string;
    dataHoraCad: string;
    codUsuarioManut: string;
    dataHoraManut: string;
    numeroEnd?: any;
    codRegiao?: any;
    regiao?: Regiao;
    codAutorizada?: any;
    autorizada?: Autorizada;
    codFilial?: any;
    filial?: Filial;
    siglaUf?: any;
    codRegional?: any;
    cnpjFaturamento?: any;
    senhaAcessoNotaFiscal?: any;
}

export interface LocalAtendimentoData extends Meta {
    locaisAtendimento: LocalAtendimento[];
};

export interface LocalAtendimentoParameters extends QueryStringParameters {
    codPosto?: number;
    codCliente?: number;
    indAtivo?: number;
};
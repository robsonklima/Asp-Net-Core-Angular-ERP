import { Cidade } from "./cidade.types";
import { Filial } from "./filial.types";
import { Pais } from "./pais.types";
import { Meta, QueryStringParameters } from "./generic.types";
import { TipoRota } from "./tipo-rota.types";

export interface Autorizada {
    codAutorizada: number;
    codFilial: number;
    filial?: Filial;
    razaoSocial: string;
    nomeFantasia: string;
    cnpj: string;
    inscricaoEstadual: string;
    cep: string;
    endereco: string;
    bairro: string;
    codCidade: number;
    cidade?: Cidade;
    email: string;
    fone: string;
    fax: string;
    indFilialPerto: number;
    indAtivo: number;
    codUsuarioCad?: string;
    dataHoraCad?: string;
    codUsuarioManut: string;
    dataHoraManut: string;
    codTipoRota: number;
    tipoRota?: TipoRota;
    longitude: string;
    latitude: string;
    atendePOS?: number;
}

export interface AutorizadaData extends Meta {
    items: Autorizada[];
};

export interface AutorizadaParameters extends QueryStringParameters {
    codAutorizada?: number;
    codFilial?: number;
    indAtivo?: number;
};
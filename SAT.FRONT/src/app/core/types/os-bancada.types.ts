import { Filial } from "./filial.types";
import { ClienteBancada } from "./cliente-bancada.types";
import { Meta, QueryStringParameters } from "./generic.types";
import { OSBancadaPecas } from "./os-bancada-pecas.types";

export class OSBancada {
    codOsbancada: number;
    codClienteBancada: number;
    nfentrada: string;
    dataChegada?: string;
    dataNf?: string;
    codUsuarioCadastro: string;
    dataCadastro: string;
    codUsuarioManut?: string;
    dataManut?: string;
    observacao?: string;
    codFilial: number;
    prazoEntrega?: number;
    valorNf?: number;
    filial?: Filial;
    clienteBancada?: ClienteBancada;
}

export interface OSBancadaData extends Meta {
    items: OSBancada[];
};

export interface OSBancadaParameters extends QueryStringParameters {
    indAtivo?: number;
    codFiliais?: string;
    codClienteBancadas?: string;
};

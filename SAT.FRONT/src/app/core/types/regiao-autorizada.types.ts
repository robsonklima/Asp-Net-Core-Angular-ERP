import { Autorizada } from "./autorizada.types";
import { Cidade } from "./cidade.types";
import { Filial } from "./filial.types";
import { Meta, QueryStringParameters } from "./generic.types";
import { Regiao } from "./regiao.types";

export class RegiaoAutorizada {
    codRegiao: number;
    regiao: Regiao;
    codAutorizada: number;
    autorizada: Autorizada;
    codFilial: number;
    filial: Filial;
    codCidade: number;
    cidade: Cidade;
    pa: number;
    indAtivo: number;
}

export interface RegiaoAutorizadaData extends Meta {
    regioesAutorizadas: RegiaoAutorizada[];
};

export interface RegiaoAutorizadaParameters extends QueryStringParameters {
    codRegiao?: number
    codAutorizada?: number
    codFilial?: number
    indAtivo?: number
};
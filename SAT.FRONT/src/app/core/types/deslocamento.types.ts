import { Meta, QueryStringParameters } from "./generic.types";

export class Deslocamento {
    origem: DeslocamentoOrigem;
    destino: DeslocamentoDestino;
    distancia: number;
    tempo: number;
    tipo: DeslocamentoTipoEnum;
}

export interface DeslocamentoOrigem {
    descricao: string;
    lat: number;
    lng: number;
}

export interface DeslocamentoDestino {
    descricao: string;
    lat: number;
    lng: number;
}

export enum DeslocamentoTipoEnum {
    INTENCAO = 1,
}

export interface DeslocamentoData extends Meta {
    items: Deslocamento[];
};

export interface DeslocamentoParameters extends QueryStringParameters {
    codTecnico?: number;
    dataHoraInicioInicio?: string;
    dataHoraInicioFim?: string;
};
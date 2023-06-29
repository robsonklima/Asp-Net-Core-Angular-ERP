import { Meta, QueryStringParameters } from "./generic.types";

export interface ANS {
    codANS?: number;
    codSLA?: number;
    nomeANS: string;
    descANS: string;
    horaInicio: string;
    horaFim: string;
    tempoHoras: number;
    permiteAgendamento: string;
    horasUteis: string;
    sabado: string;
    domingo: string;
    feriado: string;
    dataCadastro: string;
    codUsuarioCad: string;
}

export interface ANSData extends Meta {
    items: ANS[]
};

export interface ANSParameters extends QueryStringParameters {
};
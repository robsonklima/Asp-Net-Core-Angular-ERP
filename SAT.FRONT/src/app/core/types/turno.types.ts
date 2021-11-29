import { Meta, QueryStringParameters } from "./generic.types";

export class Turno {
    codTurno: number;
    descTurno: string;
    horaInicio1: string;
    horaFim1: string;
    horaInicio2: string;
    horaFim2: string;
    indAtivo: number;
}

export interface TurnoData extends Meta {
    items: Turno[];
};

export interface TurnoParameters extends QueryStringParameters {
    
};
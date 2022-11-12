import { Meta, QueryStringParameters } from "./generic.types";

export interface ORTempoReparo {
    codORTempoReparo?: number;
    codORItem: number;
    codTecnico: string;
    dataHoraInicio: string;
    dataHoraFim?: string | null;
    indAtivo: number;
    codUsuarioCad: string;
    dataHoraCad: string;
}

export interface ORTempoReparoData extends Meta {
    items: ORTempoReparo[];
};

export interface ORTempoReparoParameters extends QueryStringParameters {

}
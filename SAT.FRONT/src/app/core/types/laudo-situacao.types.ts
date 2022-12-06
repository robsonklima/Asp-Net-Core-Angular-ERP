import { Meta } from "@angular/platform-browser";
import { QueryStringParameters } from "./generic.types";

export interface LaudoSituacao
{
    codLaudoSituacao: number;
    codLaudo: number;
    causa: string;
    acao: string;
    dataHoraCad: string;
}

export interface LaudoSituacaoData extends Meta {
    items: LaudoSituacao[]
};

export interface LaudoSituacaoParameters extends QueryStringParameters {
    codLaudoSituacao?: number;
};

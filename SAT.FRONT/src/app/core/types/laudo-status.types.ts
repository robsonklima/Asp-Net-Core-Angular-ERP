import { Meta } from "@angular/platform-browser";
import { QueryStringParameters } from "./generic.types";

export interface LaudoStatus
{
    codLaudoStatus: number;
    nomeStatus: string;
    indAtivo?: number;
}

export interface LaudoStatusData extends Meta {
    items: LaudoStatus[]
};

export interface LaudoStatusParameters extends QueryStringParameters {
    codLaudoStatus?: number;
};

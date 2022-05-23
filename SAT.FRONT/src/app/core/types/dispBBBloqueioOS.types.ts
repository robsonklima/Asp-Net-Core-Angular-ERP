import { Meta } from "@angular/platform-browser";
import { QueryStringParameters } from "./generic.types";

export interface DispBBBloqueioOS
{
    codDispBbbloqueioOS: number;
    codOS: number;
    indAtivo: number;
    dataHoraCad: string;
    tipoBloqueio: string;
}

export interface DispBBBloqueioOSData extends Meta
{
    items: DispBBBloqueioOS[]
};

export interface DispBBBloqueioOSParameters extends QueryStringParameters
{
    codDispBbbloqueioOS?: number;
    codOS?: number;
    indAtivo?: number;
};
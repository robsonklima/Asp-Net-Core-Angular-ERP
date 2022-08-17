import { Meta, QueryStringParameters } from "./generic.types";

export class AuditoriaStatus {
    codAuditoriaStatus : number;
    nome : string ;
}

export interface AuditoriaStatusData extends Meta {
    items: AuditoriaStatus[]
};

export interface AuditoriaStatusParameters extends QueryStringParameters {
    codAuditoriaStatus?: number;
};
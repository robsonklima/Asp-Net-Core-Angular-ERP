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

export const AuditoriaStatusEnum = {
    SOLICITADO_AO_TECNICO: 1,
    AGUARDANDO_AVALIACAO_DA_FILIAL: 2,
    FINALIZADO: 3  
};
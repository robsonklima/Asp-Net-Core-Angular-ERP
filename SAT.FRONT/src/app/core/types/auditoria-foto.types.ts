import { Auditoria } from "./auditoria.types";
import { Meta, QueryStringParameters } from "./generic.types";

export class AuditoriaFoto {
    codAuditoriaFoto : number;
    codAuditoria : number ;
    //auditora ?: Auditoria;
    foto : string;
}

export interface AuditoriaFotoData extends Meta {
    items: AuditoriaFoto[]
};

export interface AuditoriaFotoParameters extends QueryStringParameters {
    codAuditoriaFoto?: number;
    codAuditoria?: number;
};
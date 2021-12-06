import { Meta, QueryStringParameters } from "./generic.types";

export interface InstalacaoMotivoRes{
    codInstalMotivoRes: number;
    descMotivoRes: string;
    siglaMotivoRes: string;
    indTipoRes: number;
    indAtivo: number;
}

export interface InstalacaoMotivoResData extends Meta {
    items: InstalacaoMotivoRes[];
};

export interface InstalacaoMotivoResParameters extends QueryStringParameters {
    codInstalMotivoRes: number;    
};
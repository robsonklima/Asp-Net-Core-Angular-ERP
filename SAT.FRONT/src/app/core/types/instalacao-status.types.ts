import { Meta, QueryStringParameters } from "./generic.types";

export class InstalacaoStatus {
    codInstalStatus: number;	
    nomeInstalStatus: string;	
    indAtivo: number;
}

export interface InstalStatusData extends Meta {
    items: InstalacaoStatus[];
};

export interface InstalStatusParameters extends QueryStringParameters {
    codInstalStatus?: number;
    indAtivo?: number;
};
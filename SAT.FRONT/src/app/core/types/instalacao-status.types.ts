import { Meta, QueryStringParameters } from "./generic.types";

export class InstalacaoStatus {
    codInstalStatus: number;	
    nomeInstalStatus: string;	
    indAtivo: number;
}

export interface InstalacaoStatusData extends Meta {
    items: InstalacaoStatus[];
};

export interface InstalacaoStatusParameters extends QueryStringParameters {
    codInstalStatus?: number;
    nomeInstalStatus?: string;
};
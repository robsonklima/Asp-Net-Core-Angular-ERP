import { Meta, QueryStringParameters } from "./generic.types";

export class InstalacaoMotivoMulta {
    codInstalMotivoMulta: number;
    nomeMotivoMulta?: string;
    descMotivoMulta?: string;
    indAtivo?: number;
}

export interface InstalacaoMotivoMultaData extends Meta {
    items: InstalacaoMotivoMulta[];
};

export interface InstalacaoMotivoMultaParameters extends QueryStringParameters {
    codInstalMotivoMulta?: number;
    nomeMotivoMulta?: string;
    descMotivoMulta?: string;
    indAtivo?: number;
};
import { Meta, QueryStringParameters } from "./generic.types";

export class InstalacaoTipoParcela {
    codInstalTipoParcela: number;
    nomeTipoParcela?: string;
    indAtivo?: number;
}

export interface InstalacaoTipoParcelaData extends Meta {
    items: InstalacaoTipoParcela[];
};

export interface InstalacaoTipoParcelaParameters extends QueryStringParameters {
    codInstalTipoParcela?: number;
    nomeTipoParcela?: string;
    indAtivo?: number;
};
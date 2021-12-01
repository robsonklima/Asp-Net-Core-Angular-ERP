import { Meta } from "./generic.types";

export class TipoIndiceReajuste {
    codTipoIndiceReajuste: number;
    nomeTipoIndiceReajuste: string;
    indAtivo: number;
}

export interface TipoIndiceReajusteData extends Meta {
    items: TipoIndiceReajuste[];
};

export class TipoIndiceReajusteParameters {
    codTipoIndiceReajuste?: number;
    nomeTipoIndiceReajuste?: string;
    indAtivo?: number;
}
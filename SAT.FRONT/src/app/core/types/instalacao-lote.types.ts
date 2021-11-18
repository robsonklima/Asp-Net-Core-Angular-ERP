import { Meta, QueryStringParameters } from "./generic.types";

export interface InstalacaoLote {
    codInstalLote: number;
    nomeLote: string;
    descLote: string;
    dataRecLote: string;
    codContrato: number;
    qtdEquipLote: number;
    indAtivo: number;
    codUsuarioCad: string;
    dataHoraCad: string;
    codUsuarioManut: string;
    dataHoraManut: string;
}

export interface InstalacaoLoteData extends Meta {
    items: InstalacaoLote[];
};

export interface InstalacaoLoteParameters extends QueryStringParameters {
    codInstalLote?: number;
    codContrato?: number;
};
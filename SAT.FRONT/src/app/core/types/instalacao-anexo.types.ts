import { Meta } from "@angular/platform-browser";
import { QueryStringParameters } from "./generic.types";

export interface InstalacaoAnexo {
    codInstalAnexo?: number;
    codInstalacao?: number | null;
    codInstalPleito?: number | null;
    codInstalLote?: number | null;
    nomeAnexo: string;
    descAnexo: string;
    sourceAnexo: string;
    indAtivo: number;
    codUsuarioCad: string;
    dataHoraCad: string;
    codUsuarioManut?: string;
    dataHoraManut?: string | null;
    base64?: string;
}

export interface InstalacaoAnexoData extends Meta {
    items: InstalacaoAnexo[];
};

export interface InstalacaoAnexoParameters extends QueryStringParameters {
    codInstalAnexo?: number;
    codInstalacao?: number;
    codInstalPleito?: number;
    codInstalLote?: number;
    nomeAnexo: string;
    descAnexo: string;
};

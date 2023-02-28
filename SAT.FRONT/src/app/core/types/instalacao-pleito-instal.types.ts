import { Meta } from "@angular/platform-browser";
import { QueryStringParameters } from "./generic.types";
import { Instalacao } from "./instalacao.types";

export interface InstalacaoPleitoInstal {
    codInstalacao?: number;
    codInstalPleito: number;
    codEquipContrato?: number | null;
    codUsuarioCad?: string;
    dataHoraCad?: string;
    instalacao?: Instalacao;
}

export interface InstalacaoPleitoInstalData extends Meta {
    items: InstalacaoPleitoInstal[];
};

export interface InstalacaoPleitoInstalParameters extends QueryStringParameters {
    codInstalacao?: number;
    codInstalPleito?: number;
    codEquipContrato?: number;
};
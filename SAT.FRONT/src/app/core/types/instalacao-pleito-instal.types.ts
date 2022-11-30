import { Meta } from "@angular/platform-browser";

export interface InstalacaoPleitoInstal {
    codInstalacao: number;
    codInstalPleito: number;
    codEquipContrato: number | null;
    codUsuarioCad: string;
    dataHoraCad: string;
}

export interface InstalacaoPleitoInstalData extends Meta {
    items: InstalacaoPleitoInstal[];
};
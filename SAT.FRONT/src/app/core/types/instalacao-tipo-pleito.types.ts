import { Meta } from "@angular/platform-browser";

export interface InstalacaoTipoPleito {
    codInstalTipoPleito: number;
    nomeTipoPleito: string;
    descTipoPleito: string;
    indAtivo?: number;
    introTipoPleito?: string;
    instalacaoTipoPleito?: InstalacaoTipoPleito;
}

export interface InstalacaoTipoPleitoData extends Meta {
    items: InstalacaoTipoPleito[];
};
import { Meta } from "@angular/platform-browser";
import { Contrato } from "./contrato.types";
import { QueryStringParameters } from "./generic.types";
import { InstalacaoPleitoInstal } from "./instalacao-pleito-instal.types";
import { InstalacaoTipoPleito } from "./instalacao-tipo-pleito.types";

export interface InstalacaoPleito {
    codInstalPleito: number;
    codContrato?: number;
    codInstalTipoPleito?: number;
    nomePleito: string;
    descPleito: string;
    dataEnvio: string;
    indAtivo?: number;
    codUsuarioCad: string;
    dataHoraCad: string;
    codUsuarioManut: string;
    dataHoraManut: string;
    contrato: Contrato;
    instalacaoTipoPleito: InstalacaoTipoPleito;
    instalacaoPleitoInstal: InstalacaoPleitoInstal[];
}

export interface InstalacaoPleitoData extends Meta {
    items: InstalacaoPleito[];
};

export interface InstalacaoPleitoParameters extends QueryStringParameters {
    codInstalPleito?: number;
    codInstalTipoPleito?: number;
    codContrato?: number;
};
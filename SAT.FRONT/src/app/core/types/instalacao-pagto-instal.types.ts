import { Meta, QueryStringParameters } from "./generic.types";
import { InstalacaoMotivoMulta } from "./instalacao-motivo-multa.types";
import { InstalacaoPagto } from "./instalacao-pagto.types";
import { InstalacaoTipoParcela } from "./instalacao-tipo-parcela.types";
import { Instalacao } from "./instalacao.types";

export class InstalacaoPagtoInstal {
    codInstalacao?: number;
    codInstalPagto?: number;
    codInstalTipoParcela?: number;
    vlrParcela?: number;
    codInstalMotivoMulta?: number;
    vlrMulta?: number;
    indEndossarMulta?: number;
    codUsuarioCad?: string; 
    dataHoraCad?: string; 
    codUsuarioManut?: string; 
    dataHoraManut?: string; 
    comentario?: string;
    indImportacao?: number;
    instalacao?: Instalacao;
    instalTipoParcela?:InstalacaoTipoParcela;
    instalMotivoMulta?: InstalacaoMotivoMulta;
}

export interface InstalacaoPagtoInstalData extends Meta {
    items: InstalacaoPagtoInstal[];
};

export interface InstalacaoPagtoInstalParameters extends QueryStringParameters {
    codInstalacao?: number;
    codInstalPagto?: number;
    codInstalTipoParcela?: number;
    codInstalMotivoMulta?: number;
};
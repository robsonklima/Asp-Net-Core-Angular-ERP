import { Meta, QueryStringParameters } from "./generic.types";
import { InstalacaoMotivoRes } from "./instalacao-ressalva-motivo.types";

export interface InstalacaoRessalva{
    codInstalRessalva: number;
    codInstalacao: number;
    codUsuarioCad: string;
    codInstalMotivoRes: number;
    comentario: string;
    dataHoraCad: string;
    dataOcorrencia: string;
    indAtivo: number;   
    IndJustificativa: number;   
    codUsuarioManut: string;
    dataHoraManut: string;
    instalacaoMotivoRes: InstalacaoMotivoRes;
}

export interface InstalacaoRessalvaData extends Meta {
    items: InstalacaoRessalva[];
};

export interface InstalacaoRessalvaParameters extends QueryStringParameters {
    codInstalRessalva?: number;
    codInstalacao?: number;
};
import { Meta, QueryStringParameters } from "./generic.types";

export class RelatorioAtendimentoPecaStatus {
    codRatpecasStatus?: number;
    descricao?: string;
    codUsuarioCad?: string;
    dataHoraCad?: string;

}

export interface RelatorioAtendimentoPecaStatusData extends Meta {
    items: RelatorioAtendimentoPecaStatus[]
};

export interface RelatorioAtendimentoPecaStatusParameters extends QueryStringParameters {
    codRatpecasStatus?: number;
};
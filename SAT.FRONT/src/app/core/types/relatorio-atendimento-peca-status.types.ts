import { Meta } from "./generic.types";

export class RelatorioAtendimentoPecaStatus {
    codRatpecasStatus?: number;
    descricao?: string;
    codUsuarioCad?: string;
    dataHoraCad?: string;

}
export interface RelatorioAtendimentoDetalheStatusData extends Meta {
    items: RelatorioAtendimentoPecaStatus[]
};


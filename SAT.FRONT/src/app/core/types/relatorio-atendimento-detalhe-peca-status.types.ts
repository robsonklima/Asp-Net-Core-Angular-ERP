import { Meta, QueryStringParameters } from "./generic.types";
import { RelatorioAtendimentoPecaStatus } from "./relatorio-atendimento-peca-status.types";
import { Usuario } from "./usuario.types";

export class RelatorioAtendimentoDetalhePecaStatus {
    codRATDetalhesPecasStatus?: number;
    codRATDetalhesPecas?: number ;
    codRatPecasStatus?: number;
    descricao?: string;
    codUsuarioCad?: string;
    dataHoraCad?: string;
    transportadora?: string;
    nroMinuta?: string;
    dataPrevisao?: string;
    dataEmbarque?: string;
    dataChegada?: string;
    nroNf?: string;
    relatorioAtendimentoPecaStatus?: RelatorioAtendimentoPecaStatus;
    usuario?: Usuario;
}
export interface RelatorioAtendimentoDetalhePecaStatusData extends Meta {
    items: RelatorioAtendimentoDetalhePecaStatus[]
};

export interface RelatorioAtendimentoDetalhePecaStatusParameters extends QueryStringParameters {
    codRATDetalhesPecasStatus?: number;
    codRATDetalhesPecas?: number;
    codRATDetalhesPecasIN?: string;
};

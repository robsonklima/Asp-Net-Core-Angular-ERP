import { Acao } from "./acao.types";
import { Causa } from "./causa.types";
import { Defeito } from "./defeito.types";
import { GrupoCausa } from "./grupo-causa.types";
import { RelatorioAtendimentoDetalhePeca } from "./relatorio-atendimento-detalhe-peca.type";
import { TipoCausa } from "./tipo-causa.types";
import { TipoServico } from "./tipo-servico.types";

export class RelatorioAtendimentoDetalhe
{
    codRATDetalhe?: number;
    codRAT?: number;
    codOS?: number;
    tipoCausa?: TipoCausa;
    grupoCausa?: GrupoCausa;
    defeito?: Defeito;
    causa?: Causa;
    acao?: Acao;
    tipoServico?: TipoServico;
    codTipoCausa?: number;
    codGrupoCausa?: number;
    codDefeito?: number;
    codCausa?: number;
    codAcao?: number;
    codServico?: number;
    codUsuarioCad?: string;
    dataHoraCad?: string;
    dataHoraManut?: string;
    codModulo?: number;
    codSubModulo?: number;
    relatorioAtendimentoDetalhePecas?: RelatorioAtendimentoDetalhePeca[];
    removido: boolean;
}
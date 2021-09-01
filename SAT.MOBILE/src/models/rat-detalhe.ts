import { TipoServico } from "./tipo-servico";
import { TipoCausa } from "./tipo-causa";
import { GrupoCausa } from "./grupo-causa";
import { Defeito } from "./defeito";
import { Causa } from "./causa";
import { Acao } from "./acao";
import { Peca } from "./peca";

export class RatDetalhe {
	codRatDetalhe?: number;
    tipoCausa?: TipoCausa;
    tipoServico?: TipoServico;
    grupoCausa?: GrupoCausa;
    causa: Causa;
    defeito: Defeito;
    acao: Acao;
    pecas?: Peca[];
    protocoloStn?: string;
}
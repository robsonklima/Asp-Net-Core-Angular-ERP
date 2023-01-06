import { Meta, QueryStringParameters } from "./generic.types";
import { OrdemServicoSTNOrigem } from "./ordem-servico-stn-origem.types";
import { OrdemServico } from "./ordem-servico.types";
import { Usuario } from "./usuario.types";

export interface OrdemServicoSTN {
    codAtendimento: number;
    codOS: number;
    dataHoraAberturaSTN: string;
    dataHoraFechamentoSTN: string | null;
    codStatusSTN: number;
    codTipoCausa: string;
    codGrupoCausa: string;
    codDefeito: string;
    codCausa: string;
    codAcao: number | null;
    codTecnico: string;
    codUsuarioCad: string;
    codUsuarioManut: string;
    codOrigemChamadoSTN: number | null;
    indAtivo: number | null;
    numReincidenciaAoAssumir: number | null;
    dataHoraManut: string | null;
    numTratativas: number | null;
    indEvitaPendencia: number | null;
    indPrimeiraLigacao: number | null;
    nomeSolicitante?: string;
    obsSistema?: string;
    ordemServico?: OrdemServico;
    ordemServicoSTNOrigem?: OrdemServicoSTNOrigem;
    usuario: Usuario;
}

export interface OrdemServicoSTNData extends Meta
{
    items: OrdemServicoSTN[];
};

export interface OrdemServicoSTNParameters extends QueryStringParameters
{
    codClientes?: string;
    codOS?: number;
    codAtendimento?: number;
};
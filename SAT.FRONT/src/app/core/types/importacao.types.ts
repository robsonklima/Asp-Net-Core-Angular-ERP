import { TipoIntervencaoConst} from './tipo-intervencao.types';


export interface ImportacaoAberturaOrdemServico {
    nomeFantasia: string;
    numSerie: string;
    numAgenciaBanco: string;
    dcPosto: string;
    defeitoRelatado: string;
    tipoIntervencao: string;
    numOSQuarteirizada: string;
    numOSCliente: string;
    codUsuarioCad: string;
    dataHoraCad?: string;
}

export enum ImportacaoEnum
{
    ATUALIZA_IMPLANTACAO,
    ABERTURA_CHAMADOS,
    CRIACAO_LOTES
}


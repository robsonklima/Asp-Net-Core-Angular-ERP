
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

export interface Importacao {
    id: number;
    importacaoLinhas: ImportacaoLinha[];
}

export interface ImportacaoLinha {
    importacaoColunas: ImportacaoColuna[];
    erro?: number;
    mensagem?: string;
}

export interface ImportacaoColuna {
    campo?: string;
    valor?: string;
}

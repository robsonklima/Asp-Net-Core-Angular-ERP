export interface Importacao {
    id: number;
    importacaoLinhas: ImportacaoLinha[];
}

export interface ImportacaoLinha {
    importacaoColuna: ImportacaoColuna[];
    erro?: number;
    mensagem?: string;
}

export interface ImportacaoColuna {
    campo?: string;
    valor?: string;
}

export enum ImportacaoEnum {
    INSTALACAO = 1,
    ORDEM_SERVICO = 2,
    EQUIPAMENTO_CONTRATO = 3,
    FERIADO = 4,
    INSTALACAO_PAGTO_INSTAL = 5,
    PROCESSO_REPARO = 6,
    ADENDO = 7
}

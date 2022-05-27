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

import { Meta, QueryStringParameters } from "./generic.types";

export interface ImportacaoConfiguracao {
    codImportacaoConf: number;
    codImportacaoTipo: number;
    importacaoTipo: ImportacaoTipo;
    titulo: string;
    propriedade: string;
    largura: number | null;
    tipoHeader: string;
    mascara: string;
    decimal: string;
    render: string;
}

export interface ImportacaoConfiguracaoData extends Meta {
    items: ImportacaoConfiguracao[];
}

export interface ImportacaoTipo {
    codImportacaoTipo: number;
    nomeTipo: string;
}

export interface ImportacaoTipoData {
    items: ImportacaoTipo[];
}

export interface ImportacaoTipoParameters {
    codImportacaoTipo?: number;
    nomeTipo?: string;
    indAtivo?: number;
}

export interface ImportacaoConfiguracaoParameters extends QueryStringParameters {
    codImportacaoTipo?: number;
    titulo?: string;
}
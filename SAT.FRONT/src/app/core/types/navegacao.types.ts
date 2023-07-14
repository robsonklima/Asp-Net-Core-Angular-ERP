import { Meta, QueryStringParameters } from "./generic.types";

export interface Navegacao {
    codNavegacao: number;
    codNavegacaoPai: number;
    title: string;
    subtitle: string,
    translate: string;
    type: string;
    icon: string;
    ordem: number;
    link: string;
    children: Navegacao[];
    indAtivo: number;
    id: string;
}

export interface NavegacaoData extends Meta {
    items: Navegacao[];
};

export interface NavegacaoParameters extends QueryStringParameters { 
    indAtivo?: number;
    codNavegacao?: number;
    codNavegacaoPai?: number;
};
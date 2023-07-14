import { Meta, QueryStringParameters } from "./generic.types";
import { Navegacao } from "./navegacao.types";
import { Perfil } from "./perfil.types";
import { Setor } from "./setor.types";

export interface NavegacaoConfiguracao {
    codNavegacao: number;
    codNavegacaoConfiguracao: number;
    codSetor: number;
    codPerfil: number;
    navegacao: Navegacao;
    setor: Setor;
    perfil: Perfil;
}

export interface NavegacaoConfiguracaoData extends Meta {
    items: NavegacaoConfiguracao[];
};

export interface NavegacaoConfiguracaoParameters extends QueryStringParameters {
    codPerfil?: number;
    codSetor?: number;
    codNavegacao?: number;
};

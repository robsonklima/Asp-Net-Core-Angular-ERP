import { Meta, QueryStringParameters } from "./generic.types";

export class Perfil {
    codPerfil: number;
    nomePerfil: string;
    descPerfil: string;
    indResumo?: number;
    codSistema?: number;
    indAbreChamado?: number;
}

export interface PerfilData extends Meta {
    items: Perfil[];
};

export interface PerfilParameters extends QueryStringParameters {
    codPerfil?: number;
};

export enum PerfilEnum {
    ADMINISTRADOR = 3,
    FILIAL_COORDENADOR = 5,
    FINANCEIRO_COORDENADOR = 16,
    PV_COORDENADOR_DE_CONTRATO = 29, 
    CLIENTE = 34,
    CLIENTE_BASICO = 40,
    FINANCEIRO_ADMINISTRATIVO = 59,
    PONTO_FINANCEIRO = 65,
    FINANCEIRO_COORDENADOR_PONTO = 70,
    FILIAIS_SUPERVISOR = 75,
    CLIENTE_BASICO_S_ABERTURA = 80,
    FILIAL_LIDER_C_FUNCOES_COORD = 82,
    CLIENTE_CORREIOS = 87,
    CLIENTE_BASICO_C_RESTRICOES = 90,
    CLIENTE_BASICO_BIOMETRIA = 93,    
};
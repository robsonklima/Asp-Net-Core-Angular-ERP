import { Meta, QueryStringParameters } from "./generic.types";

export class Perfil
{
    codPerfil: number;
    nomePerfil: string;
    descPerfil: string;
    indResumo?: number;
    codSistema?: number;
    indAbreChamado?: number;
    indAtivo?: number;
}

export interface PerfilData extends Meta
{
    items: Perfil[];
};

export interface PerfilParameters extends QueryStringParameters
{
    codPerfil?: number;
    indAtivo?: number;
};

export enum PerfilEnum {
        ADM_DO_SISTEMA = 3,
        PV_COORDENADOR_DE_CONTRATO = 29,
        FILIAL_TECNICO_DE_CAMPO = 35,
        CLIENTE_AVANÇADO = 33,
    	CLIENTE_BASICO = 81,
        CLIENTE_INTERMEDIARIO = 93,
        CONTABILIDADE_PAGAMENTOS = 66,
        PARCEIROS = 71,
        FILIAL_SUPORTE_TÉCNICO_CAMPO = 79,
        RASTREAMENTO = 83,
        ANALISTA = 100,
    	ASSISTENTE = 107,
    	LIDER = 108,
    	COORDENADOR = 103,
    	SUPERVISOR = 104,
    	TECNICO = 105,
        TECNICO_OPERACOES = 106,
        INSPETOR = 109
}
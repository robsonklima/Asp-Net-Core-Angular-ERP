import { Meta, QueryStringParameters } from "./generic.types";

export class Setor
{
    codSetor: number;
    nomeSetor: string;
    abreviacao: string;
    indAtivo?: number;
}

export interface SetorData extends Meta
{
    items: Setor[];
};

export interface SetorParameters extends QueryStringParameters
{
    codSetor?: number;
};

export enum SetorEnum {
    COORDENACAO_DE_CONTRATOS = 1,
    GERENCIA = 2,
    LABORATORIO_TECNICO = 3,
    LOGISTICA = 4,
    OPERACAO_DE_CAMPO = 5,
    SISTEMA_DE_ASSISTENCIA_TECNICA = 6,
    SUPORTE_TECNICO = 7,
    CENTRO_TECNICO_OPERACIONAL = 8,
    FINANCEIRO = 9,
    FISCAL = 10,
    HELPDESK_NOC = 11,
    CLIENTE = 12,
    PRESTADOR_DE_SERVICO = 13,
    ENGENHARIA_GARANTIA_E_QUALIDADE = 14
}
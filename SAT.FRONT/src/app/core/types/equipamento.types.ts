import { QueryStringParameters } from "./generic.types";

export class Equipamento
{
    codEquip: number;
    codEEquip: string;
    nomeEquip: string;
    codGrupoEquip: number;
    codTipoEquip: number;
}

export interface EquipamentoData
{
    items: Equipamento[],
    totalCount: number
};

export interface EquipamentoParameters extends QueryStringParameters
{
    codGrupo?: number;
    codTipo?: number;
    codClientes?: string;
    filter?: any;
    filterType?: EquipamentoFilterEnum;
};

export enum EquipamentoFilterEnum
{
    FILTER_CHAMADOS = 1
}
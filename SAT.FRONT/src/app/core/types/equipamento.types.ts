import { QueryStringParameters } from "./generic.types";

export class Equipamento
{
    codEquip: number;
    codEEquip: string;
    nomeEquip: string;
}

export interface EquipamentoData
{
    items: Equipamento[],
    totalCount: number
};

export interface EquipamentoParameters extends QueryStringParameters 
{
    codGrupo?: string;
    codTipo?: string;
};
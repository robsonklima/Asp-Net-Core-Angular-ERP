import { Meta, QueryStringParameters } from "./generic.types";

export class Equipamento {
    codEquip: number;
    codEEquip: string;
    nomeEquip: string;
}

export interface EquipamentoData {
    equipamentos: Equipamento[],
    totalCount: number;
};

export interface EquipamentoParameters extends QueryStringParameters {};
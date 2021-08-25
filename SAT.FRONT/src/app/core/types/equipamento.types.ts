import { Meta, QueryStringParameters } from "./generic.types";

export class Equipamento {
    codEquip: number;
    codEEquip: string;
    nomeEquip: string;
}

export interface EquipamentoData {
    items: Equipamento[]
};

export interface EquipamentoParameters extends QueryStringParameters {};
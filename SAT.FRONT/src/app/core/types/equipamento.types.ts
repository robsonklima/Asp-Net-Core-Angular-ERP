import { QueryStringParameters } from "./generic.types";
import { GrupoEquipamento } from "./grupo-equipamento.types";
import { TipoEquipamento } from "./tipo-equipamento.types";

export class Equipamento
{
    codEquip: number;
    codEEquip: string;
    nomeEquip: string;
    codGrupoEquip: number;
    codTipoEquip: number;
    descEquip: string;
    tipoEquipamento: TipoEquipamento;
    grupoEquipamento: GrupoEquipamento;
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
    codEquip?: number;
    filter?: any;
    filterType?: EquipamentoFilterEnum;
    CodTipoEquips?: string;
};

export enum EquipamentoFilterEnum
{
    FILTER_CHAMADOS = 1
}
import { Causa } from "./causa.types";
import { Equipamento } from "./equipamento.types";
import { Meta, QueryStringParameters } from "./generic.types";
import { TipoEquipamento } from "./tipo-equipamento.types";

export class EquipamentoModulo {
    codConfigEquipModulos: number;
    codEquip: number;
    codECausa: string;
    codUsuarioCad: string;
    codUsuarioManut: string;
    dataHoraCad?: string;
    dataHoraManut?: string;
    equipamento: Equipamento;
    tipoEquipamento: TipoEquipamento;
    causa: Causa;
    indAtivo?: number;
}

export interface EquipamentoModuloData extends Meta {
    items: EquipamentoModulo[]
};

export interface EquipamentoModuloParameters extends QueryStringParameters {
    codEquip?: number;
    codTipoEquip?: number;
    indAtivo?: number;
};
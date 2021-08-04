import { Meta, QueryStringParameters } from "./generic.types";
import { TipoEquipamento } from "./tipo-equipamento.types";

export interface GrupoEquipamento {
    codGrupoEquip: number;
    codTipoEquip: number;
    codEGrupoEquip: string;
    nomeGrupoEquip: string;
    tipoEquipamento: TipoEquipamento;
}

export interface GrupoEquipamentoData extends Meta {
    gruposEquipamento: GrupoEquipamento[];
};

export interface GrupoEquipamentoParameters extends QueryStringParameters {
    codGrupoEquip?: number;
};
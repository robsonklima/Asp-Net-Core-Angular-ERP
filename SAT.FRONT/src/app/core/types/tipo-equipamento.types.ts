import { Meta, QueryStringParameters } from "./generic.types";

export interface TipoEquipamento {
  codTipoEquip: number;
  codETipoEquip: string;
  nomeTipoEquip: string;
}

export interface TipoEquipamentoData extends Meta {
  tiposEquipamento: TipoEquipamento[];
};

export interface TipoEquipamentoParameters extends QueryStringParameters {
  codTipoEquip?: number;
};

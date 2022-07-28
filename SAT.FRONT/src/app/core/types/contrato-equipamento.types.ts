import { Contrato } from "./contrato.types";
import { Equipamento } from "./equipamento.types";
import { Meta, QueryStringParameters } from "./generic.types";
import { GrupoEquipamento } from "./grupo-equipamento.types";
import { TipoEquipamento } from "./tipo-equipamento.types";

export class ContratoEquipamento {
    codContrato: number;
    contrato: Contrato;
    codTipoEquip: number;
    tipoEquipamento: TipoEquipamento;
    codGrupoEquip: number;
    grupoEquipamento: GrupoEquipamento;
    codEquip: number;
    equipamento: Equipamento;
    qtdEquip: number;
    vlrUnitario: number;
    codTipoGarantia: number;
    dataRecDM?: any;
    vlrInstalacao?: any;
    indGarPriSem?: any;
    indGarSegSem?: any;
    indGarTerSem?: any;
    indGarQuaSem?: any;
    indGarPriQui?: any;
    indGarSegQui?: any;
    qtdDiaGarantia: number;
    qtdLimDiaEnt: number;
    qtdLimDiaIns?: any;
    codContratoEquipDataGar?: any;
    codContratoEquipDataEnt: number;
    codContratoEquipDataIns?: any;
    dataGar?: any;
    percIPI?: any;
    percValorEnt: number;
    percValorIns: number;
    dataInicioMTBF?: any;
    dataFimMTBF?: any;
    codUsuarioCad: string;
    dataHoraCad: Date;
    codUsuarioManut?: any;
    dataHoraManut?: any;
    codMagnus: string;
}

export interface ContratoEquipamentoData extends Meta {
    items: ContratoEquipamento[]
};

export interface ContratoEquipamentoParameters extends QueryStringParameters {
    codContrato?: number;
    codContratos?: string;
    codGrupoEquip?: number;
    codTipoEquip?: number;
    codEquip?: number;
};
import { ServicosComponent } from "app/modules/main/default/servicos/servicos.component";
import { AcordoNivelServico } from "./acordo-nivel-servico.types";
import { Contrato } from "./contrato.types";
import { Equipamento } from "./equipamento.types";
import { Meta, QueryStringParameters } from "./generic.types";
import { GrupoEquipamento } from "./grupo-equipamento.types";
import { TipoEquipamento } from "./tipo-equipamento.types";

export class ContratoServico {
    codContrato: number;
    contrato?: Contrato;
    codSLA: number;
    sla?: AcordoNivelServico;
    codTipoEquip: number;
    tipoEquipamento?: TipoEquipamento;
    codGrupoEquip: number;
    grupoEquipamento?: GrupoEquipamento;
    codEquip: number;
    equipamento?: Equipamento;
    valor: number;
    codUsuarioCad: string;
    dataHoraCad: Date;
    codUsuarioManut?: any;
    dataHoraManut?: any;
    codUsuarioCad_DEL: string;
    dataHoraCad_DEL: Date;
    codUsuarioManut_DEL?: any;
    dataHoraManut_DEL?: any;


}

export interface ContratoServicoData extends Meta {
    items: ContratoServico[]
};

export interface ContratoServicoParameters extends QueryStringParameters {
    codContrato?: number;
    codContratoServico?: number;
};
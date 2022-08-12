import { ServicosComponent } from "app/modules/main/default/servicos/servicos.component";
import { AcordoNivelServico } from "./acordo-nivel-servico.types";
import { Contrato } from "./contrato.types";
import { Equipamento } from "./equipamento.types";
import { Meta, QueryStringParameters } from "./generic.types";
import { GrupoEquipamento } from "./grupo-equipamento.types";
import { TipoEquipamento } from "./tipo-equipamento.types";
import { TipoServico } from "./tipo-servico.types";

export class ContratoServico {
    codContratoServico: number;
    codContrato: number;
    contrato?: Contrato;
    codServico: number;
    tipoServico: TipoServico;
    codSLA: number;
    contratoSLA?: AcordoNivelServico;
    codTipoEquip: number;
    tipoEquipamento?: TipoEquipamento;
    codGrupoEquip: number;
    grupoEquipamento?: GrupoEquipamento;
    codEquip: number;
    equipamento?: Equipamento;
    valor: number;
    codUsuarioCad: string;
    dataHoraCad: string;
    codUsuarioManut?: any;
    dataHoraManut?:  string;
    codUsuarioCad_DEL?: string;
    dataHoraCad_DEL?: string;
    codUsuarioManut_DEL?: any;
    dataHoraManut_DEL?: string;


}

export interface ContratoServicoData extends Meta {
    items: ContratoServico[]
};

export interface ContratoServicoParameters extends QueryStringParameters {
    codContrato?: number;
    codContratoServico?: number;
};
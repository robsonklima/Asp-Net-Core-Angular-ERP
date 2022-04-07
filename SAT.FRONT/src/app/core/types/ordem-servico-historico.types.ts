import { Autorizada } from "./autorizada.types";
import { EquipamentoContrato } from "./equipamento-contrato.types";
import { Meta, QueryStringParameters } from "./generic.types";
import { LocalAtendimento } from "./local-atendimento.types";
import { StatusServico } from "./status-servico.types";
import { Tecnico } from "./tecnico.types";
import { TipoIntervencao } from "./tipo-intervencao.types";
import { Usuario } from "./usuario.types";

export interface OrdemServicoHistorico {
    codHistOS: number;
    codOS: number;
    codTipoIntervencao: number;
    codStatusServico: number;
    codPosto: number;
    codEquipContrato: number;
    codTecnico: number;
    dataHoraCad: string;
    codCliente: number;
    codUsuarioManutencao: string;
    codAutorizada: number;
    usuario: Usuario;
    localAtendimento: LocalAtendimento;
    tecnico: Tecnico;
    autorizda: Autorizada;
    equipamentoContrato: EquipamentoContrato;
    tipoIntervencao: TipoIntervencao;
    statusServico: StatusServico;
}

export interface OrdemServicoHistoricoData extends Meta {
    items: OrdemServicoHistorico[];
};

export interface OrdemServicoHistoricoParameters extends QueryStringParameters {
    codOS: number;
};

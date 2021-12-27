import { Meta, QueryStringParameters } from "./generic.types";

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
}

export interface OrdemServicoHistoricoData extends Meta {
    items: OrdemServicoHistorico[];
};

export interface OrdemServicoHistoricoParameters extends QueryStringParameters {
    codOS: number;
};

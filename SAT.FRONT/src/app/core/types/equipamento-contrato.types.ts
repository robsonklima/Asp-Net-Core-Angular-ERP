import { Autorizada } from "./autorizada.types";
import { Cliente } from "./cliente.types";
import { ContratoEquipamento } from "./contrato-equipamento.types";
import { Contrato } from "./contrato.types";
import { Equipamento } from "./equipamento.types";
import { Filial } from "./filial.types";
import { Meta, QueryStringParameters } from "./generic.types";
import { GrupoEquipamento } from "./grupo-equipamento.types";
import { LocalAtendimento } from "./local-atendimento.types";
import { RegiaoAutorizada } from "./regiao-autorizada.types";
import { Regiao } from "./regiao.types";
import { TipoEquipamento } from "./tipo-equipamento.types";

export class EquipamentoContrato {
    codEquipContrato: number;
    codContrato: number;
    contrato: Contrato;
    codTipoEquip: number;
    tipoEquipamento: TipoEquipamento;
    codGrupoEquip: number;
    grupoEquipamento: GrupoEquipamento;
    codEquip: number;
    equipamento: Equipamento;
    codSLA: number;
    acordoNivelServico?: any;
    numSerie: string;
    numSerieCliente: string;
    codCliente: number;
    cliente: Cliente;
    codPosto: number;
    localAtendimento: LocalAtendimento;
    codRegiao: number;
    regiao: Regiao;
    codAutorizada: number;
    autorizada: Autorizada;
    regiaoAutorizada: RegiaoAutorizada;
    codFilial: number;
    filial: Filial;
    distanciaPatRes: number;
    indGarantia: number;
    dataInicGarantia?: any;
    dataFimGarantia?: any;
    indReceita: number;
    valorReceita: number;
    indRepasse: number;
    indRepasseIndividual: number;
    valorRepasse?: any;
    valorDespesa: number;
    valorDespesaInstalacao: number;
    indInstalacao: number;
    indAtivo: number;
    dataAtivacao: string;
    dataDesativacao?: string;
    codUsuarioCad: string;
    dataHoraCad: string;
    codUsuarioManut: string;
    dataHoraManut: string;
    origem: string;
    horaDispInicio: string;
    horaDispFim: string;
    indDispSabado: number;
    indDispDomingo: number;
    indDisp24H: number;
    codEquipCliente: string;
    dataCadastro?: any;
    codUsuarioCadastro: string;
    dataManutencao: string;
    codUsuarioManutencao: string;
    indSemat: number;
    pontoEstrategico: string;
    indRHorario: number;
    indRAcesso: number;
    codTipoLocalAtendimento?: any;
    indVerao?: any;
    indPAE?: any;
    indRetrofit?: any;
    dataRetrofit1?: any;
    atmId: string;
    dataRetrofit2?: any;
    indRetrofit2?: any;
    indRetrofit3?: any;
    dataRetrofit3?: any;
    indMTBF4?: any;
    numEtiquetaEquip?: any;
    dataRelatorio?: any;
    indRelatorio?: any;
    codBMP?: any;
    sequencia?: any;
    indMecanismo?: any;
    codDispBBCriticidade?: any;
    contratoEquipamento: ContratoEquipamento
}

export interface EquipamentoContratoData extends Meta {
    items: EquipamentoContrato[];
};

export interface EquipamentoContratoParameters extends QueryStringParameters {
    codEquipContrato?: number;
    codContrato?: number;
    codPosto?: number;
    indAtivo?: number;
    codFilial?: number;
    codClientes?: string;
};

export enum PontoEstrategicoEnum {
    NAO = 0,
    VIP = 1,
    VIT = 2
}
import { Meta, QueryStringParameters } from "./generic.types";

export class TipoIntervencao {
	codTipoIntervencao: number;
	codETipoIntervencao: string;
	nomTipoIntervencao: string;
	calcPreventivaIntervenc: number;
	verificaReincidenciaInt: number;
	codTraducao: number;
	indAtivo: number;
}

export interface TipoIntervencaoData extends Meta {
	items: TipoIntervencao[];
};

export interface TipoIntervencaoParameters extends QueryStringParameters {
	codTipoIntervencao?: number;
	indAtivo?: number;
};

export enum TipoIntervencaoEnum {
	CORRETIVA = 2,
	ORCAMENTO = 5,
	AUTORIZACAO_DESLOCAMENTO = 14,
	ORC_APROVADO = 17,
	ORC_REPROVADO = 18,
	ORC_PEND_APROVACAO_CLIENTE = 19,
	ORC_PEND_FILIAL_DETALHAR_MOTIVO = 20,
	ALTERACAO_DE_ENGENHARIA = 1,
	COFRE = 32,
	PREVENTIVA_GERENCIAL = 28,
	PREVENTIVA = 6
};

export const TipoIntervencaoConst =[
	{
	  id: 1,
	  name: "ALTERAÇÃO DE ENGENHARIA"
	},	{
	  id: 2,
	  name: "CORRETIVA"
	},	{
	  id: 3,
	  name: "DESINSTALAÇÃO"
	},	{
	  id: 4,
	  name: "INSTALAÇÃO"
	},	{
	  id: 5,
	  name: "ORÇAMENTO"
	},	{
	  id: 6,
	  name: "PREVENTIVA"
	},	{
	  id: 7,
	  name: "REINSTALAÇÃO"
	},	{
	  id: 10,
	  name: "INSPEÇÃO TÉCNICA"
	},	{
	  id: 11,
	  name: "REMANEJAMENTO"
	},	{
	  id: 13,
	  name: "TREINAMENTO"
	},	{
	  id: 14,
	  name: "AUTORIZAÇÃO DESLOCAMENTO"
	},	{
	  id: 17,
	  name: "ORÇ APROVADO"
	},	{
	  id: 18,
	  name: "ORÇ REPROVADO"
	},	{
	  id: 19,
	  name: "ORÇ PEND APROVAÇÃO CLIENTE"
	},	{
	  id: 20,
	  name: "ORÇ PEND FILIAL DETALHAR MOTIVO"
	},	{
	  id: 22,
	  name: "CORRETIVA-POS REINCIDENTES"
	},	{
	  id: 23,
	  name: "HELPDESK"
	},	{
	  id: 24,
	  name: "TROCA VELOH C"
	},	{
	  id: 25,
	  name: "ATUALIZAÇÃO"
	},	{
	  id: 26,
	  name: "LAUDO TÉCNICO"
	},	{
	  id: 27,
	  name: "HELP DESK DSS"
	},	{
	  id: 28,
	  name: "PREVENTIVA GERENCIAL"
	},	{
	  id: 29,
	  name: "LISTA ATUALIZAÇÃO EQUIPAMENTO"
	},	{
	  id: 30,
	  name: "VANDALISMO"
	},	{
	  id: 31,
	  name: "MANUTENÇÃO GERENCIAL"
	},	{
	  id: 32,
	  name: "COFRE"
	}
   ];
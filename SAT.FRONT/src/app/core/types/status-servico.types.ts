import { Meta, QueryStringParameters } from "./generic.types";

export class StatusServico {
	codStatusServico: number;
	nomeStatusServico: string;
	indPendente: number;
	indEncerrado: number;
	corFundo: string;
	corFonte: string;
	tamFonte: number;
	indNegrito: number;
	abrev: string;
	indServico: number;
	codTraducao: number;
	indAtivo: number;
	indLiberadoOsbloqueado: number;
}

export interface StatusServicoData extends Meta {
	items: StatusServico[];
};

export interface StatusServicoParameters extends QueryStringParameters {
	codStatusServico?: number;
	indAtivo?: number;
};

export const statusServicoConst = {
	ABERTO: 1,
	FECHADO: 3,
	CANCELADO: 2,
	TRANSFERIDO: 8
}
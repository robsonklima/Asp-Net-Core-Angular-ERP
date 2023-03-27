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
	codStatusServicos?: string;
};

export const statusServicoConst = {
	ABERTO: 1,
	FECHADO: 3,
	CANCELADO: 2,
	PECAS_LIBERADAS: 6,
	PECAS_PENDENTES: 7,
	TRANSFERIDO: 8,
	PECA_EM_TRANSITO: 9,
    PECA_FALTANTE: 10,
    PECA_SEPARADA: 11,
	ORCAMENTO: 4
}


        
        
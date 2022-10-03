import { Meta, QueryStringParameters } from "./generic.types";

export interface StatusServicoSTN {
    codStatusServicoSTN: number;
    descStatusServicoSTN: string;
    indAtivo: number;
}

export interface StatusServicoSTNData extends Meta {
    items: StatusServicoSTN[];
};

export interface StatusServicoSTNParameters extends QueryStringParameters {
    
};

export const statusServicoSTNConst = {
	ABERTO: 1,
    AGUARD_ATENDIMENTO: 2,
    FECHADO: 3,
    CANCELADO: 4
}
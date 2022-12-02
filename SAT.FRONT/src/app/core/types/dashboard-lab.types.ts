import { QueryStringParameters } from "./generic.types";

export interface ViewDashboardLabRecebidosReparados {
    mes: number;
    ano: number;
    mesExtenso: string;
    tipo: string;
    qtd: number;
}

export interface ViewDashboardLabTopFaltantes {
    codPeca: number;
    qtd: number;
    codMagnus: string;
    nomePeca: string;
    nomePecaAbrev: string;
    mediaReparo: string;
    mediaHorasUteis: number;
    mediaHorasUteisConv: string;
    qtdhoras: number;
    qtdReparada: number;
}

export interface DashboardLabParameters extends QueryStringParameters {
    ano?: number;
};
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
    qtdFaltante: number;
    codMagnus: string;
    nomePeca: string;
    nomePecaAbrev: string;
    qtdReparada: number;
    qtdPendente: number;
    qtdEmReparo: number;
}

export interface ViewDashboardLabTopTempoMedioReparo {
    codPeca: number;
    nomePeca: string;
    codMagnus: string;
    nomePecaAbrev: string;
    qtdReparada: number;
    tempoMedioReparo: number;
}

export interface ViewDashboardLabProdutividadeTecnica {
    nomeUsuario: string;
    eletronico: number;
    mecanico: number;
    total: number;
}

export interface DashboardLabParameters extends QueryStringParameters {
    ano?: number;
};
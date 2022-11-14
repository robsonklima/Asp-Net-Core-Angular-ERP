import { Meta, QueryStringParameters } from "./generic.types";
import { ORStatus } from "./or-status.types";
import { Peca } from "./peca.types";

export interface ORItemInsumo {
    codORItemInsumo?: number;
    codORItem?: number;
    dataHoraOritem?: string;
    codOR?: number;
    codPeca?: number;
    codStatus?: number;
    quantidade?: number;
    numSerie?: string;
    codTipoOR?: number;
    codOS?: number;
    codCliente?: number;
    codTecnico?: string;
    defeitoRelatado?: string;
    relatoSolucao?: string;
    codDefeito?: string;
    codAcao?: number;
    codSolucao?: number;
    indConfLog?: number;
    indConfLab?: number;
    indAtivo: number;
    codUsuarioCad?: string;
    dataHoraCad?: string;
    codStatusPendente?: number;
    indLiberacao?: number;
    orStatus?: ORStatus;
    peca?: Peca;
}

export interface ORItemInsumoData extends Meta {
    items: ORItemInsumo[];
};

export interface ORItemInsumoParameters extends QueryStringParameters {
    codORItem: number;
    indAtivo?: number;
    codPeca?: number;
}
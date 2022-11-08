import { Meta, QueryStringParameters } from "./generic.types";
import { ORStatus } from "./or-status.types";
import { Peca } from "./peca.types";

export interface ORItemInsumo {
    codORItemInsumo: number;
    codORItem: number | null;
    dataHoraOritem: string | null;
    codOR: number | null;
    codPeca: number | null;
    codStatus: number | null;
    quantidade: number | null;
    numSerie: string;
    codTipoOR: number | null;
    codOS: number | null;
    codCliente: number | null;
    codTecnico: string;
    defeitoRelatado: string;
    relatoSolucao: string;
    codDefeito: string;
    codAcao: number | null;
    codSolucao: number | null;
    indConfLog: number | null;
    indConfLab: number | null;
    indAtivo: number;
    codUsuarioCad: string;
    dataHoraCad: string;
    codStatusPendente: number | null;
    indLiberacao: number | null;
    orStatus: ORStatus;
    peca: Peca;
}

export interface ORItemInsumoData extends Meta {
    items: ORItemInsumo[];
};

export interface ORItemInsumoParameters extends QueryStringParameters {
    codORItem: number;
    indAtivo: number;
}
import { Meta, QueryStringParameters } from "./generic.types";

export interface ORCheckList {
    codORChecklist: number;
    descricao: string;
    codMagnus: string;
    codPeca: number;
    indAtivo: number;
    codUsuarioCad: string;
    dataHoraCad: string;
    codOritem: number | null;
    codORCheckListItem: string;
    tempoReparo: number | null;
}

export interface ORCheckListData extends Meta {
    items: ORCheckList[];
};

export interface ORCheckListParameters extends QueryStringParameters {

}
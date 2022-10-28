import { Meta, QueryStringParameters } from "./generic.types";

export interface ORCheckListItem {
    codORCheckListItem: number;
    codMagnus: string;
    descricao: string;
    nivel: string;
    acao: string;
    parametro: string;
    realizacao: string;
    pnMei: string;
    codORCheckList: number | null;
    passoObrigatorio: number | null;
}

export interface ORCheckListItemData extends Meta {
    items: ORCheckListItem[];
};

export interface ORCheckListItemParameters extends QueryStringParameters {

}
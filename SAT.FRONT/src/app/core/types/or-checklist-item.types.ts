import { Meta, QueryStringParameters } from "./generic.types";
import { Peca } from "./peca.types";

export interface ORCheckListItem {
    codORCheckListItem: number;
    codMagnus: string;
    descricao: string;
    nivel: string;
    acao: string;
    parametro: string;
    realizacao: string;
    pn_Mei: string;
    codORCheckList: number | null;
    passoObrigatorio: number | null;
    peca: Peca;
}

export interface ORCheckListItemData extends Meta {
    items: ORCheckListItem[];
};

export interface ORCheckListItemParameters extends QueryStringParameters {

}
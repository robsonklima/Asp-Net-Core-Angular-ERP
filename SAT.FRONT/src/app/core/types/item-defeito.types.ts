import { Meta, QueryStringParameters } from "./generic.types";
import { ORDefeito } from "./or-defeito.types";
import { ORItem } from "./or-item.types";
import { Usuario } from "./usuario.types";

export interface ItemDefeito {
    codItemDefeito: number;
    codORItem: number;
    codDefeito: number;
    codTecnico: string;
    dataHoraCad: string;
    orItem?: ORItem;
    usuario?: Usuario;
    orDefeito?: ORDefeito;
}

export interface ItemDefeitoData extends Meta {
    items: ItemDefeito[];
};

export interface ItemDefeitoParameters extends QueryStringParameters {
    codORItem?: number;
    codItemDefeito?: number;
}
import { Meta, QueryStringParameters } from "./generic.types";
import { ORSolucao } from "./or-Solucao.types";
import { ORItem } from "./or-item.types";
import { Usuario } from "./usuario.types";

export interface ItemSolucao {
    codItemSolucao?: number;
    codORItem: number;
    codSolucao: number;
    codTecnico: string;
    dataHoraCad: string;
    orItem?: ORItem;
    usuario?: Usuario;
    orSolucao?: ORSolucao;
}

export interface ItemSolucaoData extends Meta {
    items: ItemSolucao[];
};

export interface ItemSolucaoParameters extends QueryStringParameters {
    codORItem?: number;
    codItemSolucao?: number;
    codSolucao?: number;
}
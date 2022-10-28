import { Meta, QueryStringParameters } from "./generic.types";
import { ORCheckListItem } from "./or-checklist-item.types";
import { Usuario } from "./usuario.types";

export interface ORCheckList {
    codORCheckList: number;
    descricao: string;
    codMagnus: string;
    codPeca: number;
    indAtivo: number;
    codUsuarioCad: string;
    dataHoraCad: string;
    codOritem: number | null;
    codORCheckListItem: string;
    tempoReparo: number | null;
    usuarioCadastro: Usuario;
    itens: ORCheckListItem[];
}

export interface ORCheckListData extends Meta {
    items: ORCheckList[];
};

export interface ORCheckListParameters extends QueryStringParameters {
    codORCheckList?: number | null;
    descricao?: string;
    codUsuariosCad?: string;
    dataHoraCadInicio?: string;
    dataHoraCadFim?: string;
}
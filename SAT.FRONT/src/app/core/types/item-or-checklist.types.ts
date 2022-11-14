import { Meta } from "@angular/platform-browser";
import { QueryStringParameters } from "./generic.types";
import { ORCheckListItem } from "./or-checklist-item.types";
import { ORCheckList } from "./or-checklist.types";
import { ORItem } from "./or-item.types";

export interface ItemXORCheckList {
    codItemChecklist?: number;
    codORItem: number;
    orItem?: ORItem;
    codORCheckList: number;
    orCheckList?: ORCheckList;
    codORCheckListItem: number;
    orCheckListItem?: ORCheckListItem;
    indAtivo: number;
    nivel: string;
}

export interface ItemXORCheckListData extends Meta {
    items: ItemXORCheckList[];
};

export interface ItemXORCheckListParameters extends QueryStringParameters {
    codItemChecklist?: number;
    codORItem?: number;
    codORCheckList?: number;
    codORCheckListItem?: number;
    indAtivo?: number;
    nivel?: string;
}
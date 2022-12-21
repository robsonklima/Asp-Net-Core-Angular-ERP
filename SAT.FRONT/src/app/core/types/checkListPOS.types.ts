import { CheckListPOSItens } from "./checkListPOS-itens.types copy";
import { Meta, QueryStringParameters } from "./generic.types";

export class CheckListPOS {
    codCheckListPOS : number;
    codOS : number ;
    codRAT ?: number;
    codCheckListPOSItens ?: number;
    checkListPOSItens ?: CheckListPOSItens[];
    
}

export interface CheckListPOSData extends Meta {
    items: CheckListPOS[]
};

export interface CheckListPOSParameters extends QueryStringParameters {
    codCheckListPOS?: number;
    codUsuario?: number;
};

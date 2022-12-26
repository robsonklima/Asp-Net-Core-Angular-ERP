import { Meta, QueryStringParameters } from "./generic.types";

export class CheckListPOS {
    codCheckListPOS?: number;
    codOS?: number;
    codRAT?: number;
    codCheckListPOSItens?: number;
}

export interface CheckListPOSData extends Meta {
    items: CheckListPOS[]
};

export interface CheckListPOSParameters extends QueryStringParameters {
    codCheckListPOS?: number;
    codRAT?: number;
    codOS?: number;
    codCheckListPOSItens?: number;
};

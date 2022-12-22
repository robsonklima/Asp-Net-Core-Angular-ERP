import { Cliente } from "./cliente.types";
import { Meta, QueryStringParameters } from "./generic.types";

export class CheckListPOSItens {
    codCheckListPOSItens : number;
    codCliente? : number ;
    descricao ?: string;
    indAtivo ?: number;
    indPadrao ?: number;
   
}

export interface CheckListPOSItensData extends Meta {
    items: CheckListPOSItens[]
};

export interface CheckListPOSItensParameters extends QueryStringParameters {
    codCheckListPOSItens?: number;
    codCliente?: number;
    codClientes?: string;
    indAtivo ?: number;
    indPadrao ?: number;
};

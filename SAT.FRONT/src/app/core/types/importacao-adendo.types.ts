import { Meta, QueryStringParameters } from "./generic.types";

export class ImportacaoAdendo {
    codAdendo : number;
    dataHoraCad : string;
    codUsuarioCad?: string;
    dataHoraProcessamento ?: string;
    indAtivo ?: number;
    codEquipContrato ?: number;
    nomeSla : string;
    indSemat : number;
    pontoEstrategico ?: number;
    indRHorario ?: number;
    indRAcesso ?: number;
    indPAE ?: number;
    nomeContrato : string;
    indAtivoEquip ?: number;
    distanciaKmPAT_Res ?: number;
    numAgenciaDC ?: string; 
    indInstalacao : number;
    horas_RAcesso ?: number;
    indReceita ?: number;
    indRepasse ?: number;   
}

export interface ImportacaoAdendoData extends Meta {
    items: ImportacaoAdendo[]
};

export interface ImportacaoAdendoParameters extends QueryStringParameters {
    codAdendo?: number;
};

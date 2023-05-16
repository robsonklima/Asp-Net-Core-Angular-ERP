import { Meta, QueryStringParameters } from "./generic.types";

export interface EquipamentoPOS {
    codEquipamentoPOS: number;
    numeroSerie: string;
    codEquip: number;
    codGrupoEquip: number;
    codTipoEquip: number;
    dataProducao: string;
    opPqm: number;
    idOpSerie: number;
    codStatusEquipamentoPOS: number;
    codTipoMidia: number;
    codEquipPqm: string;
    numeroSeriePqm: string;
    numeroLogico: string;
}

export interface EquipamentoPOSData extends Meta {
    items: EquipamentoPOS[],
};

export interface EquipamentoPOSParameters extends QueryStringParameters {

};
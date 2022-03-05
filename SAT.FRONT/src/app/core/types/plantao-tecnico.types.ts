import { Meta, QueryStringParameters } from "./generic.types";
import { Tecnico } from "./tecnico.types";

export class PlantaoTecnico
{
    codPlantaoTecnico: number;
    codTecnico: number;
    dataPlantao: string;
    indAtivo: number;
    dataHoraCad: string;
    codUsuarioCad: string;
    tecnico: Tecnico;
}

export interface PlantaoTecnicoData extends Meta
{
    items: PlantaoTecnico[];
};

export interface PlantaoTecnicoParameters extends QueryStringParameters
{
    codTecnico?: number;
    indAtivo?: number;
    nome?: string;
};
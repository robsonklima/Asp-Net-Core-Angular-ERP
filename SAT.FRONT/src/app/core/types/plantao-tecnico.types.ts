import { Meta, QueryStringParameters } from "./generic.types";
import { Regiao } from "./regiao.types";
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
    plantaoRegioes: PlantaoTecnicoRegiao[];
    plantaoClientes: PlantaoTecnicoCliente[];
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

export class PlantaoTecnicoRegiao
{
    codPlantaoTecnicoRegiao: number;
    codPlantaoTecnico: number;
    codRegiao: number;
    indAtivo: number;
    dataHoraCad: string;
    codUsuarioCad: string;
    regiao: Regiao;
}

export interface PlantaoTecnicoRegiaoData extends Meta
{
    items: PlantaoTecnicoRegiao[];
};

export interface PlantaoTecnicoRegiaoParameters extends QueryStringParameters
{
};

export class PlantaoTecnicoCliente
{
    codPlantaoTecnicoCliente: number;
    codPlantaoTecnico: number;
    codCliente: number;
    indAtivo: number;
    dataHoraCad: string;
    codUsuarioCad: string;
    cliente: PlantaoTecnicoCliente;
}

export interface PlantaoTecnicoClienteData extends Meta
{
    items: PlantaoTecnicoCliente[];
};

export interface PlantaoTecnicoClienteParameters extends QueryStringParameters
{
};
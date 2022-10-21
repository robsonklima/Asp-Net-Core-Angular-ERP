import { Meta, QueryStringParameters } from "./generic.types";

export interface OrdemServicoSTNOrigem {
    codOrigemChamadoSTN: number;
    descOrigemChamadoSTN: string;
    indAtivo: number;
}

export interface OrdemServicoSTNOrigemData extends Meta
{
    items: OrdemServicoSTNOrigem[];
};
export interface OrdemServicoSTNOrigemParameters extends QueryStringParameters
{
    codOrigemChamadoSTN?: number;
    descOrigemChamadoSTN?: string;
    indAtivo?: number;
};
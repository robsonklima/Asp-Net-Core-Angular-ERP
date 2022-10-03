import { Meta, QueryStringParameters } from "./generic.types";

export interface OrdemServicoSTNOrigem {
    codOrigemChamadoSTN: number;
    descOrigemChamadoSTN: string;
    indAtivo: number | null;
}

export interface OrdemServicoSTNOrigemData extends Meta
{
    items: OrdemServicoSTNOrigem[];
};

export interface OrdemServicoSTNParameters extends QueryStringParameters
{
    
};
import { Meta, QueryStringParameters } from "./generic.types";
import { OrdemServicoSTNOrigem } from "./ordem-servico-stn-origem.types";
import { OrdemServico } from "./ordem-servico.types";

export interface ProtocoloSTN {
    CodProtocoloSTN: number;
    CodRAT: number;
    CodOS: number;
    NumProtocolo: number;
}

export interface ProtocoloSTNData extends Meta
{
    items: ProtocoloSTN[];
};

export interface ProtocoloSTNParameters extends QueryStringParameters
{
    CodProtocoloSTN?: number;
    CodRAT?: number;
    CodOS?: number;
    NumProtocolo?: number;
};
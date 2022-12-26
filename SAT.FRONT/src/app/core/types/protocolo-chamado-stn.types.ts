import { Meta, QueryStringParameters } from "./generic.types";
import { OrdemServicoSTN } from "./ordem-servico-stn.types";
import { TipoChamadoSTN } from "./tipo-chamado-stn.types";

export class ProtocoloChamadoSTN {
    codProtocoloChamadoSTN ?: number;
    codAtendimento ?: number;
    codTipoChamadoSTN ?: number;
    acaoSTN ?: string;
    dataHoraCad ?: string;
    codUsuarioCad ?: string;
    indPrimeiraLigacao ?: number;
    tecnicoCampo ?: string;
    indAtivo ?: number;
    tipoChamadoSTN ?: TipoChamadoSTN;
    ordemServicoSTN ?: OrdemServicoSTN;

}

export interface ProtocoloChamadoSTNData extends Meta {
    items: ProtocoloChamadoSTN[]
};

export interface ProtocoloChamadoSTNParameters extends QueryStringParameters {
    codProtocoloChamadoSTN?: number;
    codAtendimento?: number;
};

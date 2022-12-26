import { Meta, QueryStringParameters } from "./generic.types";
import { Improdutividade } from "./improdutividade.types";
import { ProtocoloChamadoSTN } from "./protocolo-chamado-stn.types";

export class CausaImprodutividade {
    codCausaImprodutividade ?: number;
    codImprodutividade ?: number;
    codProtocolo ?: number;
    indAtivo ?: number;
    improdutividade ?: Improdutividade;
    protocoloChamadoSTN ?: ProtocoloChamadoSTN;

}

export interface CausaImprodutividadeData extends Meta {
    items: CausaImprodutividade[]
};

export interface CausaImprodutividadeParameters extends QueryStringParameters {
    codCausaImprodutividade?: number;
    codProtocolo?: number;
    codImprodutividade ?: number;
    indAtivo ?: number;
};

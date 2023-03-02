import { Meta, QueryStringParameters } from "./generic.types";
import { PecaRE5114 } from "./pecaRE5114.types";
import { Peca } from "./peca.types";
import { OSBancada } from "./os-bancada.types";
import { OsBancadaPecasOrcamento } from "./os-bancada-pecas-orcamento.types";

export class OrcamentoPecasEspec {
    codOrcamentoPecasEspec: number;
    codOsbancada?: number;
    codPeca?: number;
    codOrcamento?: number;
    quantidade?: number;
    codPecaRe5114?: number;
    indCobranca?: number;
    valorPeca?: number;
    tipoDesconto?: number;
    codBancadaLista?: number;
    valorDesconto?: number;
    percIpi?: number;
    pecaRE5114?: PecaRE5114;
    peca?: Peca;
    oSBancada?: OSBancada;
    osBancadaPecasOrcamento?: OsBancadaPecasOrcamento;
}

export interface OrcamentoPecasEspecData extends Meta {
    items: OrcamentoPecasEspec[];
};

export interface OrcamentoPecasEspecParameters extends QueryStringParameters {
    codOsbancadas?: string;
    codPecas?: string;
    codOrcamentos?: string;
    codPecaRe5114s?: string;
    codOrcamento?: number;
};

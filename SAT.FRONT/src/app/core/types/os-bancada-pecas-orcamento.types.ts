import { Meta, QueryStringParameters } from "./generic.types";
import { OSBancada } from "./os-bancada.types";
import { PecaRE5114 } from "./pecaRE5114.types";

export class OsBancadaPecasOrcamento {
    codOrcamento: number;
    codOsbancada: number;
    codPecaRe5114: number;
    indOrcamentoAprov?: number;
    tipoOrcamentoEscolhido?: number;
    valorPreAprovado?: number;
    observacao?: string;
    numeroAlteracao?: number;
    codOrcamentoPai?: number;
    codUsuarioManut?: string;
    numOrdemCompra?: string;
    motivoReprov?: string;
    dataHoraManut?: string;
    codOrcamentoQtdPai?: number;
    osBancada?: OSBancada;
    pecaRE5114?: PecaRE5114;
}

export interface OsBancadaPecasOrcamentoData extends Meta {
    items: OsBancadaPecasOrcamento[];
};

export interface OsBancadaPecasOrcamentoParameters extends QueryStringParameters {
    codOsbancadas?: string;
    codPecaRe5114s?: string;
};

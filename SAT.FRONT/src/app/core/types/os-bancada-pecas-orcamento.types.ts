import { Meta, QueryStringParameters } from "./generic.types";
import { OSBancadaPecas } from "./os-bancada-pecas.types";

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
    osBancadaPecas?: OSBancadaPecas;

}

export interface OsBancadaPecasOrcamentoData extends Meta {
    items: OsBancadaPecasOrcamento[];
};

export interface OsBancadaPecasOrcamentoParameters extends QueryStringParameters {
    codOsbancadas?: string;
    codPecaRe5114s?: string;
};

import { Meta, QueryStringParameters } from "./generic.types";
import { OSBancadaPecas } from "./os-bancada-pecas.types";
import { Usuario } from "./usuario.types";

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
    valorTotal?: number;
    osBancadaPecas?: OSBancadaPecas;
    usuario?: Usuario;

}

export interface OsBancadaPecasOrcamentoData extends Meta {
    items: OsBancadaPecasOrcamento[];
};

export interface OsBancadaPecasOrcamentoParameters extends QueryStringParameters {
    codOsbancadas?: string;
    codPecaRe5114s?: string;
    codOrcamento?: number;
    numRe5114?: string;
};

export enum OsBancadaPecasOrcamentoEnum {
    REPROVADO = 0,
    APROVADO = 1
}
import { Meta, QueryStringParameters } from "./generic.types";
import { ORCheckList } from "./or-checklist.types";
import { Peca, PecaStatus } from "./peca.types";

export class PecasLaboratorio 
{
    codPeca: number;
    codMagnus: string;
    codPecaFamilia?: number;
    codPecaSubstituicao?: number;
    codPecaStatus: number;
    codTraducao?: number;
    nomePeca: string;
    valCusto: number;
    valCustoDolar: number;
    valCustoEuro: number;
    valPeca: number;
    valPecaDolar: number;
    valPecaEuro?: number;
    valIpi?: number;
    qtdMinimaVenda: number;
    codUsuarioCad: string;
    dataHoraCad: string;
    codUsuarioManut?: string;
    dataHoraManut?: string;
    valPecaAssistencia?: number;
    valIpiassistencia?: number;
    descr_Ingles?: string;
    indObrigRastreabilidade: number;
    indValorFixo: number;
    dataHoraAtualizacaoValor: string;
    isValorAtualizado: number;
    ncm: string;
    codChecklist: number;
    quantidade: number;
    pecaStatus?: PecaStatus;
    pecaFamilia?: any;
    peca?: Peca;
    oRCheckList?: ORCheckList;
}

export interface PecasLaboratorioData extends Meta 
{
    items: PecasLaboratorio[];
};

export interface PecasLaboratorioParameters extends QueryStringParameters 
{
    codPeca?: string;
    codMagnus?: string;
    codChecklist?: number;
};


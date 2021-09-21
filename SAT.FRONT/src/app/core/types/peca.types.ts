import { Meta, QueryStringParameters } from "./generic.types";

export class Peca 
{
    codPeca: number;
    codMagnus: string;
    codPecaFamilia?: any;
    pecaFamilia?: any;
    codPecaSubstituicao?: any;
    codPecaStatus: number;
    pecaStatus: PecaStatus;
    nomePeca: string;
    valCusto: number;
    valCustoDolar: number;
    valCustoEuro: number;
    valPeca: number;
    valPecaDolar: number;
    valPecaEuro: number;
    valIPI: number;
    qtdMinimaVenda: number;
    codUsuarioCad: string;
    dataHoraCad: string;
    codUsuarioManut?: string;
    dataHoraManut?: string;
    valPecaAssistencia?: any;
    valIpiassistencia?: any;
    indObrigRastreabilidade: number;
    indValorFixo: number;
    dataHoraAtualizacaoValor: string;
    isValorAtualizado: number;
    ncm: string;
    listaBackup: number;
    dtObsoleto?: any;
    utilizadoDss: number;
    itemLogix?: any;
    hierarquiaPesquisa?: any;
    indiceDeTroca?: any;
    kitTecnico: number;
    qtdpecaKitTecnico: number;
    dataIntegracaoLogix?: any;
    dataAtualizacao: string;
}

export interface PecaData extends Meta 
{
    items: Peca[];
};

export interface PecaParameters extends QueryStringParameters { };

export enum PecaStatus
{
    "Liberada" = 1,
    "Indisponível" = 2,
    "Substituída" = 3,
    "Descontinuada" = 4
};
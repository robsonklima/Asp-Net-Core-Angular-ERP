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
    clientePeca?: ClientePeca[];
    clientePecaGenerica?: ClientePecaGenerica;
}

export interface PecaData extends Meta 
{
    items: Peca[];
};

export interface PecaStatusData extends Meta 
{
    items: PecaStatus[];
};

export interface PecaParameters extends QueryStringParameters 
{
    codPeca?: string;
    include?: PecaIncludeEnum;
    codMagnus?: string;
};

export interface PecaStatusParameters extends QueryStringParameters 
{
    codPecaStatus?: string;
    include?: PecaIncludeEnum;
    
};

export enum PecaStatus
{
    "Liberada" = 1,
    "Indisponível" = 2,
    "Substituída" = 3,
    "Descontinuada" = 4
};

export enum PecaIncludeEnum
{
    OS_PECAS = 1
}

export interface ClientePeca
{
    codClientePeca: number;
    codCliente: number;
    codContrato: number;
    codPeca: number;
    valorUnitario: number;
    valorIPI: number;
    vlrSubstituicaoNovo: number;
    vlrBaseTroca: number;
    codUsuarioCad: string;
    dataHoraCad: string;
    codUsuarioManut: string;
    dataHoraManut: string;
}

export interface ClientePecaGenerica
{
    codClientePecaGenerica: number;
    codPeca: number;
    valorUnitario: number;
    valorIPI: number;
    vlrSubstituicaoNovo: number;
    vlrBaseTroca: number;
    codUsuarioCad: string;
    dataHoraCad: string;
    codUsuarioManut: string;
    dataHoraManut: string;
}
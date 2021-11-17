import { Filial } from "./filial.types";
import { Meta, QueryStringParameters } from "./generic.types";
import { RelatorioAtendimento } from "./relatorio-atendimento.types";

export interface Despesa
{
    codDespesa?: number;
    codDespesaPeriodo: number;
    codRAT: number;
    relatorioAtendimento?: RelatorioAtendimento;
    codTecnico: number;
    despesaItens?: DespesaItem[];
    codFilial: number;
    filial?: Filial;
    centroCusto: string;
    indAtivo: number;
    codUsuarioCad: string;
    dataHoraCad: string;
    codUsuarioManut?: string;
    dataHoraManut?: string;
}

export interface DespesaData extends Meta
{
    items: Despesa[]
};

export interface DespesaParameters extends QueryStringParameters
{
    codDespesaPeriodo?: number;
    codTecnico?: number;
    codRATs?: string;
};

export interface DespesaItem
{
    codDespesaItem?: number;
    codDespesa: number;
    codDespesaTipo: number;
    despesaTipo?: DespesaTipo;
    codDespesaConfiguracao?: number;
    sequenciaDespesaKm?: number;
    numNF?: string;
    despesaValor?: number;
    enderecoOrigem?: string;
    numOrigem?: string;
    bairroOrigem?: string;
    codCidadeOrigem?: number;
    indResidenciaOrigem?: number;
    indHotelOrigem?: number;
    enderecoDestino?: string;
    numDestino?: string;
    bairroDestino?: string;
    codCidadeDestino?: number;
    indResidenciaDestino?: number;
    indHotelDestino?: number;
    kmPrevisto?: number;
    kmPercorrido?: number;
    tentativaKM?: string;
    obs?: string;
    obsReprovacao?: string;
    codDespesaItemAlerta?: number;
    indReprovado?: number;
    indAtivo?: number;
    codUsuarioCad?: string;
    dataHoraCad?: string;
    codUsuarioManut?: string;
    dataHoraManut?: string;
    latitudeHotel?: string;
    longitudeHotel?: string;
}

export interface DespesaItemData extends Meta
{
    items: DespesaItem[]
};

export interface DespesaItemParameters extends QueryStringParameters
{
};

export interface DespesaTipo
{
    codDespesaTipo: number;
    nomeTipo: string;
    nomeTipoAbreviado: string;
    indAtivo: number;
    codUsuarioCad: string;
    dataHoraCad: string;
    codUsuarioManut: string;
    dataHoraManut: string;
}

export enum DespesaTipoEnum
{
    KM = 1,
    TAXI = 2,
    ONIBUS = 3,
    ESTACIONAMENTO = 4,
    TELEFONE = 5,
    REFEICAO = 6,
    PEDAGIO = 7,
    COMBUSTIVEL = 8,
    FERRAMENTAS = 9,
    PECAS = 10,
    HOTEL = 11,
    CORREIO = 12,
    PA = 13,
    ALUGUEL_CARRO = 14,
    CARTAO_TEL = 15,
    INTERNET = 16,
    FRETE = 17,
    OUTROS = 18
}

export interface DespesaTipoData extends Meta
{
    items: DespesaTipo[]
};

export interface DespesaTipoParameters extends QueryStringParameters
{
    indAtivo?: number;
};
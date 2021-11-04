import { Filial } from "./filial.types";
import { Meta, QueryStringParameters } from "./generic.types";
import { RelatorioAtendimento } from "./relatorio-atendimento.types";

export interface Despesa
{
    codDespesa: number;
    codDespesaPeriodo: number;
    codRAT: number;
    relatorioAtendimento: RelatorioAtendimento;
    codTecnico: number;
    despesaItens: DespesaItem[];
    codFilial: number;
    filial: Filial;
    centroCusto: string;
    indAtivo: number;
    codUsuarioCad: string;
    dataHoraCad: string;
    codUsuarioManut: string;
    dataHoraManut: string;
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
    codDespesaItem: number;
    codDespesa: number;
    codDespesaTipo: number;
    despesaTipo: DespesaTipo;
    codDespesaConfiguracao: number;
    sequenciaDespesaKm: number;
    numNF: string;
    despesaValor: number;
    enderecoOrigem: string;
    numOrigem: string;
    bairroOrigem: string;
    codCidadeOrigem: number;
    enderecoOrigemWebraska: string;
    numOrigemWebraska: string;
    bairroOrigemWebraska: string;
    nomeCidadeOrigemWebraska: string;
    siglaUFOrigemWebraska: string;
    siglaPaisOrigemWebraska: string;
    indResidenciaOrigem: number;
    indHotelOrigem: number;
    enderecoDestino: string;
    numDestino: string;
    bairroDestino: string;
    codCidadeDestino: number;
    enderecoDestinoWebraska: string;
    numDestinoWebraska: string;
    bairroDestinoWebraska: string;
    nomeCidadeDestinoWebraska: string;
    siglaUFDestinoWebraska: string;
    siglaPaisDestinoWebraska: string;
    indResidenciaDestino: number;
    indHotelDestino: number;
    kmPrevisto: number;
    kmPercorrido: number;
    tentativaKM: string;
    obs: string;
    obsReprovacao: string;
    codDespesaItemAlerta: number;
    indWebrascaIndisponivel: number;
    indReprovado: number;
    indAtivo: number;
    codUsuarioCad: string;
    dataHoraCad: string;
    codUsuarioManut: string;
    dataHoraManut: string;
    latitudeHotel: string;
    longitudeHotel: string;
}

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

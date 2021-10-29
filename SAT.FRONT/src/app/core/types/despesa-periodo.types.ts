import { Meta, QueryStringParameters } from "./generic.types";

export class DespesaPeriodo
{
    codDespesaPeriodo: number;
    codDespesaConfiguracao: number;
    dataInicio: string;
    dataFim: string;
    indAtivo: number;
    codUsuarioCad: string;
    dataHoraCad: string;
    codUsuarioManut: string;
    dataHoraManut: string;
}

export interface DespesaPeriodoData extends Meta
{
    items: DespesaPeriodo[]
};

export interface DespesaPeriodoParameters extends QueryStringParameters
{
    indAtivo?: number;
};

export class DespesaPeriodoTecnico
{
    codDespesaPeriodoTecnico: number;
    codDespesaPeriodo: number;
    codTecnico: number;
    codDespesaPeriodoTecnicoStatus: number;
    codUsuarioCad: string;
    dataHoraCad: string;
    codUsuarioManut: string;
    dataHoraManut: string;
    codUsuarioCredito: string;
    dataHoraCredito: string;
    codUsuarioCreditoCancelado: string;
    dataHoraCreditoCancelado: string;
    indCredito: number;
    codUsuarioVerificacao: string;
    dataHoraVerificacao: string;
    indVerificacao: number;
    codUsuarioVerificacaoCancelado: string;
    dataHoraVerificacaoCancelado: string;
    indCompensacao: number;
    dataHoraCompensacao: string;
    codUsuarioCompensacao: string;
}

export interface DespesaPeriodoTecnicoData extends Meta
{
    items: DespesaPeriodoTecnico[]
};

export interface DespesaPeriodoTecnicoParameters extends QueryStringParameters
{
    codTecnico?: number;
    codDespesaPeriodo?: number;
    indAtivoPeriodo?: number;
};

export class Despesa
{
    codDespesa: number;
    codDespesaPeriodo: number;
    codRAT: number;
    codTecnico: number;
    despesaItens: DespesaItem[];
    codFilial: number;
    centroCusto: string;
    indAtivo: number;
    codUsuarioCad: string;
    dataHoraCad: string;
    codUsuarioManut: string;
    dataHoraManut: string;
}

export class DespesaItem
{
    codDespesaItem: number;
    codDespesa: number;
    codDespesaTipo: number;
    codDespesaConfiguracao: number;
    sSequenciaDespesaKm: number;
    numNF: string;
    despesaValor: number;
    enderecoOrigem: string;
    numOrigem: string;
    bairroOrigem: string;
    codCidadeOrigem: number;
    enderecoOrigemWebraska: string;
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
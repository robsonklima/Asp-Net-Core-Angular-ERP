import { Cidade } from "./cidade.types";
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
    codTecnico?: string;
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
    cidadeOrigem?: Cidade;
    codCidadeOrigem?: number;
    indResidenciaOrigem?: number;
    indHotelOrigem?: number;
    enderecoDestino?: string;
    numDestino?: string;
    bairroDestino?: string;
    cidadeDestino?: Cidade;
    codCidadeDestino?: number;
    indResidenciaDestino?: number;
    indHotelDestino?: number;
    kmPrevisto?: number;
    kmPercorrido?: number;
    tentativaKM?: string;
    obs?: string;
    obsReprovacao?: string;
    codDespesaItemAlerta?: number;
    despesaItemAlerta?: DespesaItemAlerta;
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
    codigosDespesa?: string;
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

export interface DespesaConfiguracao
{
    codDespesaConfiguracao: number;
    percentualKmCidade: number;
    percentualKmForaCidade: number;
    valorRefeicaoLimiteTecnico: number;
    valorRefeicaoLimiteOutros: number;
    horaExtraInicioAlmoco: string;
    horaExtraInicioJanta: string;
    percentualNotaKM: number;
    valorKM: number;
    valorAluguelCarro: number;
    dataVigencia: string;
    indAtivo: number;
    codUsuarioCad: string;
    dataHoraCad: string;
    codUsuarioManut: string;
    dataHoraManut: string;
};

export interface DespesaConfiguracaoData extends Meta
{
    items: DespesaConfiguracao[]
};

export interface DespesaConfiguracaoParameters extends QueryStringParameters 
{
    indAtivo?: number;
};

export interface DespesaItemAlerta
{
    codDespesaItemAlerta: number;
    descItemAlerta: string;
};

export interface DespesaItemAlertaData extends Meta
{
    items: DespesaItemAlerta[]
};

export interface DespesaItemAlertaParameters extends QueryStringParameters { };

export enum DespesaItemAlertaEnum
{
    Indefinido = 0,
    TecnicoTeveRefeicaoEmHorarioNaoExtraEmDiaSemana = 1,
    TecnicoTeveMaisDeDuasRefeicoesEmFinalSemana = 2,
    TecnicoTeveAlgumaRefeicaoMaiorQueLimiteEspecificado = 3,
    TecnicoTeveUmaQuilometragemPercorridaMaiorQuePrevista = 4,
    SistemaIndisponivelTecnicoTeveQuilometragemNaoValidada = 5,
    SistemaNaoEncontrouCoordenadaOrigem = 6,
    SistemaNaoEncontrouCoordenadaDestino = 7,
    SistemaNaoEncontrouCoordenadaOrigemNemCoordenadaDestino = 8,
    SistemaNaoEncontrouRota = 9,
    SistemaCalculouRotaCentroDasCidades = 10,
    SabadoDomingoDespesaDeAlmocoDeveSerFeitaAposCatorzeHorasJantaAposVinteHoras = 11,
    TecnicoTeveQuilometragemPercorridaMaiorQuePrevistaCalculadaDoCentroAoCentro = 12
}
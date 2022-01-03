import { Cidade } from "./cidade.types";
import { Meta, QueryStringParameters } from "./generic.types";

export interface Orcamento
{
    codOrc: number;
    codigoMotivo: number;
    codigoStatus: number;
    codigoSla: number;
    codigoEquipamento: number;
    codigoCliente: number;
    codigoPosto: number;
    codigoFilial: number;
    codigoContrato: number;
    isMaterialEspecifico: number;
    codigoOrdemServico: number;
    codigoEquipamentoContrato: number;
    descricaoOutroMotivo: string;
    detalhe: string;
    nomeContrato: string;
    numero: string;
    data: string;
    valorIss: number;
    valorTotal: number;
    valorTotalDesconto: number;
    dataCadastro: string;
    usuarioCadastro: string;
    dataEnvioAprovacao: string;
    dataAprovacaoCliente: string;
    enderecoFaturamentoNF?: EnderecoFaturamentoNF;
}

export interface EnderecoFaturamentoNF
{
    codLocalEnvioNFFaturamento: number;
    codCliente: number;
    codContrato: number;
    razaoSocialFaturamento: string;
    enderecoFaturamento: string;
    complementoFaturamento: string;
    numeroFaturamento: string;
    bairroFaturamento: string;
    cnpjFaturamento: string;
    inscricaoEstadualFaturamento: string;
    responsavelFaturamento: string;
    emailFaturamento: string;
    foneFaturamento: string;
    faxFaturamento: string;
    indAtivoFaturamento?: number;
    cepFaturamento: string;
    codUFFaturamento?: number;
    codCidadeFaturamento?: number;
    razaoSocialEnvioNF: string;
    enderecoEnvioNF: string;
    complementoEnvioNF: string;
    numeroEnvioNF: string;
    bairroEnvioNF: string;
    cnpjEnvioNF: string;
    inscricaoEstadualEnvioNF: string;
    responsavelEnvioNF: string;
    emailEnvioNF: string;
    foneEnvioNF: string;
    faxEnvioNF: string;
    indAtivoEnvioNF?: number;
    cepEnvioNF: string;
    codCidadeEnvioNF?: number;
    codUFEnvioNF?: number;
    codUsuarioCad: string;
    dataHoraCad: string;
    codUsuarioManut: string;
    dataHoraManut: string;
    cidadeEnvioNF?: Cidade;
    cidadeFaturamento?: Cidade;
}

export interface OrcamentoData extends Meta
{
    items: Orcamento[];
};

export interface OrcamentoParameters extends QueryStringParameters
{
    codUsuario: string;
};

export enum OrcamentoDadosLocalEnum
{
    FATURAMENTO = 1,
    NOTA_FISCAL = 2,
    ATENDIMENTO = 3
}

export interface OrcamentoDadosLocal
{
    tipo: OrcamentoDadosLocalEnum;
    razaoSocial?: string;
    endereco?: string;
    nomeLocal?: string;
    nroContrato?: string;
    bairro?: string;
    cnpj?: string;
    responsavel?: string;
    cep?: string;
    complemento?: string;
    cidade?: string;
    inscricaoEstadual?: string;
    email?: string;
    fax?: string;
    numero?: string;
    uf?: string;
    fone?: string;
    oscliente?: string;
    modelo?: string;
    osPerto?: string;
    motivoOrcamento?: string;
    agencia?: string;
    nroSerie?: string;
}

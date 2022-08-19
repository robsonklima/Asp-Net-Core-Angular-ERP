import { OrdemServico } from './ordem-servico.types';
import { Cidade } from "./cidade.types";
import { Meta, QueryStringParameters } from "./generic.types";
import { LocalEnvioNFFaturamento } from "./local-envio-nf-faturamento.types";
import { Peca } from "./peca.types";
import { OrcamentoMaoDeObra, OrcamentoMaoDeObraData } from './orcamento-mao-de-obra.types';
import { OrcamentoOutroServico } from './orcamento-outro-servico.types';
import { OrcamentoMaterial } from './orcamento.material.types';

export interface Orcamento
{
    codOrc?: number;
    codigoMotivo?: number;
    codigoStatus?: number;
    codigoSla: number;
    codigoEquipamento: number;
    codigoCliente: number;
    codigoPosto: number;
    codigoFilial: number;
    codigoContrato: number;
    isMaterialEspecifico: number;
    codigoOrdemServico: number;
    codigoEquipamentoContrato: number;
    descricaoOutroMotivo?: string;
    detalhe: string;
    nomeContrato: string;
    numero?: string;
    data?: string;
    valorIss: number;
    valorTotal?: number;
    valorTotalDesconto?: number;
    dataCadastro?: string;
    usuarioCadastro?: string;
    dataEnvioAprovacao?: string;
    dataAprovacaoCliente?: string;
    localEnvioNFFaturamento?: LocalEnvioNFFaturamento;
    orcamentoMotivo?: OrcamentoMotivo;
    materiais?: OrcamentoMaterial;
    maoDeObra?: OrcamentoMaoDeObra;
    outrosServicos?: OrcamentoOutroServico;
    descontos?: OrcamentoDesconto[];
    ordemServico?: OrdemServico;
    orcamentoStatus?: OrcamentoStatus;
    orcamentoDeslocamento?: OrcamentoDeslocamento;
    orcamentoISS?: OrcamentoISS;
    numPedido?: string;
    obsPedido?: string;
    indFaturamento?: number;
    dataHoraFaturamento?: string;
    codUsuarioFaturamento?: string;
    orcamentoMateriais?: OrcamentoMaterial[];
    orcamentoMaoDeObra?: OrcamentoMaoDeObraData[];
    orcamentoOutrosServicos?: OrcamentoOutroServico[];
}

export interface OrcamentoMotivo
{
    codOrcMotivo: number;
    descricao: string;
}

export enum OrcamentoMotivoEnum
{
    INSTALACAO_DESINSTACALAO = 13
}

export interface OrcamentoMotivoData extends Meta
{
    items: OrcamentoMotivo[];
};

export interface OrcamentoMotivoParameters extends QueryStringParameters
{
};

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

export interface OrcamentoDesconto
{
    codOrcDesconto: number;
    codOrc: number;
    indiceCampo: number;
    indiceTipo: number;
    nomeCampo: string;
    nomeTipo: string;
    valor: number;
    valorTotal: number;
    motivo: string;
    dataCadastro: string;
    usuarioCadastro: string;
}

export interface OrcamentoStatus
{
    codOrcStatus: number;
    nome: string;
}

export interface OrcamentoStatusData extends Meta
{
    items: OrcamentoStatus[];
};

export interface OrcamentoStatusParameters extends QueryStringParameters
{
};

export interface OrcamentoDeslocamento
{
    codOrcDeslocamento?: number;
    codOrc: number;
    quantidadeHoraCadaSessentaKm?: number;
    valorUnitarioKmRodado: number;
    quantidadeKm?: number;
    valorTotalKmRodado?: number;
    valorTotalKmDeslocamento?: number;
    valorHoraDeslocamento: number;
    latitudeOrigem: string;
    longitudeOrigem: string;
    latitudeDestino: string;
    longitudeDestino: string;
    dataCadastro: string;
    data: string;
    usuarioCadastro: string;
}

export interface OrcamentoISS
{
    codOrcIss: number;
    codigoFilial: number;
    valor: number;
}

export interface OrcamentoData extends Meta
{
    items: Orcamento[];
};

export interface OrcamentoParameters extends QueryStringParameters
{
    codStatusServicos?: string;
    codFiliais?: string;
    codClientes?: string;
    codTiposIntervencao?: string;
    codEquipContrato?: number;   
    numRAT?: string;
    codigoOrdemServico?: number;
    numOSCliente?: string;
    numOSQuarteirizada?: string;
    numSerie?: string;
    numero?: string;
    isFaturamento?: boolean;
};

export enum OrcamentoDadosLocalEnum
{
    FATURAMENTO = 1,
    NOTA_FISCAL = 2,
    ATENDIMENTO = 3
}

export interface OrcamentoDadosLocal
{
    codLocalEnvioNFFaturamento: number;
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

export interface OrcamentoAprovacao {
    codOrc: number;
    motivo: string;
    nome: string;
    email: string;
    departamento: string;
    telefone: string;
    ramal: string;
    isAprovado: boolean;
}

export enum OrcamentoTipoOutroServicoEnum
{
    ABERTURA_COFRE = "Abertura de cofre",
    ABERTURA_COFRE_COM_REFORMA = "Abertura de cofre com reforma na porta",
    SOLDAGEM_PORTA_COFRE = "Soldagem na porta do cofre",
    OUTROS = "Outros    ",
    ABERTURA_TAMPA_INFERIOR = "Abertura da tampa inferior",
    ABERTURA_TAMPA_SUPERIOR = "Abertura da tampa superior"
}

export enum OrcamentoTipoDescontoEnum
{
    TOTAL_MAO_DE_OBRA = "Valor Total Mão de Obra",
    TOTAL_KM_RODADO_OBRA = "Valor Total KM Rodado",
    TOTAL_HORA_DESLOCAMENTO = "Valor Total Hora em Deslocamento",
    TOTAL_ORCAMENTO = "Valor Total Orçamento"
}

export enum OrcamentoFormaDescontoEnum
{
    PERCENTUAL = "Percentual",
    VALOR = "Valor"
}

export enum OrcamentoTipoIntervencao
{
    ORCAMENTO_APROVADO = 17,
    ORCAMENTO = 5,
    ORCAMENTO_REPROVADO = 18,
}

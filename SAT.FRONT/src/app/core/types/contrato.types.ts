import { ContratoEquipamento } from "./contrato-equipamento.types";
import { Meta, QueryStringParameters } from "./generic.types";
import { InstalacaoLote } from "./instalacao-lote.types";
import { TipoContrato } from "./tipo-contrato.types";

export class Contrato
{
    codContrato: number;
    codContratoPai: number;
    codCliente: number;
    codTipoContrato: number;
    cnpj: string;
    nroContrato: string;
    nomeContrato: string;
    dataContrato: string;
    dataAssinatura: string;
    dataInicioVigencia: string;
    dataFimVigencia: string;
    dataInicioPeriodoReajuste: string;
    dataFimPeriodoReajuste: string;
    nomeResponsavelPerto: string;
    nomeResponsavelCliente: string;
    objetoContrato: string;
    valorTotalContrato: number;
    numMinReincidencia: number;
    kmMinimoAdicional: number;
    kmAdicionalHora: number;
    mtbfnominal: number;
    indAtivo: number;
    indPermitePecaGenerica: number;
    dataCadastro: string;
    codUsuarioCadastro: string;
    dataManutencao: string;
    codUsuarioManutencao: string;
    horaDispInicio: string;
    horaDispFim: string;
    indDispSabado: number;
    indDispDomingo: number;
    indDisp24H: number;
    codSla: number;
    numDiasSubsEquip: number;
    codEmpresa: number;
    dataReauste: string;
    dataCancelamento: string;
    codUsuarioCancelamento: string;
    motivoCancelamento: string;
    numeroEnd: string;
    complemEnd: string;
    enderecoCobranca: string;
    bairroCobranca: string;
    cidadeCobranca: string;
    siglaUFCobranca: string;
    cepCobranca: string;
    telefoneCobranca: string;
    faxCobranca: string;
    indGarantia: number;
    indHerenca: number;
    percReajuste: number;
    indPermitePecaEspecifica: number;
    semCobertura: string;
    codFormaPagamento: number;
    codOrcDadosBancarios: number;
    codPosVenda: number;
    indPedido: number;    
    lotes: InstalacaoLote[];
    contratoEquipamento: ContratoEquipamento;
    contratoServico: ContratoServico[];
    tipoContrato: TipoContrato;
}

export interface ContratoData extends Meta
{
    items: Contrato[];
};

export interface ContratoParameters extends QueryStringParameters
{
    codContrato?: number;
    codTipoContrato?: number;
    indAtivo?: number;
    codCliente?: number;
    codClientes?: string;
    filter?: string;
};

export interface ContratoServico
{
    codContratoServico: number;
    codContrato: number;
    codServico: number;
    codSLA: number;
    codTipoEquip: number;
    codGrupoEquip: number;
    codEquip: number;
    valor: number;
    codUsuarioCad: string;
    dataHoraCad: string;
    codUsuarioManut?: string;
    dataHoraManut?: string;
    codUsuarioCadastroDel?: string;
    dataHoraCadastroDel?: string;
    codUsuarioManutencaoDel?: string;
    dataHoraManutencaoDel?: string;
}
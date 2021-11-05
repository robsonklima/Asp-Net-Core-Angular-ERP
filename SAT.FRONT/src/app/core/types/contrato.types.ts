import { Meta, QueryStringParameters } from "./generic.types";

export class Contrato {
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
    dataReauste:  string;
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
}

export interface ContratoData extends Meta {
    items: Contrato[];
};

export interface ContratoParameters extends QueryStringParameters {
    codContrato?:number;
    indAtivo?: number;
    codCliente?: number;
    filter?: string;
};
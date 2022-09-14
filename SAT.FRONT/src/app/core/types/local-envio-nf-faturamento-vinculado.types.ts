import { Contrato } from "./contrato.types";
import { Meta, QueryStringParameters } from "./generic.types";
import { LocalAtendimento } from "./local-atendimento.types";
import { LocalEnvioNFFaturamento } from "./local-envio-nf-faturamento.types";

export interface LocalEnvioNFFaturamentoVinculado {
    codLocalEnvioNFFaturamento?: number;
    codPosto: number;
    codContrato: number;
    indAdicionado?: number | null;
    codUsuarioCad: string;
    dataHoraCad: string;
    codUsuarioManut?: string;
    dataHoraManut?: string | null;
    contrato?: Contrato;
    localAtendimento?: LocalAtendimento;
    localEnvioNFFaturamento?: LocalEnvioNFFaturamento;
}

export interface LocalEnvioNFFaturamentoVinculadoData extends Meta
{
    items: LocalEnvioNFFaturamentoVinculado[];
};

export interface LocalEnvioNFFaturamentoVinculadoParameters extends QueryStringParameters
{
    codLocalEnvioNFFaturamento?: number;
    codContrato?: number;
};
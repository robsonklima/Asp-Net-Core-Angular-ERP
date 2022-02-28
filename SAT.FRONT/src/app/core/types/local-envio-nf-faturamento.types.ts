import { Cidade } from "./cidade.types";
import { Cliente } from "./cliente.types";
import { Contrato } from "./contrato.types";
import { Meta, QueryStringParameters } from "./generic.types";
import { LocalEnvioNFFaturamentoVinculado } from "./local-envio-nf-faturamento-vinculado.types";

export interface LocalEnvioNFFaturamento {
    codLocalEnvioNFFaturamento: number;
    codCliente: number;
    codContrato: number;
    razaoSocialFaturamento: string;
    enderecoFaturamento: string;
    complementoFaturamento: string;
    numeroFaturamento: string;
    bairroFaturamento: string;
    cnpjfaturamento: string;
    inscricaoEstadualFaturamento: string;
    responsavelFaturamento: string;
    emailFaturamento: string;
    foneFaturamento: string;
    faxFaturamento: string;
    indAtivoFaturamento: number | null;
    cepFaturamento: string;
    codUFFaturamento: number | null;
    codCidadeFaturamento: number | null;
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
    indAtivoEnvioNF: number | null;
    cepEnvioNF: string;
    codCidadeEnvioNF: number | null;
    codUFEnvioNF: number | null;
    codUsuarioCad: string;
    dataHoraCad: string;
    codUsuarioManut: string;
    dataHoraManut: string | null;
    cliente: Cliente;
    cidadeFaturamento: Cidade;
    cidadeEnvioNF: Cidade;
    contrato: Contrato;
    locaisVinculados: LocalEnvioNFFaturamentoVinculado[];
}

export interface LocalEnvioNFFaturamentoData extends Meta
{
    items: LocalEnvioNFFaturamento[];
};

export interface LocalEnvioNFFaturamentoParameters extends QueryStringParameters
{
    
};
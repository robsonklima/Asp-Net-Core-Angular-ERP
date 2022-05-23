import { Meta } from "@angular/platform-browser";
import { QueryStringParameters } from "./generic.types";

export interface IntegracaoCobra
{
    codOS: number;
    numOscliente: string;
    nomeTipoArquivoEnviado: string;
    nomeArquivo: string;
    dataHoraEnvio: string;
}

export interface IntegracaoCobraData extends Meta
{
    items: IntegracaoCobra[]
};

export interface IntegracaoCobraParameters extends QueryStringParameters
{
    codOS: number;
    numOscliente: string;
    nomeTipoArquivoEnviado: string;
};
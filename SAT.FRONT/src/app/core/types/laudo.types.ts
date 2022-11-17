import { Meta } from "@angular/platform-browser";
import { QueryStringParameters } from "./generic.types";
import { OrdemServico } from "./ordem-servico.types";
import { RelatorioAtendimento } from "./relatorio-atendimento.types";
import { Tecnico } from "./tecnico.types";

export interface Laudo
{
    codLaudo: number;
    codOS: number;
    codRAT: number;
    codTecnico: number;
    relatoCliente?: string;
    conclusao?: string;
    dataHoraCad?: string;
    codLaudoStatus: number;
    nomeCliente: string;
    matriculaCliente: string;
    tensaoComCarga: string;
    tensaoSemCarga: string;
    tensaoTerraENeutro: string;
    temperatura: string;
    justificativa: string;
    indRedeEstabilizada: number;
    indPossuiNobreak: number;
    indPossuiArCond: number;
    codUsuarioManut: string;
    dataHoraManut: string;
    indAtivo: number;
    laudosSituacao: LaudoSituacao[];
    laudoStatus: LaudoStatus;
    tecnico?: Tecnico[];
    or?: OrdemServico[];
    rat?: RelatorioAtendimento[];
}

export interface LaudoData extends Meta {
    items: Laudo[]
};

export interface LaudoParameters extends QueryStringParameters {
    codLaudo?: number;
    codOS?: number;
    codRAT?: number;
    codTecnico?: number;
    indAtivo?: number;
};

export interface LaudoStatus
{
    codLaudoStatus: number;
    nomeStatus: string;
    indAtivo?: number;
}

export interface LaudoSituacao
{
    codLaudoSituacao: number;
    codLaudo: number;
    causa: string;
    acao: string;
    dataHoraCad: string;
}
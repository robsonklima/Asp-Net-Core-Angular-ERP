import { Meta } from "@angular/platform-browser";
import { QueryStringParameters } from "./generic.types";
import { LaudoSituacao } from "./laudo-situacao.types";
import { LaudoStatus } from "./laudo-status.types";
import { OrdemServico } from "./ordem-servico.types";
import { Tecnico } from "./tecnico.types";
import { Usuario } from "./usuario.types";

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
    usuario?: Usuario[];
}

export interface LaudoData extends Meta {
    items: Laudo[];
};

export interface LaudoParameters extends QueryStringParameters {
    codLaudo?: number;
    codOS?: number;
    codTecnico?: number;
    indAtivo?: number;
    codRAT?: number;
    codLaudoStatus?: number;
    codLaudosStatus?: string;
};

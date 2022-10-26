import { Meta, QueryStringParameters } from "./generic.types";
import { Usuario } from "./usuario.types";

export class BancadaLaboratorio {
    codBancadaLaboratorio : number;
    codUsuario : String;
    numBancada : number;
    usuario?: Usuario;
    usuarioCadastro? : Usuario;
    codUsuarioCad : string;
    dataHoraCad : string;
}

export interface BancadaLaboratorioData extends Meta {
    items: BancadaLaboratorio[]
};

export interface BancadaLaboratorioParameters extends QueryStringParameters {
    codUsuario?: String;
    numBancada?: number;
    indUsuarioAtivo?: number;
};

export interface ViewLaboratorioTecnicoBancada {
    nome: string;
    codUsuario: string;
    emReparo: string;
    tempoOcioso: string;
    tempoEmReparo: string;
    qtdEmReparo: number;
    numBancada: number;
    tempoReparoPeca: string;
    statusReparo: string;
    codOR?: number;
}
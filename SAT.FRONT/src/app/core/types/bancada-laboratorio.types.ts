import { Meta, QueryStringParameters } from "./generic.types";
import { Usuario } from "./usuario.types";

export class BancadaLaboratorio {
    codUsuario : String;
    numBancada : number;
    usuario?: Usuario;
    UsuarioCadastro? : Usuario;
    CodUsuarioCad : string;
    dataHoraCad : string;
    
}

export interface BancadaLaboratorioData extends Meta {
    items: BancadaLaboratorio[]
};

export interface BancadaLaboratorioParameters extends QueryStringParameters {
    codUsuario?: String;
    numBancada?: number;
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
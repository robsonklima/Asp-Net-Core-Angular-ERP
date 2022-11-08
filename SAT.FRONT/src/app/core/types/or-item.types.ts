import { Cliente } from "./cliente.types";
import { Meta, QueryStringParameters } from "./generic.types";
import { ORDefeito } from "./or-defeito.types";
import { ORSolucao } from "./or-solucao.types";
import { ORStatus } from "./or-status.types";
import { ORTempoReparo } from "./or-tempo-reparo.types";
import { ORTipo } from "./or-tipo.types";
import { OrdemServico } from "./ordem-servico.types";
import { Peca } from "./peca.types";
import { Usuario } from "./usuario.types";

export interface ORItem {
    codORItem: number;
    dataHoraORItem: string;
    codOR: number;
    codPeca: number;
    peca: Peca;
    codStatus: number;
    quantidade: number;
    numSerie: string;
    codTipoOR: number;
    orTipo: ORTipo;
    codOS: number;
    ordemServico: OrdemServico;
    codCliente: number;
    cliente: Cliente;
    codTecnico: string;
    defeitoRelatado: string;
    relatoSolucao: string;
    codDefeito: number;
    codAcao: number;
    codSolucao: number;
    indConfLog: number;
    indConfLab: number;
    indAtivo: number;
    codUsuarioCad: string;
    usuarioCadastro: Usuario;
    usuarioTecnico: Usuario;
    dataHoraCad: string;
    divergenciaDescricao: string;
    dataConfLab: string;
    dataConfLog: string;
    codStatusOR: number;
    statusOR: ORStatus;
    indPrioridade: number;
    diasEmReparo: number;
    selecionado: boolean;
    temposReparo: ORTempoReparo[];
    orDefeito: ORDefeito;
    orSolucao: ORSolucao;
}

export interface ORItemData extends Meta {
    items: ORItem[];
};

export interface ORItemParameters extends QueryStringParameters {
    codOR?: number;
    codTiposOR?: string;
    codStatus?: string;
    codMagnus?: string;
    nomeTecnico?: string;
}
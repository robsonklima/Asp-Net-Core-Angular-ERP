import { Cliente } from "./cliente.types";
import { Meta, QueryStringParameters } from "./generic.types";
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
    codTipoOR: number | null;
    orTipo: ORTipo;
    codOS: number | null;
    ordemServico: OrdemServico;
    codCliente: number | null;
    cliente: Cliente;
    codTecnico: string;
    defeitoRelatado: string;
    relatoSolucao: string;
    codDefeito: number | null;
    codAcao: number | null;
    codSolucao: number | null;
    indConfLog: number | null;
    indConfLab: number | null;
    indAtivo: number;
    codUsuarioCad: string;
    usuarioCadastro: Usuario;
    usuarioTecnico: Usuario;
    dataHoraCad: string;
    divergenciaDescricao: string;
    dataConfLab: string | null;
    dataConfLog: string | null;
    codStatusOR: number | null;
    statusOR: ORStatus;
    indPrioridade: number | null;
    diasEmReparo: number;
    selecionado: boolean;
    temposReparo: ORTempoReparo[];
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
import { Filial } from "./filial.types";
import { ClienteBancada } from "./cliente-bancada.types";
import { Meta, QueryStringParameters } from "./generic.types";
import { OSBancada } from "./os-bancada.types";
import { PecaRE5114 } from "./pecaRE5114.types";

export class OSBancadaPecas {
     filial?: Filial;
    clienteBancada?: ClienteBancada;

    codOsbancada: number;
    codPecaRe5114: number;
    codFilialRe5114?: number;
    indGarantia?: number;
    defeitoRelatado?: string;
    defeitoConstatado?: string;
    solucao?: string;
    codUsuarioCadastro: string;
    dataCadastro: string;
    codUsuarioManut?: string;
    dataManut?: string;
    motivoQgarantia?: string;
    indPecaLiberada?: number;
    dataHoraPecaLiberada?: string;
    valorEntrada?: number;
    indPecaDevolvida?: number;
    codPecaRe5114troca?: number;
    numItemNf?: number;
    nomeTecnicoRelatante?: string;
    oSBancada?: OSBancada;
    pecaRE5114?: PecaRE5114;
}

export interface OSBancadaPecasData extends Meta {
    items: OSBancadaPecas[];
};

export interface OSBancadaPecasParameters extends QueryStringParameters {
    codOsbancadas?: string;
    codPecaRe5114s?: string;
};

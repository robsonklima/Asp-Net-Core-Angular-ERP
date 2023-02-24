import { Meta, QueryStringParameters } from "./generic.types";
import { OsBancadaPecasOrcamento } from "./os-bancada-pecas-orcamento.types";
import { OSBancada } from "./os-bancada.types";
import { Peca } from "./peca.types";

export class PecaRE5114 
{
    codPecaRe5114: number;
    numRe5114: string;
    codPeca?: number;
    numSerie?: string;
    numPecaCliente?: string;
    codUsuarioCadastro: string;
    dataCadastro: string;
    codUsuarioManut?: string;
    dataManut?: string;
    indSucata?: number;
    indDevolver?: number;
    indMarcacaoEspecial?: number;
    motivoDevolucao?: string;
    motivoSucata?: string;
    codOsbancada?: number;
    peca?: Peca;
    osBancada?: OSBancada;
}

export interface PecaRE5114Data extends Meta 
{
    items: PecaRE5114[];
};

export interface PecaRE5114Parameters extends QueryStringParameters 
{
    codPecas?: string;
    numRe5114?: string;
    codOsbancada?: number;
};


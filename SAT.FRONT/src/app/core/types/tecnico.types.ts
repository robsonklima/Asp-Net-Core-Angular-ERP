import { Autorizada } from "./autorizada.types";
import { Cidade } from "./cidade.types";
import { Cliente } from "./cliente.types";
import { DespesaCartaoCombustivel, DespesaCartaoCombustivelTecnico } from "./despesa-cartao-combustivel.types";
import { Filial } from "./filial.types";
import { Meta, QueryStringParameters } from "./generic.types";
import { OrdemServico } from "./ordem-servico.types";
import { Regiao } from "./regiao.types";
import { TipoRota } from "./tipo-rota.types";
import { Usuario } from "./usuario.types";

export class Tecnico {
    codTecnico: number;
    codAutorizada: number;
    codFilial: number;
    codTipoRota: number;
    nome: string;
    apelido: string;
    dataNascimento?: any;
    dataAdmissao: Date;
    cpf: string;
    rg: string;
    cep: string;
    endereco: string;
    enderecoComplemento: string;
    latitude: string;
    longitude: string;
    enderecoCoordenadas: string;
    bairroCoordenadas: string;
    cidadeCoordenadas: string;
    ufcoordenadas: string;
    paisCoordenadas: string;
    bairro: string;
    codCidade: number;
    cidade: Cidade;
    fone: string;
    email: string;
    numCrea?: any;
    indTecnicoBancada: number;
    indAtivo: number;
    codUsuarioCad: string;
    dataHoraCad?: any;
    codUsuarioManut: string;
    dataHoraManut: Date;
    foneParticular: string;
    fonePerto?: any;
    simCardMobile: string;
    indPA?: number;
    trackerId?: any;
    codSimCard?: any;
    cpflogix: string;
    indFerias: number;
    codRegiao: number;
    regiao?: Regiao;
    codDespesaCartaoCombustivel?: any;
    codFrotaCobrancaGaragem: number;
    codFrotaFinalidadeUso: number;
    cnh?: any;
    cnhcategorias?: any;
    finalidadesUso?: any;
    dtFeriasInicio?: any;
    dtFeriasFim?: any;
    cnhvalidade?: any;
    autorizada: Autorizada;
    filial: Filial;
    tipoRota: TipoRota;
    ordensServico: OrdemServico[];
    qtdChamadosCorretivos: number;
    qtdChamadosPreventivos: number;
    qtdChamadosOutros: number;
    qtdChamados: number;
    distanciaResidenciaLocalAtendimento: number;
    tempoResidenciaLocalAtendimento: string;
    usuario: Usuario;
    mediaTempoAtendMin: number;
    despesaCartaoCombustivelTecnico?: DespesaCartaoCombustivelTecnico[];
    tecnicoConta?: TecnicoConta[];
    tecnicoCategoriaCredito?: TecnicoCategoriaCredito;
    tecnicoCliente?: TecnicoCliente[];
}

export interface TecnicoData extends Meta {
    items: Tecnico[];
};

export interface TecnicoParameters extends QueryStringParameters {
    codTecnico?: number;
    codTecnicos?: string;
    nome?: string;
    indAtivo?: number;
    codFiliais?: string;
    indFerias?: number;
    codAutorizada?: number;
    codPerfil?: number;
    codigosStatusServico?: string;
    codAutorizadas?: string;
    periodoMediaAtendInicio?: string;
    periodoMediaAtendFim?: string;
    pas?: string;
    codRegioes?: string;
    include?: TecnicoIncludeEnum;
    filterType?: TecnicoFilterEnum;
    naoVinculados?: number;
};

export enum TecnicoIncludeEnum {
    TECNICO_ORDENS_SERVICO = 1
}

export enum TecnicoFilterEnum {
    FILTER_TECNICO_OS = 1
}


export enum FrotaFinalidadeUsoEnum {
    "Apenas Trabalho" = 1,
    "Apenas Trabalho/Particular" = 2
}

export enum FrotaCobrancaGaragemEnum {
    "Pela Empresa" = 1,
    "Pelo Técnico" = 2,
    "Pelo Técnico Sem Cobrança" = 3
}

export class DashboardTecnicoDisponibilidadeTecnicoViewModel {
    codFilial: number;
    nomeFilial: string;
    indFerias: number;
    mediaAtendimentosPorDiaPreventivos: number;
    mediaAtendimentosPorDiaCorretivos: number;
    mediaAtendimentosPorDiaTodasIntervencoes: number;
    mediaAtendimentosPorDiaInstalacoes: number;
    mediaAtendimentosPorDiaEngenharia: number;
    tecnicoSemChamadosTransferidos: boolean;
}

export interface TecnicoConta {
    codTecnicoConta: number;
    cdTecnico: number;
    numBanco: string;
    numAgencia: string;
    numConta: string;
    indPadrao: number;
    indAtivo: number;
    codUsuarioCad: string;
    dataHoraCad: string;
    codUsuarioManut: string;
    dataHoraManut: string;
}

export enum TecnicoCategoriaCreditoEnum {
    "A",
    "B",
    "C",
    "D"
}

export interface TecnicoCategoriaCredito {
    categoriaCredito: TecnicoCategoriaCreditoEnum;
    media: number;
    valor: number;
}

export interface TecnicoCliente {
    codTecnicoCliente: number;
    codTecnico: number;
    codCliente: number;
    cliente: Cliente;
}
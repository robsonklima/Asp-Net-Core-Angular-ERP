import { Autorizada } from "./autorizada.types";
import { Cliente } from "./cliente.types";
import { Contrato } from "./contrato.types";
import { EquipamentoContrato } from "./equipamento-contrato.types";
import { Equipamento } from "./equipamento.types";
import { Filial } from "./filial.types";
import { Meta, QueryStringParameters } from "./generic.types";
import { GrupoEquipamento } from "./grupo-equipamento.types";
import { InstalacaoLote } from "./instalacao-lote.types";
import { InstalacaoRessalva } from "./instalacao-ressalva.types";
import { InstalacaoStatus } from "./instalacao-status.types";
import { LocalAtendimento } from "./local-atendimento.types";
import { OrdemServico } from "./ordem-servico.types";
import { Regiao } from "./regiao.types";
import { TipoEquipamento } from "./tipo-equipamento.types";
import { Transportadora } from "./transportadora.types";

export interface Instalacao {
    codInstalacao: number;
    codInstalLote: number;
    codContrato: number;
    codTipoEquip: number;
    codGrupoEquip: number;
    codEquip: number;
    codRegiao: number;
    codAutorizada: number;
    codFilial: number;
    codSla: number;
    codEquipContrato?: number;
    codCliente: number;
    codPosto: number;
    dataSugEntrega: string;
    dataConfEntrega?: string;
    dataRecDm?: string;
    nfRemessa: string;
    dataNFRemessa: string;
    dataExpedicao: string;
    codTransportadora: number;
    codClienteEnt?: number;
    codPostoEnt?: number;
    dataHoraChegTranspBt?: string;
    indEquipPosicOkbt?: number;
    nomeRespBancoBT?: string;   
    numMatriculaBT?: string;
    indBTOrigEnt?: number;
    indBTOK?: number;
    dataSugInstalacao?: string;
    dataConfInstalacao?: string;
    codOS?: number;
    codRAT?: number;
    codClienteIns?: number;
    codPostoIns?: number;
    dataBI?: string;
    qtdParaboldBI?: number;
    superE?: string;
    csl?: string;
    csoServ?: string;
    supridora?: string;
    msT606TipoNovo?: string;
    indEquipRebaixadoBI?: number;
    nomeRespBancoBI?: string;
    numMatriculaBI?: string;
    indBiorigEnt?: number;
    indBIOK?: number;
    indRATOK?: number;
    indLaudoOK?: number;
    indRE5330OK?: number;
    codInstalNFVenda: number;
    nfVenda_DEL: string;
    dataNFVenda_DEL?: string;
    dataNFVendaEnvio_DEL?: string;
    codInstalNFAut?: number;
    vlrPagtoNFAut?: string;
    numFaturaTransp?: string;
    codInstalStatus: number;
    codUsuarioBlock?: number;
    termoDescaracterizacao?: string;
    fornecedorTradeIn1?: string;
    fornecedorTradeIn2?: string;
    bemTradeIn?: string;
    fabricante?: string;
    modelo?: string;
    dataRetirada?: string;
    nfTradeIn1?: string;
    nfTradeIn2?: string;
    dataNFTradeIn1?: string;
    dataNFTradeIn2?: string;
    nomeTransportadora?: string;
    nomeReponsavelBancoAcompanhamento?: string;
    numMatriculaRespBancoAcompanhamento?: string;
    vlrKMTradeIn1?: string;
    vlrKMTradeIn2?: string;
    vlrDesFixacao1?: string;
    vlrDesFixacao2?: string;
    distanciaKmTradeIn1?: string;
    distanciaKmTradeIn2?: string;
    nfTransportadoraTradeIn?: string;
    dataNFTransportadoraTradeIn?: string;
    vlrRecolhimentoTradeIn?: string;
    codUsuarioCad: string;
    dataHoraCad: string;
    codUsuarioManut: string;
    dataHoraManut: string;
    fornecedorCompraTradeIn?: string;
    nfVendaTradeIn?: string;
    dataNFVendaTradeIn?: string;
    valorUnitarioVenda?: string;
    romaneio?: string;
    dtPrevRecolhimentoTradeIn?: string;
    nfRemessaConferida?: string;
    dtbCliente?: string;
    faturaTranspReEntrega?: string;
    dtReEntrega?: string;
    responsavelRecebReEntrega?: string;
    indInstalacao: number;
    pedidoCompra?: string;
    dtVencBord100?: string;
    dtEntBord100?: string;
    dtVencBord90?: string;
    dtEntBord90?: string;
    dtVencBord10?: string;
    dtEntBord10?: string;
    valorFrete1?: string;
    faturaFrete1?: string;
    cteFrete1?: string;
    dtFaturaFrete1?: string;
    valorFrete2?: string;
    faturaFrete2?: string;
    cteFrete2?: string;
    dtFaturaFrete2?: string;
    valorExtraFrete?: string;
    ddd?: string;
    telefone1?: string;
    redestinacao?: string;
    antigoPrefixoRedestinacao?: string;
    antigoSbRedestinacao?: string;
    antigoNomeDependenciaRedestinacao?: string;
    antigoUfRedestinacao?: string;
    antigoTipoDependenciaRedestinacao?: string;
    antigoPedidoCompraRedestinacao?: string;
    antigoProtocoloCdo?: string;
    novoProtocoloCdo?: string;
    instalacaoLote?: InstalacaoLote;
    contrato?: Contrato;
    tipoEquipamento?: TipoEquipamento;
    grupoEquipamento?: GrupoEquipamento;
    equipamento?: Equipamento;
    regiao?: Regiao;
    autorizada?: Autorizada;
    filial?: Filial;
    equipamentoContrato?: EquipamentoContrato;
    cliente?: Cliente;
    localAtendimento?: LocalAtendimento;  
    localAtendimentoIns?: LocalAtendimento;
    localAtendimentoSol?: LocalAtendimento;
    localAtendimentoEnt?: LocalAtendimento;
    transportadoras?: Transportadora;
    instalacoesRessalva?: InstalacaoRessalva[];
    ordemServico?: OrdemServico;
    instalacaoStatus?: InstalacaoStatus;
    selecionado: boolean;
}

export interface InstalacaoData extends Meta {
    items: Instalacao[];
};

export interface InstalacaoParameters extends QueryStringParameters {
    codContrato?: number;
    codInstalLote?: number;
    codTipoEquip?: number;
    codGrupoEquip?: number;
    codEquip?: number;
    codEquips?: string;
    codEquipContrato?: number;
};

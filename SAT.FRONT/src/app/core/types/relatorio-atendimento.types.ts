import { Foto } from "./foto.types";
import { Meta, QueryStringParameters } from "./generic.types";
import { Laudo } from "./laudo.types";
import { RelatorioAtendimentoDetalhe } from "./relatorio-atendimento-detalhe.type";
import { StatusServico } from "./status-servico.types";
import { Tecnico } from "./tecnico.types";

export interface RelatorioAtendimento
{
    codRAT?: number;
    numRAT?: string;
    codTecnico?: number;
    nomeRespCliente: string;
    nomeAcompanhante: string;
    dataHoraChegada: string;
    dataHoraInicio: string;
    dataHoraFim?: any;
    dataHoraSolucao: string;
    relatoSolucao: string;
    obsRAT: string;
    codUsuarioCad: string;
    dataHoraCad: string;
    codUsuarioManut: string;
    dataHoraManut?: string;
    codServico?: number;
    codTipoEquip?: number;
    codGrupoEquip?: number;
    codEquip?: number;
    dataHoraAbertura?: any;
    dataHoraSolicitacao?: any;
    dataHoraReparo?: string;
    qtdeHorasInicio?: any;
    qtdeHorasReparo?: any;
    qtdeHorasSolucao?: any;
    qtdeHorasEspera?: any;
    motivoEspera?: any;
    qtdeHorasInterrupcao?: any;
    motivoInterrupcao?: any;
    qtdeHorasTecnicas: number;
    valServicos?: any;
    dataCadastro: string;
    codUsuarioCadastro?: any;
    dataManutencao?: string;
    codUsuarioManutencao?: any;
    tempoSlaInicio?: any;
    tempoSlaReparo?: any;
    tempoSlaSolucao?: any;
    tempoEfetInicio?: any;
    tempoEfetReparo?: any;
    tempoEfetSolucao?: any;
    indBRBAtendeConfederal?: any;
    indRatDigitalizada?: any;
    caminhoRATDigitalizada?: any;
    indQuarentena?: any;
    dataHoraInicioValida?: any;
    dataHoraSolucaoValida?: any;
    dataHoraFechamentoValida?: any;
    ventNotaBrb?: any;
    repCasBRB?: any;
    cpuMecanismoDispensadorCedula?: any;
    cpuMecanismoDepositarioEnvelope?: any;
    cpuDispensadorEnvelope?: any;
    cpuImpressoraRecibo?: any;
    cpuPresenterFolhaCheque?: any;
    cpuAntiskimming?: any;
    placaSensor?: any;
    aceitadorCedula?: any;
    biosCpuMicroComputador?: any;
    pertoScan?: any;
    tensaoSemCarga?: any;
    tensaoComCarga?: any;
    temperaturaAmbiente?: any;
    indRedeEstabilizada?: any;
    indCedulaBoaQualidade?: any;
    indCedulaVentilada?: any;
    indInfraEstruturaLogicaAdequada?: any;
    tensaoTerraNeutro?: any;
    horarioInicioIntervalo: string;
    horarioTerminoIntervalo: string;
    qtdPagamentos?: any;
    qtdCedulasPagas?: any;
    nroSerieMecanismo?: any;
    codOS: number;
    relatorioAtendimentoDetalhes: RelatorioAtendimentoDetalhe[];
    protocolosSTN: any[];
    statusServico: StatusServico;
    codStatusServico?: number;
    checkinsCheckouts?: CheckinCheckout[];
    tecnico: Tecnico;
    laudos: Laudo[];
    fotos: Foto[];
}

export interface RelatorioAtendimentoData extends Meta
{
    items: RelatorioAtendimento[];
};

export interface RelatorioAtendimentoParameters extends QueryStringParameters
{
    codTecnicos?: string;
    codOS?: number;
    dataInicio?: string;
    dataSolucao?: string;
    numRAT?: string;
};

export interface CheckinCheckout
{
    codCheckInCheckOut: number;
    tipo: string;
    modalidade: string;
    codOS: number;
    codRAT: number;
    latitude: string;
    longitude: string;
    codUsuarioTecnico: string;
    codUsuarioPa: string;
    dataHoraCadSmartphone: string;
    dataHoraCad: string;
    distanciaLocalMetros: string;
    distanciaLocalDescricao: string;
    duracaoLocalSegundos: string;
    duracaoLocalDescricao: string;
};
import { AgendaTecnico } from "./agenda-tecnico.types";
import { Agendamento } from "./agendamento.types";
import { Autorizada } from "./autorizada.types";
import { Chamado } from "./chamado.types";
import { Cliente } from "./cliente.types";
import { EquipamentoContrato } from "./equipamento-contrato.types";
import { Equipamento } from "./equipamento.types";
import { Filial } from "./filial.types";
import { Foto } from "./foto.types";
import { Meta, QueryStringParameters } from "./generic.types";
import { LocalAtendimento } from "./local-atendimento.types";
import { Orcamento } from "./orcamento.types";
import { OSPrazoAtendimento } from "./os-prazo-atendimento.types";
import { RegiaoAutorizada } from "./regiao-autorizada.types";
import { Regiao } from "./regiao.types";
import { RelatorioAtendimento } from "./relatorio-atendimento.types";
import { StatusServico } from "./status-servico.types";
import { Tecnico } from "./tecnico.types";
import { TipoIntervencao } from "./tipo-intervencao.types";
import { Usuario } from "./usuario.types";

export class OrdemServico
{
    codOS: number;
    chamado: Chamado;
    alertas: OrdemServicoAlerta[];
    statusServico?: StatusServico;
    tipoIntervencao?: TipoIntervencao;
    localAtendimento?: LocalAtendimento;
    equipamentoContrato?: EquipamentoContrato;
    equipamento?: Equipamento;
    filial?: Filial;
    regiao?: Regiao;
    autorizada?: Autorizada;
    regiaoAutorizada?: RegiaoAutorizada;
    relatoriosAtendimento: RelatorioAtendimento[];
    fotos: Foto[];
    cliente: Cliente;
    tecnico: Tecnico;
    agendamentos: Agendamento[];
    defeitoRelatado: string;
    observacaoCliente?: string;
    dataHoraCad: string;
    dataHoraTransf: string;
    dataHoraOSMobileLida: string;
    dataHoraFechamento: string;
    codUsuarioCad?: any;
    nomeSolicitante?: any;
    telefoneSolicitante?: any;
    codCliente: number;
    codPosto: number;
    codEquipContrato: number;
    codEquip: number;
    codTipoIntervencao: number;
    codFilial: number;
    codAutorizada: number;
    codRegiao: number;
    codStatusServico: number;
    codTecnico: number;
    descNumSerieNi?: any;
    dataHoraSolicitacao: string;
    numOSCliente?: any;
    numOSQuarteirizada?: string;
    nomeContato?: any;
    descMotivoMarcaEspecial?: any;
    indMarcaEspecial: number;
    codUsuarioMarcaEspecial?: any;
    dataMarcaEspecial?: any;
    codUsuarioFechamento: string;
    motivoCancelamento?: any;
    codUsuarioCancelamento?: any;
    dataHoraCancelamento?: any;
    indRevisaoReincidencia: number;
    indCienciaAtendente: number;
    indCienciaTerceirizada: number;
    indServico?: any;
    servicoEmail?: any;
    numAgenciaNI?: any;
    numRemessa?: any;
    codUsuarioManut: string;
    dataHoraManut: string;
    codTipoMarcacao?: any;
    tempoSlaInicio: string;
    tempoSlaReparo: string;
    tempoSlaSolucao: string;
    endereco?: any;
    bairro?: any;
    cidade?: any;
    siglaUF?: any;
    pais?: any;
    cep?: any;
    numPatrimonio?: any;
    dataHoraAberturaOS: string;
    dataHoraSolicAtendimento?: any;
    indCancelado: number;
    indReincidencia?: any;
    dataCadastro: string;
    codUsuarioCadastro: string;
    usuarioCadastro: Usuario;
    usuarioCad: Usuario;
    usuarioManutencao: Usuario;
    dataManutencao: string;
    codUsuarioManutencao: string;
    indAtrasoSLA: number;
    tempoEfetInicio?: any;
    tempoEfetReparo?: any;
    tempoEfetSolucao?: any;
    indAcertoParque: number;
    indEndossado: number;
    codUsuarioAcertoParque?: any;
    dataAcertoParque?: any;
    indRevOK?: any;
    codTipoEquip: number;
    codGrupoEquip: number;
    indOrcamentoEnd?: any;
    indEnderecoRevisado: number;
    dataHoraEnderecoVerificado?: any;
    dataHoraIntegracaoRevisao?: any;
    dataHoraIntegracaoRevisaoAgendamento?: any;
    indAgendamentoReenviado?: any;
    dataHoraOsmobileRecebida?: any;
    dataHoraOsmobileLida: string;
    numAgenciaBanco?: any;
    numContaEstabelecimentoCliente?: any;
    cnpjestabelecimentoCliente?: any;
    razaoSocialEstabelecimentoCliente?: any;
    redeEquipamento?: any;
    numTerminal?: any;
    indServicoVerificado?: any;
    indIntegracao: number;
    dataHoraEnvioAgendamentoSemat?: any;
    indAgendamentoUnico?: any;
    indNotificacaoOrcamentoEnviado?: any;
    codMotivoCancelamentoBanrisul?: any;
    nomeArquivoIntegracaoBanrisul?: any;
    indReaberturaIntegracaoBanrisul?: any;
    indOSIntervencaoEquipamento?: any;
    indLiberacaoFechaduraCofre: number;
    indExclusaoBanrisul?: any;
    dataExclusaoBanrisul?: any;
    indBloqueioReincidencia: number;
    numReincidencia: number;
    indVandalismo?: any;
    indStatusEnvioReincidencia: number;
    indFechamentoBanrisul?: any;
    dataFechamentoBanrisul?: any;
    obsFechamentoBanrisul?: any;
    numEstabelecimentoCliente?: any;
    codSeveridade?: number;
    codContrato?: number;
    sugestaoOS?: any;
    codMotivoCancelamento?: number;
    codOperadoraTelefonia?: number;
    codDefeitoPOS?: number;
    codUsuarioOSMobileLida: string;
    codUsuarioOsmobileRecebida?: string;
    dataHoraIntegracaoRevisaoV2?: string;
    dataHoraIntegracaoRevisaoAgendamentoV2?: any;
    prazosAtendimento: OSPrazoAtendimento[];
    indNumRATObrigatorio?: boolean;
    agendaTecnico?: AgendaTecnico[];
    orcamentos?: Orcamento[];
}

export interface OrdemServicoData extends Meta
{
    items: OrdemServico[];
};

export interface OrdemServicoAlerta
{
    titulo: string,
    descricao: any[],
    tipo?: any
};

export interface OrdemServicoParameters extends QueryStringParameters
{
    codOS?: string;
    codEquipContrato?: number;
    codTecnico?: number;
    numOSCliente?: string;
    numOSQuarteirizada?: string;
    dataAberturaInicio?: string;
    dataAberturaFim?: string;
    dataFechamentoInicio?: string;
    dataFechamentoFim?: string;
    codStatusServicos?: string,
    codTiposIntervencao?: string;
    codClientes?: string;
    codCliente?: number;
    codFiliais?: string;
    pa?: number;
    dataTransfInicio?: string;
    dataTransfFim?: string;
    include?: OrdemServicoIncludeEnum;
    filterType?: OrdemServicoFilterEnum;
    dataHoraInicioInicio?: string;
    dataHoraInicioFim?: string;
    dataCancelamentoInicio?: string;
    dataCancelamentoFim?: string;
    inicioPeriodoAgenda?: string;
    fimPeriodoAgenda?: string;
    notIn_CodStatusServicos?: string;
    dataHoraSolucaoInicio?: string;
    dataHoraSolucaoFim?: string;
    numSerie?: string;
    codTipoIntervencaoNotIn?: string;
};

export enum OrdemServicoIncludeEnum
{
    OS_RAT = 1,
    OS_AGENDA = 2,
    OS_PECAS = 3,
    OS_CHAMADO_PECA = 4,
    OS_RAT_FILIAL_PRAZOS_ATENDIMENTO = 5,
    OS_EQUIPAMENTOS = 6,
    OS_TECNICOS = 7,
    OS_RAT_CLIENTE_PRAZOS_ATENDIMENTO = 8,
    OS_TECNICO_ATENDIMENTO = 9,
    OS_EQUIPAMENTOS_ATENDIMENTOS = 10,
    OS_DISPONIBILIDADE_BB = 11,
    OS_LISTA = 12,
    OS_ORCAMENTO = 13,
    OS_EXPORTAR = 14
}

export enum OrdemServicoFilterEnum
{
    FILTER_AGENDA = 1,
    FILTER_INDICADOR = 2,
    FILTER_CORRETIVAS_ANTIGAS = 3,
    FILTER_ORCAMENTOS_ANTIGOS = 4,
    FILTER_PECAS_FALTANTES = 5,
    FILTER_DISPONIBILIDADE_BB_CALCULA_OS = 6,
    FILTER_GENERIC_TEXT = 7
}

export enum StatusServicoEnum
{
    ABERTO = 1,
    CANCELADO = 2,
    FECHADO = 3,
    INSTALACAO = 4,
    PARCIAL = 5,
    PECAS_LIBERADAS = 6,
    PECAS_PENDENTES = 7,
    TRANSFERIDO = 8,
    PECA_EM_TRANSITO = 9,
    PECA_FALTANTE = 10,
    PECA_SEPARADA = 11,
    AGUARDANDO_CONTATO_COM_CLIENTE = 14,
    AGUARDANDO_DECLARACAO = 15,
    CANCELADO_COM_ATENDIMENTO = 16
}

export enum AlertasEnum
{
    WARNING = 'WARNING',
    INFO = 'INFO',
    SUCCESS = 'SUCCESS',
}
import { QueryStringParameters } from "./generic.types";

export interface ViewDadosDashboardParameters extends QueryStringParameters {
	dashboardViewEnum: DashboardViewEnum;
	codPeca?: number;
	codFilial?: number;
	codAutorizada?: number;
	codRegiao?: number;
	codClientes?: string;
}

export enum DashboardViewEnum {
	INDICADORES_FILIAL,
	CHAMADOS_ANTIGOS_CORRETIVAS,
	CHAMADOS_ANTIGOS_ORCAMENTOS,
	BBTS_FILIAIS,
	BBTS_MAPA_REGIOES,
	BBTS_MULTA_REGIOES,
	BBTS_MULTA_DISPONIBILIDADE,
	DISPONIBILIDADE_TECNICOS,
	DISPONIBILIDADE_TECNICOS_MEDIA_GLOBAL,
	SPA,
	SPA_TECNICOS_MENOR_DESEMPENHO,
	SPA_TECNICOS_MAIOR_DESEMPENHO,
	SLA_CLIENTES,
	REINCIDENCIA_FILIAIS,	
	REINCIDENCIA_TECNICOS_MAIS_REINCIDENTES,
	REINCIDENCIA_TECNICOS_MENOS_REINCIDENTES,
	REINCIDENCIA_CLIENTES,
	REINCIDENCIA_EQUIPAMENTOS_MAIS_REINCIDENTES,
	PENDENCIA_FILIAIS,
	PENDENCIA_TECNICOS_MAIS_PENDENCIA,
	PENDENCIA_TECNICOS_MENOS_PENDENCIA,
	PENDENCIA_GLOBAL,
	PECAS_FALTANTES,
	PECAS_MAIS_FALTANTES,
	PECAS_CRITICAS_MAIS_FALTANTES,
	PECAS_CRITICAS_CHAMADOS_FALTANTES,
	PECAS_CRITICAS_ESTOQUE_FALTANTES,
	DENSIDADE_EQUIPAMENTOS,
	DENSIDADE_TECNICOS,
	INDICADORES_DETALHADOS_SLA_CLIENTE,
	INDICADORES_DETALHADOS_SLA_TECNICO,
	INDICADORES_DETALHADOS_SLA_REGIAO,
	INDICADORES_DETALHADOS_PENDENCIA_CLIENTE,
	INDICADORES_DETALHADOS_PENDENCIA_TECNICO,
	INDICADORES_DETALHADOS_PENDENCIA_REGIAO,
	INDICADORES_DETALHADOS_REINCIDENCIA_CLIENTE,
	INDICADORES_DETALHADOS_REINCIDENCIA_TECNICO,
	INDICADORES_DETALHADOS_REINCIDENCIA_REGIAO,
	INDICADORES_DETALHADOS_PERFORMANCE,
	INDICADORES_DETALHADOS_CHAMADOS_ANTIGOS,
	REINCIDENCIA_QUADRIMESTRE_FILIAIS,
	PENDENCIA_QUADRIMESTRE_FILIAIS
}

export class ViewDadosDashboard {
	viewDashboardChamadosMaisAntigosCorretivas: ViewDashboardChamadosMaisAntigosCorretivas[] = [];
	viewDashboardChamadosMaisAntigosOrcamentos: ViewDashboardChamadosMaisAntigosOrcamentos[] = [];
	viewDashboardDisponibilidadeBBTSFiliais: ViewDashboardDisponibilidadeBBTSFiliais[] = [];
	viewDashboardDisponibilidadeBBTSMapaRegioes: ViewDashboardDisponibilidadeBBTSMapaRegioes[] = [];
	viewDashboardDisponibilidadeBBTSMultasDisponibilidade: ViewDashboardDisponibilidadeBBTSMultasDisponibilidade[] = [];
	viewDashboardDisponibilidadeBBTSMultasRegioes: ViewDashboardDisponibilidadeBBTSMultasRegioes[] = [];
	viewDashboardDisponibilidadeTecnicos: ViewDashboardDisponibilidadeTecnicos[] = [];
	viewDashboardDisponibilidadeTecnicosMediaGlobal: ViewDashboardDisponibilidadeTecnicosMediaGlobal[] = [];
	viewDashboardEquipamentosMaisReincidentes: ViewDashboardEquipamentosMaisReincidentes[] = [];
	viewDashboardIndicadoresFiliais: ViewDashboardIndicadoresFiliais[] = [];
	viewDashboardPecasCriticaChamadosFaltantes: ViewDashboardPecasCriticaChamadosFaltantes[] = [];
	viewDashboardPecasCriticaEstoqueFaltantes: ViewDashboardPecasCriticaEstoqueFaltantes[] = [];
	viewDashboardPecasCriticasMaisFaltantes: ViewDashboardPecasCriticasMaisFaltantes[] = [];
	viewDashboardPecasFaltantes: ViewDashboardPecasFaltantes[] = [];
	viewDashboardPecasMaisFaltantes: ViewDashboardPecasMaisFaltantes[] = [];
	viewDashboardPendenciaFiliais: ViewDashboardPendenciaFiliais[] = [];	
	viewDashboardPendenciaGlobal: ViewDashboardPendenciaGlobal[] = [];
	viewDashboardReincidenciaClientes: ViewDashboardReincidenciaClientes[] = [];
	viewDashboardReincidenciaFiliais: ViewDashboardReincidenciaFiliais[] = [];
	viewDashboardReincidenciaTecnicosMaisReincidentes: ViewDashboardReincidenciaTecnicosReincidentes[] = [];
	viewDashboardReincidenciaTecnicosMenosReincidentes: ViewDashboardReincidenciaTecnicosReincidentes[] = [];
	viewDashboardSLAClientes: ViewDashboardSLAClientes[] = [];
	viewDashboardSPA: ViewDashboardSPA[] = [];
	viewDashboardSPATecnicosMaiorDesempenho: ViewDashboardSPATecnicosDesempenho[] = [];
	viewDashboardSPATecnicosMenorDesempenho: ViewDashboardSPATecnicosDesempenho[] = [];
	viewDashboardTecnicosMaisPendentes: ViewDashboardTecnicosPendentes[] = [];
	viewDashboardTecnicosMenosPendentes: ViewDashboardTecnicosPendentes[] = [];
	viewDashboardDensidadeEquipamentos: ViewDashboardDensidadeEquipamentos[] = [];
	viewDashboardDensidadeTecnicos: ViewDashboardDensidadeTecnicos[] = [];
	viewDashboardIndicadoresDetalhadosSLACliente: ViewDashboardIndicadoresDetalhadosSLACliente[] = [];
	viewDashboardIndicadoresDetalhadosSLARegiao: ViewDashboardIndicadoresDetalhadosSLARegiao[] = [];
	viewDashboardIndicadoresDetalhadosSLATecnico: ViewDashboardIndicadoresDetalhadosSLATecnico[] = [];
	viewDashboardIndicadoresDetalhadosPendenciaTecnico: ViewDashboardIndicadoresDetalhadosPendenciaTecnico[] = [];
	viewDashboardIndicadoresDetalhadosPendenciaRegiao: ViewDashboardIndicadoresDetalhadosPendenciaRegiao[] = [];
	viewDashboardIndicadoresDetalhadosPendenciaCliente: ViewDashboardIndicadoresDetalhadosPendenciaCliente[] = [];
	viewDashboardIndicadoresDetalhadosReincidenciaCliente: ViewDashboardIndicadoresDetalhadosReincidenciaCliente[] = [];
	viewDashboardIndicadoresDetalhadosReincidenciaTecnico: ViewDashboardIndicadoresDetalhadosReincidenciaTecnico[] = [];
	viewDashboardIndicadoresDetalhadosReincidenciaRegiao: ViewDashboardIndicadoresDetalhadosReincidenciaRegiao[] = [];
	viewDashboardIndicadoresDetalhadosPerformance: ViewDashboardIndicadoresDetalhadosPerformance[] = [];
	viewDashboardIndicadoresDetalhadosChamadosAntigos: ViewDashboardIndicadoresDetalhadosChamadosAntigos[] = [];
	viewDashboardReincidenciaQuadrimestreFiliais: ViewDashboardReincidenciaQuadrimestreFiliais[] = [];
	viewDashboardPendenciaQuadrimestreFiliais: ViewDashboardPendenciaQuadrimestreFiliais[] = [];
}

export class ViewDashboardIndicadoresFiliais {
	filial: string;
	sla: number;
	pendencia: number;
	reincidencia: number;
	spa: number;
	osMedTec: number;
}

export class ViewDashboardChamadosMaisAntigosCorretivas {
	filial: string;
	cliente: string;
	modelo: string;
	os: number;
	dataAbertura: string;
}

export class ViewDashboardChamadosMaisAntigosOrcamentos {
	filial: string;
	cliente: string;
	modelo: string;
	os: number;
	dataAbertura: string;
}

export class ViewDashboardDisponibilidadeBBTSFiliais {
	filial: string;
	criticidade: string;
	indice?: number;
	saldo: string;
}

export class ViewDashboardDisponibilidadeBBTSMapaRegioes {
	regiao: string;
	mediaDisponibilidade?: number;
	qtdOSAbertas?: number;
	qtdOSFechadas?: number;
	backlogOS?: number;
	saldoHoras: string;
}

export class ViewDashboardDisponibilidadeBBTSMultasDisponibilidade {
	regiao: string;
	filial: string;
	criticidade: string;
	multa?: number;
}

export class ViewDashboardDisponibilidadeBBTSMultasRegioes {
	regiao: string;
	criticidade: string;
	multa?: number;
}

export class ViewDashboardDisponibilidadeTecnicos {
	filial: string;
	tecnicosComChamados?: number;
	tecnicosSemChamados?: number;
	tecnicosInativos?: number;
	tecnicosTotal?: number;
	qtdOSNaoTransferidasCorretivas?: number;
	qtdOSNaoTransferidasPreventivas?: number;
	qtdOSNaoTransferidasOrcamentoAprovado?: number;
	qtdOSNaoTransferidasOrcamentoRecallUpGrade?: number;
}

export class ViewDashboardDisponibilidadeTecnicosMediaGlobal {
	classificacao: string;
	media?: number;
}

export class ViewDashboardSPA {
	filial: string;
	percentual?: number;
}

export class ViewDashboardSLAClientes {
	cliente: string;
	percentual?: number;
}

export class ViewDashboardReincidenciaFiliais {
	filial: string;
	percentual: number;
}

export class ViewDashboardReincidenciaQuadrimestreFiliais {
	codFilial: number;
	filial: string;
	anoMes: string;
	percentual?: number;
}

export class ViewDashboardReincidenciaClientes {
	cliente: string;
	percentual?: number;
}

export class ViewDashboardSPATecnicosDesempenho {
	codFilial?: number;
	tecnico: string;
	filial: string;
	spa?: number;
	qtdAtendimentos?: number;
	osMedTec?: number;
}

export class ViewDashboardReincidenciaTecnicosReincidentes {
	tecnico: string;
	filial: string;
	spa?: number;
	qtdAtendimentos?: number;
}

export class ViewDashboardEquipamentosMaisReincidentes {
	modelo: string;
	cliente: string;
	serie: string;
	reincidencia?: number;
}

export class ViewDashboardPendenciaFiliais {
	filial: string;
	pendencia?: number;
}

export class ViewDashboardPendenciaQuadrimestreFiliais {
	codFilial: number;
	filial: string;
	anoMes: string;
	percentual?: number;
}

export class ViewDashboardTecnicosPendentes {
	tecnico: string;
	filial: string;
	pendencia?: number;
	qtdAtendimentos?: number;
}

export class ViewDashboardPendenciaGlobal {
	pendenciaGlobal?: number;
	chamadosPendentes?: number;
}

export class ViewDashboardPecasFaltantes {
	nomeUsuario: string;
	nomeFilial: string;
	dataFaltante: string;
	qtd?: number;
}

export class ViewDashboardPecasMaisFaltantes {
	codMagnus: string;
	nomePeca: string;
	qtdPecas?: number;
}

export class ViewDashboardPecasCriticasMaisFaltantes {
	codPeca?: number;
	codMagnus: string;
	nomePeca: string;
	qtdPecas?: number;
}

export class ViewDashboardPecasCriticaChamadosFaltantes {
	codPeca?: number;
	filial: string;
	os?: number;
	dataAbertura: string;
	cliente: string;
	equipamento: string;
}

export class ViewDashboardPecasCriticaEstoqueFaltantes {
	codPeca?: number;
	filial: string;
	qtd?: number;
}

export class ViewDashboardDensidadeEquipamentos {
	codFilial?: number;
	filial: string;
	equipamento: string;
	numSerie: string;
	cliente: string;
	latitude: string;
	longitude: string;
	coordenadas: string;
}

export class ViewDashboardDensidadeTecnicos {
	codFilial?: number;
	filial: string;
	tecnico: string;
	latitude: string;
	longitude: string;
	coordenadas: string;
}

export class ViewDashboardIndicadoresDetalhadosSLACliente {
	codFilial: number;
	filial: string;
	nomeFantasia: string;
	dentro: number;
	fora: number;
	totalGeral: number;
	percentual: number;
}

export class ViewDashboardIndicadoresDetalhadosSLARegiao {
	codFilial: number;
	filial: string;
	nomeRegiao: string;
	dentro: number;
	fora: number;
	totalGeral: number;
	percentual: number;
}

export class ViewDashboardIndicadoresDetalhadosSLATecnico {
	codFilial: number;
	filial: string;
	nomeTecnico: string;
	dentro: number;
	fora: number;
	totalGeral: number;
	percentual: number;
}

export interface ViewDashboardIndicadoresDetalhadosPendenciaCliente {
    codFilial: number;
    filial: string;
    nomeFantasia: string;
    chamadosMes: number;
    chamadosMesPecasPendentes: number;
    totalGeral: number;
    percentual: number;
}

export interface ViewDashboardIndicadoresDetalhadosPendenciaRegiao {
    codFilial: number;
    filial: string;
    nomeRegiao: string;
    chamadosMes: number;
    chamadosMesPecasPendentes: number;
    totalGeral: number;
    percentual: number;
}

export interface ViewDashboardIndicadoresDetalhadosPendenciaTecnico {
    codFilial: number;
    filial: string;
    nomeTecnico: string;
    chamadosMes: number;
    chamadosMesPecasPendentes: number;
    totalGeral: number;
    percentual: number;
}

export interface ViewDashboardIndicadoresDetalhadosReincidenciaRegiao {
    codFilial: number;
    filial: string;
    nomeRegiao: string;
    chamadosMes: number;
    chamadosMesReinc: number;
    totalGeral: number;
    percentual: number;
}

export interface ViewDashboardIndicadoresDetalhadosReincidenciaCliente {
    codFilial: number;
    filial: string;
    nomeFantasia: string;
    chamadosMes: number;
    chamadosMesReinc: number;
    totalGeral: number;
    percentual: number;
}

export interface ViewDashboardIndicadoresDetalhadosReincidenciaTecnico {
    codFilial: number;
    filial: string;
    nomeTecnico: string;
    chamadosMes: number;
    chamadosMesReinc: number;
    totalGeral: number;
    percentual: number;
}

export interface ViewDashboardIndicadoresDetalhadosPerformance {
    codFilial: number;
    filial: string;
    anoMes: string;
    tipo: string;
    percentual: number;
}

export interface ViewDashboardIndicadoresDetalhadosChamadosAntigos {
    codFilial: number;
    nomeFilial: string;
    nomeFantasia: string;
    modelo: string;
    modeloCompleto: string;
    codOS: number;
    dataAbertura: string;
    intervencao: string;
}

export enum ViewDashboardIndicadoresDetalhadosPerformanceTipoEnum {
	SLA = 'SLA',
	PENDENCIA = 'PENDENCIA',
	REINCIDENCIA = 'REINCIDENCIA'
}
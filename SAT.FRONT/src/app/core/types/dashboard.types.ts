import { QueryStringParameters } from "./generic.types";

export enum DashboardEnum {
	PERFORMANCE_FILIAIS_RESULTADO_GERAL = 'Performance das Filiais e Resultado Geral',
	DISPONIBILIDADE_BBTS = 'Disponibilidade BBTS',
	DISPONIBILIDADE_DOS_TECNICOS = 'Disponibilidade dos Técnicos',
	DASHBOARD_SPA = 'Dashboard SPA',
	SLA_CLIENTES = 'Índice de SLA por Cliente',
	REINCIDENCIA_FILIAIS = 'Reincidência por Filiais',
	REINCIDENCIA_CLIENTES = 'Reincidência por Clientes',
	PENDENCIA_FILIAIS = 'Pendência por Filiais',
	PECAS_FALTANTES_FILIAIS = 'Peças Faltantes por Filial',
	TOP_PECAS_FALTANTES = 'Peças Faltantes Mais Críticas',
	DENSIDADE = 'Dashboard Densidade'
}

export interface ViewDadosDashboardParameters extends QueryStringParameters {
	dashboardViewEnum: DashboardViewEnum;
	codPeca?: number;
	codFilial?: number;
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
	INDICADORES_DETALHADOS_REINCIDENCIA_REGIAO
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
}

export class ViewDashboardIndicadoresFiliais {
	filial: string;
	sla: number;
	pendencia: number;
	reincidencia: number;
	spa: number;
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
	mediaAtendimentoTecnicoDiaTodasIntervencoes?: number;
	mediaAtendimentoTecnicoDiaCorretivas?: number;
	mediaAtendimentoTecnicoDiaPreventivas?: number;
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

export class ViewDashboardReincidenciaClientes {
	cliente: string;
	percentual?: number;
}

export class ViewDashboardSPATecnicosDesempenho {
	tecnico: string;
	filial: string;
	spa?: number;
	qtdAtendimentos?: number;
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
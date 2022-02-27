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

export interface DashboardParameters extends QueryStringParameters {
	include?: DashboardIncludeEnum;
	filterType?: DashboardFilterEnum;
	agrupador?: DashboardAgrupadorEnum;
	tipo?: DashboardTipoEnum;
};

export enum DashboardAgrupadorEnum {

}

export enum DashboardIncludeEnum {

}

export enum DashboardFilterEnum {

}

export enum DashboardTipoEnum {
	DISPONIBILIDADE_BBTS = 1
}

export class DashboardDisponibilidade {
	public Regiao: string;
	public Criticidade?: number;
	public Filial: string;
	public PrcTotalFilial?: number;
	public CodFilial?: number;
}
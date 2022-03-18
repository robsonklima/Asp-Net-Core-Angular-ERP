import { EquipamentoContratoParameters } from './equipamento-contrato.types';
import { OrdemServicoParameters } from 'app/core/types/ordem-servico.types';

export interface ExportacaoParameters
{
	ordemServicoParameters?: string,
	equipamentoContratoParameters?: EquipamentoContratoParameters,
	exportacaoFormato: number,
	exportacaoTipo: number
};

export enum ExportacaoTipoEnum {
	ORDEM_SERVICO = 1,
	EQUIPAMENTO_CONTRATO = 2
}

export enum ExportacaoFormatoEnum {
	EXCEL = 1
}
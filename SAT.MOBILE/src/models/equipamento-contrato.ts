import { Contrato } from './contrato';
import { Equipamento } from './equipamento';

export class EquipamentoContrato {
    codEquipContrato: number;
    numSerie: string;
    numSerieCliente: string;
    equipamento: Equipamento;
    contrato: Contrato;
    indGarantia: number;    
}
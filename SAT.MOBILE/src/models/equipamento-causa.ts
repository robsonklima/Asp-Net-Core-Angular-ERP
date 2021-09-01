import { Equipamento } from './equipamento';
import { Causa } from './causa';

export class EquipamentoCausa {
  equipamento: Equipamento;
  causas: Causa[] = [];
}
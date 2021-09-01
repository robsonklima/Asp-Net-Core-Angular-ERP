import { Causa } from './causa';
import { Defeito } from './defeito';

export class DefeitoCausa {
  causa: Causa;
  defeitos: Defeito[] = [];
}
import { Filial } from "./filial";

export class Condutor {
  nome: string;
  matricula: string;
  rg: string;
  cpf: string;
  cnh: string;
  categorias: string[];
  cnhValidade: string;
  filial: Filial;
  finalidadesUso: string[];
}
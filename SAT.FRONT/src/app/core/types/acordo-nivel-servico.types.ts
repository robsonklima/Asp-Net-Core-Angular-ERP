import { Meta, QueryStringParameters } from "./generic.types";

export interface AcordoNivelServico {
  codSLA: number;
  nomeSLA: string;
  descSLA: string;
  tempoInicio?: number;
  tempoReparo?: number;
  tempoSolucao?: number;
  indAgendamento?: number;
  indHorasUteis?: number;
  indFeriado?: number;
  indSegunda?: number;
  indTerca?: number;
  indQuarta?: number;
  indQuinta?: number;
  indSexta?: number;
  indSabado?: number;
  indDomingo?: number;
}

export interface AcordoNivelServicoData extends Meta {
  items: AcordoNivelServico[];
};

export interface AcordoNivelServicoParameters extends QueryStringParameters {
  codSLA?: number;
  nomeSLA?: string;
};
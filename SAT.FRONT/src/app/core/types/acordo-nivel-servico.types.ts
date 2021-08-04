import { Meta, QueryStringParameters } from "./generic.types";

export interface AcordoNivelServico {
    codSLA: number;
    nomeSLA: string;
    descSLA: string;
}

export interface AcordoNivelServicoData extends Meta {
    acordosNivelServico: AcordoNivelServico[];
  };
  
export interface AcordoNivelServicoParameters extends QueryStringParameters { };
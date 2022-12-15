import { Meta, QueryStringParameters } from "./generic.types";

export interface Improdutividade {
  codImprodutividade: number;
  descImprodutividade: string;
  indAtivo: number;
}

export interface ImprodutividadeData extends Meta {
  items: Improdutividade[];
};

export interface ImprodutividadeParameters extends QueryStringParameters {
  codTipoChamadoSTN?: number;
};

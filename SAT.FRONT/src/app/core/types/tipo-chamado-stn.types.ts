import { Meta, QueryStringParameters } from "./generic.types";

export interface TipoChamadoSTN {
  codTipoChamadoSTN: number;
  descTipoChamadoSTN: string;
  indAtivo: number;
}

export interface TipoChamadoSTNData extends Meta {
  items: TipoChamadoSTN[];
};

export interface TipoChamadoSTNParameters extends QueryStringParameters {
  codTipoChamadoSTN?: number;
};
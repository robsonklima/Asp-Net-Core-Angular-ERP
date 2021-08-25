import { Meta, QueryStringParameters } from "./generic.types";

export class TipoIntervencao {
    codTipoIntervencao: number;
    codETipoIntervencao: string;
    nomTipoIntervencao: string;
    calcPreventivaIntervenc: number;
    verificaReincidenciaInt: number;
    codTraducao: number;
    indAtivo: number;
}

export interface TipoIntervencaoData extends Meta {
    items: TipoIntervencao[];
};

export interface TipoIntervencaoParameters extends QueryStringParameters {
    codTipoIntervencao?: number;
    indAtivo?: number;
};
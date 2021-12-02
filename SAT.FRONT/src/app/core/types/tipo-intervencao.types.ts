import { Meta, QueryStringParameters } from "./generic.types";

export class TipoIntervencao
{
    codTipoIntervencao: number;
    codETipoIntervencao: string;
    nomTipoIntervencao: string;
    calcPreventivaIntervenc: number;
    verificaReincidenciaInt: number;
    codTraducao: number;
    indAtivo: number;
}

export interface TipoIntervencaoData extends Meta
{
    items: TipoIntervencao[];
};

export interface TipoIntervencaoParameters extends QueryStringParameters
{
    codTipoIntervencao?: number;
    indAtivo?: number;
};

export enum TipoIntervencaoEnum
{
    CORRETIVA = 2,
    ORCAMENTO = 5,
    AUTORIZACAO_DESLOCAMENTO = 14,
    ORC_APROVADO = 17,
    ORC_REPROVADO = 18,
    ORC_PEND_APROVACAO_CLIENTE = 19,
    ORC_PEND_FILIAL_DETALHAR_MOTIVO = 20
};
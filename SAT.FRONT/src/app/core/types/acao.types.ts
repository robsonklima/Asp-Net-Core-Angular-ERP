import { Meta, QueryStringParameters } from "./generic.types";

export class Acao {
    codAcao: number;
    codEAcao: string;
    nomeAcao: string;
    indAtivo?: number;
}

export interface AcaoData extends Meta {
    items: Acao[]
};

export interface AcaoParameters extends QueryStringParameters {
    indAtivo?: number;
};

export enum AcaoEnum {
    ACOMPANHAMENTO = 1,
    AJUSTE = 2,
    ATENDIMENTO_PARCIAL = 3,
    ATUALIZAÇÃO = 4,
    BAIXA_DE_SOFTWARE = 5,
    CONFIGURAÇÃO = 6,
    DESENROSCO_DE_NUMERÁRIO = 7,
    FECHAMENTO_PARCIAL = 8,
    FORMATAÇÃO = 9,
    INSTALAÇÃOREINSTALAÇÃO = 10,
    LIMPEZA = 11,
    LUBRIFICAÇÃO = 12,
    NÃO_AUTORIZOU = 14,
    NÃO_COMPARECEU = 15,
    NÃO_LIBEROU_EQUIPAMENTO = 16,
    ORÇAMENTO_PARA_APROVAÇÃO = 17,
    ORIENTAÇÃO = 18,
    PENDÊNCIA_DE_PEÇA = 19,
    PREVENTIVA = 20,
    REMOÇÃO_DE_MAU_CONTATO = 21,
    REPARO = 22,
    RESETE_DO_EQUIPAMENTO = 23,
    RETIRADA_DE_OBJETO_ESTRANHO = 24,
    RETIRADO_PARA_CONSERTO = 25,
    TROCA_SUBSTITUIÇÃO = 26,
    INCIDENTE_DEVOLVIDO = 27
}
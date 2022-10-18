import { Meta, QueryStringParameters } from "./generic.types";
import { ORItem } from "./or-item.types";
import { ORStatus } from "./or-status.types";

export interface OR {
    codOR: number;
    dataHoraOR: string;
    codOrigem: number | null;
    codDestino: number | null;
    codStatusOR: number;
    orStatus: ORStatus;
    numNF: string;
    volumes: string;
    codModal: number | null;
    dataExpedicao: string;
    indAtivo: number;
    codUsuarioCad: string;
    dataHoraCad: string;
    minuta: string;
    codTransportadora: number | null;
    orItens: ORItem[];
}

export interface ORData extends Meta {
    items: OR[];
};

export interface ORParameters extends QueryStringParameters {

}
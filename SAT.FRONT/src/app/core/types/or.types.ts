import { Meta, QueryStringParameters } from "./generic.types";
import { ORDestino } from "./or-destino-types";
import { ORItem } from "./or-item.types";
import { ORStatus } from "./or-status.types";
import { Usuario } from "./usuario.types";

export interface OR {
    codOR?: number;
    dataHoraOR: string;
    codOrigem?: number | null;
    codDestino?: number | null;
    codStatusOR?: number;
    orStatus?: ORStatus;
    numNF?: string;
    volumes?: string;
    codModal?: number;
    dataExpedicao?: string;
    indAtivo: number;
    codUsuarioCad: string;
    dataHoraCad: string;
    minuta?: string;
    codTransportadora?: number;
    orItens: ORItem[];
    destino?: ORDestino;
    usuarioCadatro?: Usuario;
}

export interface ORData extends Meta {
    items: OR[];
};

export interface ORParameters extends QueryStringParameters {
    codStatus?: string;
    codOR?: number;
}
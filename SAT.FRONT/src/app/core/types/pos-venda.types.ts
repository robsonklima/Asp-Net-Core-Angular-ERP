import { Meta } from "./generic.types";

export class PosVenda {
    codPosvenda: number;
    nome: string;
    codigoLogix: number;
}

export interface PosVendaData extends Meta {
    items: PosVenda[];
};

export class PosVendaParameters {
    codPosvenda?: number;
    nome?: string;
    codigoLogix?: number;
}
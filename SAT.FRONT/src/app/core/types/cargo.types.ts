import { Meta } from "@angular/platform-browser";
import { QueryStringParameters } from "./generic.types";

export class Cargo {
    codCargo: number;
    nomeCargo: string;
    indAtivo: boolean;
}

export interface CargoData extends Meta {
    items: Cargo[];
};

export interface CargoParameters extends QueryStringParameters {
    codCargo?: number;
    indAtivo?: number;
};

import { Meta, QueryStringParameters } from "./generic.types";

export interface ORStatus {
    codStatus: number;
    descStatus: string;
    indAtivo: number | null;
}

export interface ORStatusData extends Meta {
    items: ORStatus[];
};

export interface ORStatusParameters extends QueryStringParameters {

}
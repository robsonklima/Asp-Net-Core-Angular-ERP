import { QueryStringParameters } from "./generic.types";

export interface NLogRegistro {
    time: string;
    level: string;
    nested: NLogNested;
}

export interface NLogNested {
    message: string;
}

export interface NLogParameters extends QueryStringParameters { };
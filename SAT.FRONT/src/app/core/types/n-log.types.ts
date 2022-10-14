import { QueryStringParameters } from "./generic.types";

export interface NLogRegistro {
    time: string;
    level: string;
    nested: NLogNested;
}

export interface NLogNested {
    application: string;
    message: string;
}

export interface NLogParameters extends QueryStringParameters { };
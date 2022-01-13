import { QueryStringParameters } from "./generic.types";

export enum GeolocalizacaoServiceEnum
{
    GOOGLE,
    NOMINATIM
}

export interface GeolocalizacaoParameters extends QueryStringParameters
{
    enderecoCep?: string;
    latitudeOrigem?: string;
    longitudeOrigem?: string;
    latitudeDestino?: string;
    longitudeDestino?: string;
    geolocalizacaoServiceEnum: GeolocalizacaoServiceEnum;
};

export interface Geolocalizacao
{
    enderecoCEP: string;
    latitude: string;
    longitude: string;
    bairro: string;
    endereco: string;
    numero: string;
    estado: string;
    pais: string;
    cidade: string;
    duracao: number;
    distancia: number;
}
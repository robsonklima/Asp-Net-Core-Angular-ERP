import { Meta, QueryStringParameters } from "./generic.types";

export class CheckinCheckout
{
    codCheckInCheckOut: number;
    tipo: string;
    modalidade: string;
    codOS: number;
    codRAT: number;
    latitude: string;
    longitude: string;
    codUsuarioTecnico: string;
    codUsuarioPa: string;
    dataHoraCadSmartphone: string;
    dataHoraCad: string;
    distanciaLocalMetros: string;
    distanciaLocalDescricao: string;
    duracaoLocalSegundos: string;
    duracaoLocalDescricao: string;
}

export interface CheckinCheckoutData extends Meta
{
    items: CheckinCheckout[];
};

export interface CheckinCheckoutParameters extends QueryStringParameters
{    
    codOS?: number;
    codRAT?: number;
    codUsuarioTecnico?: string;
};

export const CheckinCheckoutTipo = {
    CHECKIN: 'CHECKIN',
    CHECKOUT: 'CHECKOUT'
}
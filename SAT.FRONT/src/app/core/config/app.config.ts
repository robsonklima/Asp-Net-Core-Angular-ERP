import { Layout } from 'app/layout/layout.types';

export type Scheme = 'auto' | 'dark' | 'light';
export type Theme = 'default' | string;

export interface AppConfig
{
    layout: Layout;
    scheme: Scheme;
    theme: Theme;
    api: string;
    map_quest_keys: string[];
    tipo_intervencao: any;
    status_servico: any;
}

export const appConfig: AppConfig = {
    layout: 'dense',
    scheme: 'light',
    theme : 'brand',
    //api: 'https://sat.perto.com.br/SAT.V2.API/api',
    //api: 'https://localhost:5001/api',
    api: 'https://localhost:44341/api',
    map_quest_keys: [
        'Io2YoCuiLJ8SFAW14pXwozOSYgxPAOM1', 'nCEqh4v9AjSGJreT75AAIaOx5vQZgVQ2',
        'KDVU5s6t3bOZkAksJfpuUiygIFPlXH9U', 'klrano7LC8Vk88QmjXvAt9jUrjzGReiz',
        'bP1zqnkhSVsAj5gL8GucMipVqDRPNmID', 'A0bAhXKQNNEqjFWUUOvR2HhAStiElB0L',
        'EDvdlS7xGN5U8WqHFiXMWXmXGwNSAhvh', 'XsjlWnkAo5fPMGhJ8l3RTwEpQfPINIGU', 
        'tbYhdvKIFCxkDFjoGATSHmVPL54ItdlC', 'l19CNtzjRZmwVCncGjBycgFV5WSUGYQ1',
    ],
    status_servico: {
        aberto: 1,
        cancelado: 2,
        transferido: 8
    },
    tipo_intervencao: {
        corretiva: 2,
        preventiva: 6
    }
};
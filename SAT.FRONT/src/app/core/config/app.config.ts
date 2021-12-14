import { Layout } from 'app/layout/layout.types';

export type Scheme = 'auto' | 'dark' | 'light';
export type Theme = 'default' | string;
export type Api = 'https://localhost:44341/api' |
    'https://sat.perto.com.br/SAT.V2.API/api' |
    'https://apisat-homologacao.perto.com.br/api' |
    'https://localhost:5001/api' | string;

export interface AppConfig
{
    layout: Layout;
    scheme: Scheme;
    theme: Theme;
    google_key: string;
    api: Api;
    tempo_atualizacao_dashboard_minutos: number;
    map_quest_keys: string[];
    tailwind_css: string;
    autonomia_veiculo_frota: number;
    rd_centro_de_custo: string;
    system_user: string;
}

export const appConfig: AppConfig = {
    layout: 'dense',
    scheme: 'light',
    theme: 'brand',
    api: 'https://sat.perto.com.br/SAT.V2.API/api',
    map_quest_keys: [
        'Io2YoCuiLJ8SFAW14pXwozOSYgxPAOM1', 'nCEqh4v9AjSGJreT75AAIaOx5vQZgVQ2',
        'KDVU5s6t3bOZkAksJfpuUiygIFPlXH9U', 'klrano7LC8Vk88QmjXvAt9jUrjzGReiz',
        'bP1zqnkhSVsAj5gL8GucMipVqDRPNmID', 'A0bAhXKQNNEqjFWUUOvR2HhAStiElB0L',
        'EDvdlS7xGN5U8WqHFiXMWXmXGwNSAhvh', 'XsjlWnkAo5fPMGhJ8l3RTwEpQfPINIGU',
        'tbYhdvKIFCxkDFjoGATSHmVPL54ItdlC', 'l19CNtzjRZmwVCncGjBycgFV5WSUGYQ1',
    ],
    tempo_atualizacao_dashboard_minutos: 5,
    google_key: 'AIzaSyC4StJs8DtJZZIELzFgJckwrsvluzRo_WM',
    tailwind_css: 'https://unpkg.com/tailwindcss@^1.0/dist/tailwind.min.css',
    autonomia_veiculo_frota: 13,
    rd_centro_de_custo: "5001",
    system_user: 'ADMIN'
};
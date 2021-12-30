export interface AppConfig
{
    layout: string;
    scheme: string;
    theme: string;
    google_key: string;
    api: string;
    tempo_atualizacao_dashboard_minutos: number;
    map_quest_keys: string[];
    tailwind_css: string;
    autonomia_veiculo_frota: number;
    rd_centro_de_custo: string;
    system_user: string;
}

export const apiConst = {
	LOCALHOST_44341: 'https://localhost:44341/api',
    LOCALHOST_5001: 'https://localhost:5001/api',
    PROD: 'https://sat.perto.com.br/SAT.V2.API/api'
}

export const schemeConst = {
    AUTO: 'auto',
    DARK: 'dark',
    LIGHT: 'light'
}

export const layoutConst = {
    DENSE: 'dense',
    EMPTY: 'empty',
    CENTERED: 'centered',
    ENTERPRISE: 'enterprise',
    MATERIAL: 'material',
    MODERN: 'modern',
    CLASSIC: 'classic',
    CLASSY: 'classy',
    COMPACT: 'compact',
    FUTURISTIC: 'futuristic',
    THIN: 'thin'
}

export const themeConst = {
    BRAND: 'brand'
}

export const keysConst = {
    MAPQUEST: [
        'Io2YoCuiLJ8SFAW14pXwozOSYgxPAOM1', 'nCEqh4v9AjSGJreT75AAIaOx5vQZgVQ2',
        'KDVU5s6t3bOZkAksJfpuUiygIFPlXH9U', 'klrano7LC8Vk88QmjXvAt9jUrjzGReiz',
        'bP1zqnkhSVsAj5gL8GucMipVqDRPNmID', 'A0bAhXKQNNEqjFWUUOvR2HhAStiElB0L',
        'EDvdlS7xGN5U8WqHFiXMWXmXGwNSAhvh', 'XsjlWnkAo5fPMGhJ8l3RTwEpQfPINIGU',
        'tbYhdvKIFCxkDFjoGATSHmVPL54ItdlC', 'l19CNtzjRZmwVCncGjBycgFV5WSUGYQ1',
    ],
    GOOGLE: 'AIzaSyC4StJs8DtJZZIELzFgJckwrsvluzRo_WM'
}

export const tailwindcssConst = {
    CSS: 'https://unpkg.com/tailwindcss@^1.0/dist/tailwind.min.css'
}
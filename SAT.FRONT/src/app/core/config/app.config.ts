import { Layout } from 'app/layout/layout.types';
import { environment } from 'environments/environment';
export type Scheme = 'auto' | 'dark' | 'light';
export type Theme = 'default' | string;

export interface AppConfig {
    layout: Layout;
    scheme: Scheme;
    theme: Theme;
    google_key: string;
    api: string;
    tempo_atualizacao_dashboard_minutos: number;
    tailwind_css: string;
    autonomia_veiculo_frota: number;
    rd_centro_de_custo: string;
    system_user: string;
    email_equipe: string;
    parametroReajusteValorOrcamento: number;
    quillModules: any
}

export const appConfig: AppConfig = {
    layout: 'dense',
    scheme: 'light',
    theme: 'brand',
    api: environment.apiUrl,
    tempo_atualizacao_dashboard_minutos: 5,
    google_key: 'AIzaSyC4StJs8DtJZZIELzFgJckwrsvluzRo_WM',
    tailwind_css: 'https://unpkg.com/tailwindcss@^1.0/dist/tailwind.min.css',
    autonomia_veiculo_frota: 13,
    rd_centro_de_custo: "5001",
    system_user: 'SAT',
    email_equipe: 'equipe.sat@perto.com.br',
    parametroReajusteValorOrcamento: 0.81,
    quillModules: {
        toolbar: [
            ['bold', 'italic', 'underline', 'strike'],
            ['blockquote', 'code-block'],
            [{ 'header': 1 }, { 'header': 2 }],
            [{ 'list': 'ordered' }, { 'list': 'bullet' }],
            [{ 'script': 'sub' }, { 'script': 'super' }],
            [{ 'indent': '-1' }, { 'indent': '+1' }],
            [{ 'size': ['small', false, 'large', 'huge'] }],
            [{ 'align': [] }],
            ['clean'],
            ['link', 'image']
        ]
    }
};
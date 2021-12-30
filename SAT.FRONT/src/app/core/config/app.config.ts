import { apiConst, AppConfig, layoutConst, keysConst, schemeConst, themeConst, tailwindcssConst } from './app.config.types';

export const appConfig: AppConfig = {
    layout: layoutConst.DENSE,
    scheme: schemeConst.LIGHT,
    theme: themeConst.BRAND,
    api: apiConst.LOCALHOST_5001,
    map_quest_keys: keysConst.MAPQUEST,
    tempo_atualizacao_dashboard_minutos: 5,
    google_key: keysConst.GOOGLE,
    tailwind_css: tailwindcssConst.CSS,
    autonomia_veiculo_frota: 13,
    rd_centro_de_custo: "5001",
    system_user: 'SAT'
};
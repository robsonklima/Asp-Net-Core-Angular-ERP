namespace SAT.MODELS.Entities.Constants
{
    public class Constants
    {
        public static string SISTEMA_NOME = "SAT";
        public static string AGENDADOR_NOME = "SAT_AGENDADOR";
        public static string SMTP_HOST = "zimbragd.perto.com.br";
        public static int SMTP_PORT = 587;
        public static string SMTP_USER = "aplicacao.sat@perto.com.br";
        public static string EQUIPE_SAT_EMAIL = "equipe.sat@perto.com.br";
        public static string MAP_QUEST_KEY = "nCEqh4v9AjSGJreT75AAIaOx5vQZgVQ2";
        public static string GOOGLE_API_KEY = "AIzaSyC4StJs8DtJZZIELzFgJckwrsvluzRo_WM";
        public static string SMTP_PASSWORD = "S@aPlic20(v";
        public static string DB_PROD = "Prod";
        public static string DB_HOMOLOG = "Homolog";
        public static int TEMPO_IISLOG_MS = 3 * 60 * 1000;
        public static string IIS_LOG_PATH = @"D:\SAT\Branch\SAT.V2\SAT.API\Logs\IIS\";

        // Status de Serviço
        public static int TRANFERIDO = 8;
        public static int CANCELADO = 2;
        public static int AGUARDANDO_CONTATO_COM_CLIENTE = 14;
        public static int AGUARDANDO_DECLARACAO = 15;
        public static int CANCELADO_COM_ATENDIMENTO = 16;
        public static int FECHADO = 3;
        public static int PECAS_PENDENTES = 7;

        // Alertas para OS
        public static string WARNING = "warn";
        public static string PRIMARY = "primary";
        public static string SUCCESS = "success";

        public static string USUARIO_SERVICO = "SERVIÇO";
        public static int[] EQUIPAMENTOS_PINPAD = { 153, 856, 1121 };
        public static int[] TIPO_INTERVENCAO_GERAL = { 2, 5, 17, 18, 19, 20 };
        public static int[] EQUIPS_TDS_TCC_TOP_TR1150 = { 91, 101, 112, 114, 151, 263, 264, 298, 320, 329, 407, 410, 415, 447, 448, 459, 460, 603, 604, 628, 779, 865, 958, 959, 960, 961, 962, 970, 1090 };
        public static int CONTRATO_BB_TECNOLOGIA = 3145;

        // Tipos de Intervenção
        public static int ALTERACAO_ENGENHARIA = 1;
        public static int CORRETIVA = 2;
        public static int DESINSTALACAO = 3;
        public static int INSTALACAO = 4;
        public static int PREVENTIVA = 6;
        public static int REINSTALACAO = 7;

        // Mensagens
        public static string NAO_FOI_POSSIVEL_DELETAR = "Não foi possível deletar o registro";
        public static string NAO_FOI_POSSIVEL_ATUALIZAR = "Não foi possível atualizar o registro";
        public static string NAO_FOI_POSSIVEL_CRIAR = "Não foi possível criar o registro";
        public static string NAO_FOI_POSSIVEL_OBTER = "Não foi possível atualizar o(s) registro(s)";
        public static string USUARIO_OU_SENHA_INVALIDOS = "Usuário ou senha inválidos";
        public static string SENHA_INVALIDA = "Senha inválida";
        public static string ERRO_ALTERAR_SENHA = "Erro ao alterar a senha";
        public static string ERRO_CONSULTAR_COORDENADAS = "Erro ao consultar as coordenadas";

        // Filiais
        public static int FRS = 4;

        // Clientes
        public static int CLIENTE_BB = 1;
        public static int CLIENTE_BANRISUL = 2;

        // Modelos
        public static int POS = 85;
        public static int POS_2020 = 107;
        public static int POS_2024 = 134;
        public static int POS_2025 = 147;
        public static int POS_GPRS = 157;
        public static int POS_ADSL = 158;
        public static int POS_VELOH_G = 172;
        public static int POS_VELOH_C = 204;
        public static int POS_VELOH_W = 268;
        public static int POS_VELOH_3 = 289;
        public static int POS_290_20_000 = 400;
        public static int POS_2020_290_20_012 = 401;

        // Permissoes
        public static string PERFIL_PONTO = "3";
        public static string SEM_NADA = "S/N";

        // Clientes
        public static int SICOOB = 68;
        public static int SICOOB_CONFEDERACAO = 246;

        //Email

        public static string ASSINATURA_EMAIL = @"
                                                <br><br>
                                                Equipe SAT<br> 
                                                Perto S.A. – Tecnologia para Bancos e Varejo<br> 
                                                Ramal (51) 2126-6944<br> 
                                                Whatsapp: (51) 997144990<br>";

    }
}

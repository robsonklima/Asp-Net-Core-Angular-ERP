namespace SAT.MODELS.Entities.Constants
{
    public class Constants
    {
        // Status de Serviço
        public static int TRANFERIDO = 8;
        public static int CANCELADO = 2;
        public static int AGUARDANDO_CONTATO_COM_CLIENTE = 14;
        public static int AGUARDANDO_DECLARACAO = 15;
        public static int CANCELADO_COM_ATENDIMENTO = 16;
        public static int FECHADO = 3;

        // Mensagens
        public static string NAO_FOI_POSSIVEL_DELETAR = "Não foi possível deletar o registro";
        public static string NAO_FOI_POSSIVEL_ATUALIZAR = "Não foi possível atualizar o registro";
        public static string NAO_FOI_POSSIVEL_CRIAR = "Não foi possível criar o registro";
        public static string NAO_FOI_POSSIVEL_OBTER = "Não foi possível atualizar o(s) registro(s)";
        public static string USUARIO_OU_SENHA_INVALIDOS = "Usuário ou senha inválidos";

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
    }
}

﻿namespace SAT.MODELS.Entities.Constants
{
    public class Constants
    {
        public static string SISTEMA_NOME = "SAT";

        // Status de Serviço
        public static int TRANFERIDO = 8;
        public static int CANCELADO = 2;
        public static int AGUARDANDO_CONTATO_COM_CLIENTE = 14;
        public static int AGUARDANDO_DECLARACAO = 15;
        public static int CANCELADO_COM_ATENDIMENTO = 16;
        public static int FECHADO = 3;
        public static int PECAS_PENDENTES = 7;

        // Alertas para OS
        public static string WARNING = "WARNING";
        public static string INFO = "INFO";
        public static string SUCCESS = "SUCCESS";
        public static int[] EQUIPAMENTOS_PINPAD = { 153, 856, 1121 };

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
    }
}

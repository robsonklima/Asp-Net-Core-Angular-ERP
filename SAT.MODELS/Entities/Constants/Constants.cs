using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace SAT.MODELS.Entities.Constants
{
    public class Constants
    {
        public static string SISTEMA_NOME = "SAT 2.0";
        public static string SISTEMA_CAMADA_API = "Backend";
        public static string SISTEMA_CAMADA_TASKS = "TASKS";
        public static string AGENDADOR_NOME = "SAT_AGENDADOR";
        public static string EQUIPE_SAT_EMAIL = "equipe.sat@perto.com.br";
        public static string BANRISUL_EMAIL = "tecnologia_suporte_service_desk@banrisul.com.br";
        public static string BANRISUL_EMAIL2 = "tecnologia_SDMOV@banrisul.com.br";
        public static string MAP_QUEST_KEY = "nCEqh4v9AjSGJreT75AAIaOx5vQZgVQ2";
        public static string GOOGLE_API_KEY = "AIzaSyC4StJs8DtJZZIELzFgJckwrsvluzRo_WM";
        public static string DB_PROD = "Prod";
        public static string DB_HOMOLOG = "Homolog";
        public static int TEMPO_IISLOG_MS = 3 * 60 * 1000;
        public static string IIS_LOG_PATH = @"D:\SAT\Branch\SAT.V2\SAT.API\Logs\IIS\";
        public static string VONAGE_KEY = @"eab57cf8";
        public static string VONAGE_SECRET = @"NX7ZdN7nNDrxoNyC";
        public static string INTEGRACAO_SENIOR = "Integração Senior";
        public static string INTEGRACAO_SENIOR_API_URL = "http://??";
        public static string INTEGRACAO_FINANCEIRO_API_URL = "http://perto31.perto.com.br";
        public static string INTEGRACAO_ZAFFARI_API_URL = "https://capgeminibrdev.service-now.com/api/now/table/u_integration_zaffari_perto";
        public static string INTEGRACAO_ZAFFARI_USER = "userperto";
        public static string INTEGRACAO_ZAFFARI_PASSWORD = "hqP9FzOL0g_lzba-";
        public static string INTEGRACAO_SENIOR_USER = "??";
        public static string INTEGRACAO_SENIOR_PASSWORD = "??";
        public static string INTEGRACAO_FINANCEIRO_USER = "sistemasat";
        public static string INTEGRACAO_FINANCEIRO_PASSWORD = "e62076f38d1d367931e00a4c6785f67e";
        public static string INTEGRACAO_PROTEGE_API_URL = "https://servicedeskhomolog.protege.com.br/CherwellAPI/";
        public static string INTEGRACAO_PROTEGE_USER = "CHERWELL\\integra.perto";
        public static string INTEGRACAO_PROTEGE_PASSWORD = "integrA#23";
        public static string INTEGRACAO_PROTEGE_CLIENT_ID = "fbcc8044-cb27-4651-81e9-d53ecf6f71ea";
        public static string INTEGRACAO_PROTEGE_GRANT_TYPE = "password";
        public static string INTEGRACAO_PROTEGE_AUTH_MODE = "internal";
        public static string EMP_NOME = "Perto S.A";
        public static string EMP_SLOGAN = "Tecnologia para Bancos e Varejo";
        public static string EMP_ENDERECO = "Rua Nissin Castiel, 640 Distrito Industrial";
        public static string EMP_COMPLEMENTO = "CEP: 94045-420 | Gravataí | RS | Brasil";
        public static string EMP_TELEFONE = "(51) 3489-8700";
        public static string EMP_SITE = "www.perto.com.br";

        // Email
        public static Office365Config OFFICE_365_CONFIG = new Office365Config
        { // Apenas para obtencao do Token
            Host = "smtp.office365.com",
            Port = 587,
            ClientID = "d1d8e679-7fa3-4461-9b71-56b7a290ea96",
            ClientSecret = "Uyt8Q~DxTOYrhXeLwr-K_xdRyw.iHofX8MhG2aND",
            Instance = "https://login.microsoftonline.com/{0}",
            Tenant = "grupodigicon.onmicrosoft.com",
            ApiUri = "https://graph.microsoft.com/",
        };

        public static Office365EmailConfig EMAIL_APLICACAO_CONFIG = new Office365EmailConfig
        {
            Username = "aplicacao.sat@perto.com.br",
            Password = "6PyhnD=FUius^=&",
            ClientID = "1b71cdeb-01a7-493e-a4a1-aedacb2488e3"
        };

        public static Office365EmailConfig EMAIL_BANRISUL_CONFIG = new Office365EmailConfig
        {
            Username = "gss.sat.banrisul@perto.com.br",
            Password = "Aolv!@#g8A)g4",
            ClientID = "4a0d5062-6188-4067-af77-e5eb261b2119"
        };

        // Application types
        public static string APPLICATION_JSON = "application/json";

        // Status
        public static int ATIVO = 1;
        public static int INATIVO = 0;

        // Status de Serviço
        public static int STATUS_SERVICO_ABERTO = 1;

        // Alertas para OS
        public static string WARNING = "warn";
        public static string PRIMARY = "primary";
        public static string SUCCESS = "success";

        public static string USUARIO_SERVICO = "SERVIÇO";
        public static int[] EQUIPAMENTOS_PINPAD = { 153, 856, 1121 };
        public static int[] TIPO_INTERVENCAO_GERAL = { 2, 5, 17, 18, 19, 20 };
        public static int[] EQUIPS_TDS_TCC_TOP_TR1150 = {
            91, 101, 112, 114, 151, 263, 264, 298, 320, 329, 407, 410, 415,
            447, 448, 459, 460, 603, 604, 628, 779, 865, 958, 959, 960, 961,
            962, 970, 1090
        };
        public static int CONTRATO_BB_TECNOLOGIA = 3145;

        // Tipos de Serviço
        public static int TIPO_SERVICO_SOFTWARE_EMBARCADO = 59;
        public static int TIPO_SERVICO_MONITORAMENTO_REMOTO = 60;

        // Tipos de Intervenção
        public static int CORRETIVA = 2;

        // Mensagens
        public static string NAO_FOI_POSSIVEL_ATUALIZAR = "Não foi possível atualizar o registro";
        public static string USUARIO_OU_SENHA_INVALIDOS = "Usuário ou senha inválidos";
        public static string SENHA_INVALIDA = "Senha inválida";
        public static string ERRO_ALTERAR_SENHA = "Erro ao alterar a senha";
        public static string ERRO_CONSULTAR_COORDENADAS = "Erro ao consultar as coordenadas";
        public static string ERROR = "Ocorreu um erro";

        // Clientes
        public const int CLIENTE_BB = 1;
        public const int CLIENTE_ZAFFARI = 16;
        public const int CLIENTE_BANRISUL = 2;
        public const int CLIENTE_SAFRA = 8;
        public const int CLIENTE_CEF = 58;
        public const int CLIENTE_SICOOB = 68;
        public const int CLIENTE_SICREDI = 88;
        public const int CLIENTE_BANCO_DA_AMAZONIA = 109;
        public const int CLIENTE_BRB = 197;
        public const int CLIENTE_ITAU = 251;
        public const int CLIENTE_BANESTES = 331;
        public const int CLIENTE_BANPARA = 426;
        public const int CLIENTE_SAQUE_PAGUE = 434;

        // Modelos
        public static int TMF_2100_290_01_906 = 24;
        public static int TC_3100_290_02_288 = 34;
        public static int TS_2100_290_01_908 = 51;
        public static int TMD_2100_290_01_904 = 132;
        public static int TMF_5100_290_01_979 = 322;
        public static int TMF_4100ABNT_290_01_434 = 346;
        public static int TMF_5100_290_01_898 = 351;
        public static int TMF_5100_290_01_975 = 353;
        public static int TMD_2100_290_02_037 = 355;
        public static int TS_2100_290_01_918 = 361;
        public static int TMF_2100_290_02_057 = 434;
        public static int TMF_2100_P_I_290_02_058 = 435;
        public static int TMD_5100_290_01_980 = 438;
        public static int TMF_4100_290_01_776 = 440;
        public static int TAAMC_290_02_049 = 442;
        public static int TAASF_290_02_047 = 443;
        public static int TMF_4100_290_02_028 = 461;
        public static int TMF_4100_290_01_863 = 462;
        public static int TMD_4100_290_01_834 = 524;
        public static int TMF_5100_290_01_604 = 525;
        public static int TMD_5100_290_02_025 = 532;
        public static int TMF_5110_290_01_976 = 533;
        public static int TAS_3100_290_02_252 = 561;
        public static int TPC_4110_290_01_941 = 620;
        public static int TMF_5100_290_01_940 = 621;
        public static int TS_5150_290_01_942 = 622;
        public static int TS_5160_290_01_943 = 623;
        public static int TS_3100F_290_01_552 = 659;
        public static int TMF_5110_290_02_303 = 695;
        public static int TS_2100_290_02_322 = 707;
        public static int TAART_290_02_309 = 740;
        public static int TMR_5100_290_02_326 = 742;
        public static int TMR_5150_290_02_061 = 784;
        public static int TMR_5160_290_02_062 = 793;
        public static int TMR_5160_290_02_363 = 807;
        public static int TPR_4111_290_01_946 = 810;
        public static int TMR_5160_290_02_363_LEGADO_INATIVO = 1016;
        public static int TMF_5100_290_02_402 = 1101;
        public static int TC_3100_290_02_071 = 1138;
        public static int TMRSD_5100_290_02_323 = 1164;
        public static int TMF_5100_290_02_349 = 1169;
        public static int TMD_5100_290_02_346 = 1183;

        // Permissoes
        public static string NENHUM_REGISTRO = "S/N";

        // Arquivos Logix
        public static string PEDIDOS_PENDENTES = "pedidos_pendentes.unl";
        public static string ESTOQUE_LOTE = "estoque_lote.unl";

        // Sim e Nao
        public static string SIM = "SIM";
        public static string NAO = "NAO";

        // Clientes
        public static int SICOOB = 68;
        public static int SICOOB_CONFEDERACAO = 246;

        // Filiais
        public static int PERTO_INDIA = 33;

        // Cores
        public static string COR_PRETO = "#212121";
        public static string COR_CINZA = "#808080";

        //Integracoes
        //public static string INT_ZAF_KEY = "f4eb70cb197b81aae231a3ddb1203169ef2a4b300372633078303c5a09dbacb9";
        public static string INT_ZAF_KEY = "12";

        //Dicionários
        public static Dictionary<string, string> DICIONARIO_CAMPOS_PLANILHA = new Dictionary<string, string>
            {
                { "NumSerie", "CodEquipContrato" },
                { "NFVenda", "CodInstalNFVenda" },
                { "NFVendaData", "CodInstalNFVenda" },
                { "NomeContrato", "CodContrato" },
                { "NomeCliente", "CodCliente" },
                { "NomeAutorizada", "CodAutorizada" },
                { "NomeRegiao", "CodRegiao" },
                { "NomeGrupoEquip", "CodGrupoEquip" },
                { "NomeTipoEquip", "CodTipoEquip" },
                { "NomeEquipamento", "CodEquip" },
                { "NomeSla", "CodSLA" },
                { "NomeInstalStatus", "CodInstalStatus" },
                { "NomeFilial", "CodFilial" },
                { "NomeLote", "CodInstalLote" },
                { "NumAgenciaDC", "CodPosto" },
                { "NomeTipoParcela", "CodInstalTipoParcela" },
                { "NumSeriePagto", "CodInstalacao" },
                { "MagnusPeca", "CodPeca" },
                { "ORStatus", "CodStatus" },
                { "NomeUsuario", "CodUsuario" },
                { "NomeUsuarioLab", "CodTecnico"}
            };

        // Tecnicos
        public static int TECNICO_SISTEMA = 2329;
        public static int INT_BB_TAMANHO_ARQUIVO = 830;

        // Tasks
        public static string INTEGRACAO_BANRISUL_ATM = "Integracao Banrisul ATM";
        public static string INTEGRACAO_ZAFFARI = "Integracao Zaffari";
        public static string INTEGRACAO_BB = "Integracao BB";
        public static string INTEGRACAO_LOGIX_MRP = "Integracao Logix MRP";
        public static string[] LOGS_URLS = {
            "D:\\SAT\\Branch\\SAT.V2\\SAT.TASKS\\Logs\\",
            "D:\\SAT\\Branch\\SAT.V2\\SAT.API\\Logs\\"
        };
    }
}
namespace SAT.MODELS.Enums
{
    public enum RoleEnum
    {
        ADM_DO_SISTEMA = 3,
        PV_COORDENADOR_DE_CONTRATO = 29,
        FILIAL_TECNICO_DE_CAMPO = 35,
        CLIENTE_AVANÇADO = 33,
    	CLIENTE_BASICO = 81,
        CLIENTE_INTERMEDIARIO = 93,
        CONTABILIDADE_PAGAMENTOS = 66,
        PARCEIROS = 71,
        FILIAL_SUPORTE_TÉCNICO_CAMPO = 79,
        RASTREAMENTO = 83,
        ANALISTA = 100,
    	ASSISTENTE = 107,
    	LIDER = 108,
    	COORDENADOR = 103,
    	SUPERVISOR = 104,
    	TECNICO = 105,
        TECNICO_OPERACOES = 106,
        INSPETOR = 109
    }

    public static class RoleGroup
    {
        public const string TECNICOS = "3, 32, 35, 61, 79, 84";
        public const string FINANCEIRO = "3, 16, 59, 65, 70";
    }
}
namespace SAT.MODELS.Enums
{
    public enum RoleEnum
    {
        ADMIN = 3,
        FINANCEIRO_COORDENADOR = 16,
        FILIAL_SUPORTE_TECNICO = 32,
        FILIAL_TECNICO_DE_CAMPO = 35,
        FINANCEIRO_ADMINISTRATIVO = 59,
        BANCADA_TECNICO = 61,
        PONTO_FINANCEIRO = 65,
        FINANCEIRO_COORDENADOR_PONTO = 70,
        FILIAL_SUPORTE_TECNICO_CAMPO = 79,
        FILIAL_LIDER = 82,
        FILIAL_SUPORTE_TECNICO_CAMPO_COM_CHAMADOS = 84
    }

    public static class RoleGroup
    {
        public const string TECNICOS = "3, 32, 35, 61, 79, 84";
        public const string FINANCEIRO = "3, 16, 59, 65, 70";
    }
}
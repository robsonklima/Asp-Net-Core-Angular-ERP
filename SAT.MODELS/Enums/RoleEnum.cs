namespace SAT.MODELS.Enums
{
    public enum RoleEnum
    {
        ADMIN = 3,
        FILIAL_SUPORTE_TECNICO = 32,
        FILIAL_TECNICO_DE_CAMPO = 35,
        BANCADA_TECNICO = 61,
        FILIAL_SUPORTE_TECNICO_CAMPO = 79,
        FILIAL_SUPORTE_TECNICO_CAMPO_COM_CHAMADOS = 84
    }

    public static class RoleGroup
    {
        public const string TECNICOS = "32, 35, 61, 79, 84";
    }
}
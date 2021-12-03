using System.ComponentModel;

namespace SAT.MODELS.Enums
{
    public enum NomeIndicadorEnum
    {
        [Description("PERFORMANCE FILIAIS")]
        PERFORMANCE_FILIALS,

        [Description("SLA FILIAL")]
        SLA_FILIAL,

        [Description("SLA CLIENTE")]
        SLA_CLIENTE,

        [Description("SPA CLIENTE")]
        SPA_CLIENTE,

        [Description("SPA FILIAL")]
        SPA_FILIAL,

        [Description("SPA TECNICO PERCENT")]
        SPA_TECNICO_PERCENT,

        [Description("SPA TECNICO QNT CHAMADOS")]
        SPA_TECNICO_QNT_CHAMADOS,

        [Description("PENDENCIA CLIENTE")]
        PENDENCIA_CLIENTE,

        [Description("PENDENCIA FILIAL")]
        PENDENCIA_FILIAL,

        [Description("PENDENCIA TECNICO PERCENT")]
        PENDENCIA_TECNICO_PERCENT,

        [Description("PENDENCIA TECNICO QNT CHAMADOS")]
        PENDENCIA_TECNICO_QNT_CHAMADOS,

        [Description("REINCIDENCIA CLIENTE")]
        REINCIDENCIA_CLIENTE,

        [Description("REINCIDENCIA FILIAL")]
        REINCIDENCIA_FILIAL,

        [Description("REINCIDENCIA TECNICO PERCENT")]
        REINCIDENCIA_TECNICO_PERCENT,

        [Description("REINCIDENCIA TECNICO QNT CHAMADOS")]
        REINCIDENCIA_TECNICO_QNT_CHAMADOS,

        [Description("REINCIDENCIA EQUIPAMENTO PERCENT")]
        REINCIDENCIA_EQUIPAMENTO_PERCENT,

        [Description("PECAS FILIAL")]
        PECAS_FILIAL,

        [Description("PECAS TOP MAIS FALTANTES")]
        PECAS_TOP_MAIS_FALTANTES,

        [Description("PECAS MAIS FALTANTES")]
        PECAS_MAIS_FALTANTES,

        [Description("PECAS NOVAS CADASTRADAS")]
        PECAS_NOVAS_CADASTRADAS,

        [Description("PECAS NOVAS LIBERADAS")]
        PECAS_NOVAS_LIBERADAS,

        [Description("DISPONIBILIDADE TECNICOS")]
        DISPONIBILIDADE_TECNICOS
    }
}

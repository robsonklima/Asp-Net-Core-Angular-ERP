using System.ComponentModel;

namespace SAT.INTEGRACOES.Correios
{
    public enum ChamadosCorreiosEnum
    {
        [Description("O")]
        CHAMADOS_ABERTOS = 0,
        [Description("A")]
        CHAMADOS_ATRIBUIDOS = 1,
        [Description("T")]
        CHAMADOS_TODOS = 2,
        [Description("D")]
        CHAMADOS_ATRASADOS = 3,
        [Description("F")]
        CHAMADOS_FECHADOS = 4
    }
}

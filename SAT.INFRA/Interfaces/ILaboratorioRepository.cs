using System.Collections.Generic;
using SAT.MODELS.Views;

namespace SAT.INFRA.Interfaces
{
    public interface ILaboratorioRepository
    {
        List<ViewLaboratorioTecnicoBancada> ObterTecnicosBancada();
    }
}

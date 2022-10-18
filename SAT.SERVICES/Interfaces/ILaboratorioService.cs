using System.Collections.Generic;
using SAT.MODELS.Views;

namespace SAT.SERVICES.Interfaces
{
    public interface ILaboratorioService
    {
        List<ViewLaboratorioTecnicoBancada> ObterTecnicosBancada();
    }
}
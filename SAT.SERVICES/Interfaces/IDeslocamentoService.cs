using System.Collections.Generic;
using SAT.MODELS.Entities;

namespace SAT.SERVICES.Interfaces
{
    public interface IDeslocamentoService
    {
        IEnumerable<Deslocamento> ObterPorParametros(DeslocamentoParameters parameters);
    }
}

using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;

namespace SAT.SERVICES.Interfaces
{
    public interface IOrcamentoStatusService
    {
        ListViewModel ObterPorParametros(OrcamentoStatusParameters parameters);
    }
}
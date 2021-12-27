using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;

namespace SAT.SERVICES.Interfaces
{
    public interface IOrdemServicoHistoricoService
    {
        ListViewModel ObterPorParametros(OrdemServicoHistoricoParameters parameters);
    }
}
using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;

namespace SAT.SERVICES.Interfaces
{
    public interface IDeslocamentoService
    {
        ListViewModel ObterPorParametros(DeslocamentoParameters parameters);
    }
}

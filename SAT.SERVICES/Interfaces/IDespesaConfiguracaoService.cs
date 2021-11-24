using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;

namespace SAT.SERVICES.Interfaces
{
    public interface IDespesaConfiguracaoService
    {
        ListViewModel ObterPorParametros(DespesaConfiguracaoParameters parameters);
    }
}
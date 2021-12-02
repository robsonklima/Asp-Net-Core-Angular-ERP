using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;

namespace SAT.SERVICES.Interfaces
{
    public interface IDespesaAdiantamentoTipoService
    {
        ListViewModel ObterPorParametros(DespesaAdiantamentoTipoParameters parameters);
    }
}

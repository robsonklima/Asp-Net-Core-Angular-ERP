using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;

namespace SAT.INFRA.Interfaces
{
    public interface IDespesaConfiguracaoRepository
    {
        PagedList<DespesaConfiguracao> ObterPorParametros(DespesaConfiguracaoParameters parameters);
    }
}

using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;

namespace SAT.INFRA.Interfaces
{
    public interface IDespesaConfiguracaoRepository
    {
        PagedList<DespesaConfiguracao> ObterPorParametros(DespesaConfiguracaoParameters parameters);

        void Criar(DespesaConfiguracao despesa);
        void Atualizar(DespesaConfiguracao despesa);
        DespesaConfiguracao ObterPorCodigo(int codigo);
    }
}

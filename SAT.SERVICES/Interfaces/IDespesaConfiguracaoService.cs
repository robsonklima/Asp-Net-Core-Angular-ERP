using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;

namespace SAT.SERVICES.Interfaces
{
    public interface IDespesaConfiguracaoService
    {
        ListViewModel ObterPorParametros(DespesaConfiguracaoParameters parameters);
        DespesaConfiguracao ObterPorCodigo(int codigo);
        DespesaConfiguracao Criar(DespesaConfiguracao despesa);
        void Atualizar(DespesaConfiguracao despesa);

    }
}
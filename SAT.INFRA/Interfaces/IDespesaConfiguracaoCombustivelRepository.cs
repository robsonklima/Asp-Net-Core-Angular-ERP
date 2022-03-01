using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;

namespace SAT.INFRA.Interfaces
{
    public interface IDespesaConfiguracaoCombustivelRepository
    {
        PagedList<DespesaConfiguracaoCombustivel> ObterPorParametros(DespesaConfiguracaoCombustivelParameters parameters);
        void Criar(DespesaConfiguracaoCombustivel despesaConfiguracao);
        void Deletar(int codigo);
        void Atualizar(DespesaConfiguracaoCombustivel despesaConfiguracao);
        DespesaConfiguracaoCombustivel ObterPorCodigo(int codigo);
    }
}

using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;

namespace SAT.SERVICES.Interfaces
{
    public interface IDespesaConfiguracaoCombustivelService
    {
        ListViewModel ObterPorParametros(DespesaConfiguracaoCombustivelParameters parameters);
        DespesaConfiguracaoCombustivel Criar(DespesaConfiguracaoCombustivel despesa);
        void Deletar(int codigo);
        void Atualizar(DespesaConfiguracaoCombustivel despesa);
        DespesaConfiguracaoCombustivel ObterPorCodigo(int codigo);
    }
}
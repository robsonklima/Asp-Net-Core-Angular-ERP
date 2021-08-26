using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;

namespace SAT.SERVICES.Interfaces
{
    public interface IDespesaCartaoCombustivelService
    {
        ListViewModel ObterPorParametros(DespesaCartaoCombustivelParameters parameters);
        DespesaCartaoCombustivel Criar(DespesaCartaoCombustivel despesaCartaoCombustivel);
        void Deletar(int codigo);
        void Atualizar(DespesaCartaoCombustivel despesaCartaoCombustivel);
        DespesaCartaoCombustivel ObterPorCodigo(int codigo);
    }
}

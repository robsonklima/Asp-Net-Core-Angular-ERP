using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;

namespace SAT.SERVICES.Interfaces
{
    public interface IPontoPeriodoModoAprovacaoService
    {
        ListViewModel ObterPorParametros(PontoPeriodoModoAprovacaoParameters parameters);
        PontoPeriodoModoAprovacao Criar(PontoPeriodoModoAprovacao pontoPeriodoModoAprovacao);
        void Deletar(int codigo);
        void Atualizar(PontoPeriodoModoAprovacao pontoPeriodoModoAprovacao);
        PontoPeriodoModoAprovacao ObterPorCodigo(int codigo);
    }
}

using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;

namespace SAT.INFRA.Interfaces
{
    public interface IPontoPeriodoModoAprovacaoRepository
    {
        PagedList<PontoPeriodoModoAprovacao> ObterPorParametros(PontoPeriodoModoAprovacaoParameters parameters);
        void Criar(PontoPeriodoModoAprovacao pontoPeriodoModoAprovacao);
        void Deletar(int codigo);
        void Atualizar(PontoPeriodoModoAprovacao pontoPeriodoModoAprovacao);
        PontoPeriodoModoAprovacao ObterPorCodigo(int codigo);
    }
}
using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;

namespace SAT.INFRA.Interfaces
{
    public interface IPontoPeriodoRepository
    {
        PagedList<PontoPeriodo> ObterPorParametros(PontoPeriodoParameters parameters);
        void Criar(PontoPeriodo pontoPeriodo);
        void Deletar(int codigo);
        void Atualizar(PontoPeriodo pontoPeriodo);
        PontoPeriodo ObterPorCodigo(int codigo);
    }
}
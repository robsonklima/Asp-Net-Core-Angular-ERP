using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;

namespace SAT.INFRA.Interfaces
{
    public interface IPontoPeriodoStatusRepository
    {
        PagedList<PontoPeriodoStatus> ObterPorParametros(PontoPeriodoStatusParameters parameters);
        void Criar(PontoPeriodoStatus pontoPeriodoStatus);
        void Deletar(int codigo);
        void Atualizar(PontoPeriodoStatus pontoPeriodoStatus);
        PontoPeriodoStatus ObterPorCodigo(int codigo);
    }
}
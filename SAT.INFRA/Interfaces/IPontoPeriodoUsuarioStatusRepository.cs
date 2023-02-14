using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;

namespace SAT.INFRA.Interfaces
{
    public interface IPontoPeriodoUsuarioStatusRepository
    {
        PagedList<PontoPeriodoUsuarioStatus> ObterPorParametros(PontoPeriodoUsuarioStatusParameters parameters);
        void Criar(PontoPeriodoUsuarioStatus pontoPeriodoUsuarioStatus);
        void Deletar(int codigo);
        void Atualizar(PontoPeriodoUsuarioStatus pontoPeriodoUsuarioStatus);
        PontoPeriodoUsuarioStatus ObterPorCodigo(int codigo);
    }
}
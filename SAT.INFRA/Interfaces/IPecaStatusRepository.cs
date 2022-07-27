using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;
using System.Linq;

namespace SAT.INFRA.Interfaces
{
    public interface IPecaStatusRepository
    {
        PagedList<PecaStatus> ObterPorParametros(PecaStatusParameters parameters);
        IQueryable<PecaStatus> ObterQuery(PecaStatusParameters parameters);
        void Criar(PecaStatus pecastatus);
        void Atualizar(PecaStatus pecastatus);
        void Deletar(int codPecaStatus);
        PecaStatus ObterPorCodigo(int codigo);
    }
}

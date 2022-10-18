using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;

namespace SAT.INFRA.Interfaces
{
    public interface IORStatusRepository
    {
        PagedList<ORStatus> ObterPorParametros(ORStatusParameters parameters);
        void Criar(ORStatus status);
        void Atualizar(ORStatus status);
        void Deletar(int codigo);
        ORStatus ObterPorCodigo(int codigo);
    }
}
